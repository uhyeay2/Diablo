using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Models.ResponseObjects
{
    public class BaseResponse
    {
        public string[] ErrorMessages = Array.Empty<string>();
        public BaseResponse(string[] errorMessages ) { ErrorMessages = errorMessages; }
        public BaseResponse() { }
    }
}
