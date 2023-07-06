using System.Net;

namespace KingsUsers.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message)
            : base(message, null, HttpStatusCode.BadRequest)
        {
        }
    }

}
