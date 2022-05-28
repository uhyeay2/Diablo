using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Exceptions
{
    public class NameAlreadyTakenException : Exception
    {
        public NameAlreadyTakenException(string message) : base(message) { }

        public NameAlreadyTakenException(string message, Exception innerException) : base(message, innerException) { }
    }
}
