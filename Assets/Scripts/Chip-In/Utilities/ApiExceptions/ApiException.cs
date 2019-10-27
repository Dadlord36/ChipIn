using System.IO;

namespace Utilities.ApiExceptions
{

    
    public class ApiException : IOException
    {
        public ApiException(string message) : base(message)
        {
        }
    }
}