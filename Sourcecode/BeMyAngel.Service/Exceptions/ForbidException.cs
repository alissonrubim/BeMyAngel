using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Exceptions
{
    public class ForbidException: Exception
    {
        public ForbidException(string Message) : base(Message)
        {

        }
    }
}
