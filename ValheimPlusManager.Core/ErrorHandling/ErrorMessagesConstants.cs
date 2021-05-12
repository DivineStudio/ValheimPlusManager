using System;

namespace ValheimPlusManager.Core.ErrorHandling
{
    internal static class ErrorMessagesConstants
    {
        public static string DefaultMessage => "{0} is invalid. {0}={1}";
        public static string DefaultMessageWithException => $"{DefaultMessage}{Environment.NewLine}Inner Exception={{2}}";
        public static string NullArg => "{null}";
        public static string HiddenArg => "{hidden}";
    }
}
