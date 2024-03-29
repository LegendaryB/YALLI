﻿using System;
using System.Diagnostics;

namespace YALLI.Extensions
{
    public static class ProcessExtensions
    {
        public static IntPtr LoadModule(
            this Process process,
            string moduleName)
        {
            return Injector.Inject(
                process,
                moduleName);
        }
    }
}
