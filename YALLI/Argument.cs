using System;

namespace YALLI
{
    internal static class Argument
    {
        internal static void IsNotNull(
            object param,
            string paramName,
            string message = "")
        {
            if (param == null)
                throw new ArgumentNullException(
                    paramName,
                    message);
        }

        internal static void IsNotNullOrWhitespace(
            string param,
            string paramName,
            string message = "")
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentNullException(
                    paramName,
                    message);
        }
    }
}
