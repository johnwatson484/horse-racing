using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HorseRacing.Exceptions
{
    [Serializable]
    public class RaceException : Exception
    {
        public RaceException()
        {
        }

        public RaceException(string message) : base(message)
        {
        }

        public RaceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RaceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}