using System;
using System.Collections.Generic;
using System.Text;

namespace TodoBAL.Dtos
{
    public class ResponceDTO
    {
        public ResponceDTO(bool success, object result, string message = "", Exception ex = null)  
        {
            IsSuccess = success;
            Results = result;
            DisplayMessage = message;
            Exception = ex;
        }
        public bool IsSuccess { get; set; } = true;
        public object Results { get; set; }
        public string DisplayMessage { get; set; } = "";
        public Exception Exception { get; set; }
    }
}
