﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_Basic_03_rev1
{
    internal class ArgumentOutOfRangeIntTypeException : Exception
    {
        public ArgumentOutOfRangeIntTypeException(string message)
        : base(message){ }
    }
}
