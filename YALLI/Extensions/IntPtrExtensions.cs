using System;

namespace LoadLibraryDLLInjector.Extensions
{
    internal static class IntPtrExtensions
    {
        internal static bool IsNULL(
            this IntPtr ptr)
        {
            return ptr == IntPtr.Zero;
        }
    }
}
