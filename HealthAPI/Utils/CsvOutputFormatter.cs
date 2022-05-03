using HealthAPI.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthAPI.Utils
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(PatientDto).IsAssignableFrom(type) || typeof(IEnumerable<PatientDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if ( context.Object is IEnumerable<PatientDto>)
            {
                foreach(var patient in (IEnumerable<PatientDto>)context.Object)
                {
                    FormatCsv(buffer, patient);
                }
            } else
            {
                FormatCsv(buffer, (PatientDto)context.Object);
            }
        }

        public static void FormatCsv(StringBuilder buffer, PatientDto patientDto)
        {
            buffer.AppendLine($"{patientDto.Id}, \"{patientDto.Gender}, \"{patientDto.FirstName}");
        }
    }
}
