using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diablo.Domain.Exceptions
{
    public class PlayerNotFoundException : Exception
    {
        public PlayerNotFoundException(string message) : base(message) { }

        public PlayerNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
