using System;

namespace HealthAPI.Utils
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string userIdentifier) : base($"The user with credential: {userIdentifier} does not exist in the database")
        { }
    }
    public class PatientNotFoundException : NotFoundException
    {
        public PatientNotFoundException(Guid patientId)
            : base($"The patient with id: {patientId} doesn't exist in the database.")
        {

        }
    }
}
