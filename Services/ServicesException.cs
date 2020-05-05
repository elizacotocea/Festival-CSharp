﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    public class ServicesException : Exception
    {
        public ServicesException() : base() { }

        public ServicesException(String msg) : base(msg) { }

        public ServicesException(String msg, Exception ex) : base(msg, ex) { }

    }
}
