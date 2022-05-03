using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore;
using HealthAPI.Utils;
using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using HealthAPI.Middlewares;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using AspNetCoreRateLimit;
using NLog;
using System.IO;
using HealthAPI.Repositories.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using HealthAPI.ActionFilters;

namespace HealthAPI
{
    public class Startup
    {
        readonly string AllowedOriginSpecifications = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /*NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
            new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
            .Services.BuildServiceProvider()
            .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
            .OfType<NewtonsoftJsonInputFormatter>().First();*/

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // add cors to service
            services.ConfigureCors();
            services.ConfigureLoggerService();

            // add compression to response
            services.AddResponseCompression();



            services.AddControllers(
                config =>
                {
                    config.RespectBrowserAcceptHeader = true;
                    config.ReturnHttpNotAcceptable = true;
                    // config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
                }).AddXmlDataContractSerializerFormatters()
                .AddCustomCSVFormatter()
                .AddNewtonsoftJson(
                opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .AddFluentValidation();

            services.ConfigureFluentValidators();

            services.ConfigureJWT(Configuration);
            services.ConfigureIdentity();

            services.AddScoped<ValidationFilterAttribute>();

            services.ConfigureRepositoryManager();
            services.ConfigureServiceManager();

            // register auto mapper
            services.AddAutoMapper(typeof(Startup));

            // memory cache
            services.AddMemoryCache();

            // configure service for file upload
            services.Configure<FormOptions>(f =>
            {
                f.ValueLengthLimit = int.MaxValue;
                f.MultipartBodyLengthLimit = int.MaxValue; // change to defined upload value
                //f.MemoryBufferThreshold = int.MaxValue;
            });

            // response caching
            services.AddResponseCaching(options =>
            {
                // Each response cannot be more than 1 KB 
                options.MaximumBodySize = 1024;

                // Case Sensitive Paths 
                // Responses to be returned only if case sensitive paths match
                options.UseCaseSensitivePaths = true;
            });

            services.ConfigureRateLimitingOptions();
            services.AddHttpContextAccessor();

            // enable api health check
            services.AddHealthChecks();

            // Configure API version
            services.ConfigureApiVersioning();

            // versioning explorer
            services.ConfigureApiVersioningExplorer();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());


            // configure mysql connection
            services.ConfigureMySqlContext(Configuration);

            // Add Hangfire services
            string hangfireConnectionString = Configuration.GetConnectionString("HangFireDb");
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(
                    new MySqlStorage(
                        hangfireConnectionString,
                        new MySqlStorageOptions
                        {
                            QueuePollInterval = TimeSpan.FromSeconds(10),
                            JobExpirationCheckInterval = TimeSpan.FromHours(1),
                            CountersAggregateInterval = TimeSpan.FromMinutes(5),
                            PrepareSchemaIfNecessary = true,
                            DashboardJobListLimit = 25000,
                            TransactionTimeout = TimeSpan.FromMinutes(1),
                            TablesPrefix = "Hangfire",
                        })
                    )
                );

            // format global json message response
            services.ConfigureGlobalJsonFormat();

            // Add the processing server as IHostedService
            services.AddHangfireServer(options => options.WorkerCount = 1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler(logger);

            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
            });

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        //options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                        options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });

            app.UseMiddleware<GlobalErrorHandler>();

            app.UseRouting();

            app.UseIpRateLimiting();

            app.UseCors(AllowedOriginSpecifications);
            app.UseResponseCaching();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                /*endpoints.MapHangfireDashboard("/hangfire", new DashboardOptions
                {
                    Authorization = new[] { new HangfireAuthorizationFilter() },
                    IgnoreAntiforgeryToken = true
                });*/
            });

            app.UseHangfireDashboard();
        }
    }
}
