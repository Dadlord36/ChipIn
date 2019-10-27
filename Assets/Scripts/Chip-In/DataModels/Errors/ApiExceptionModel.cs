using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataModels.Errors
{
    public class ApiExceptionModel
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Status { get; set; }
    }
}