using System.Text.RegularExpressions;

namespace Scheduler.Services
{
    public static partial class InputRegExps
    {
        [GeneratedRegex("^(\\+)?\\d{11}$")]
        public static partial Regex PhoneRegEx();
        [GeneratedRegex("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9]+$")]
        public static partial Regex EmailRegEx();
        [GeneratedRegex("[^@a-zA-Z0-9_\\-\\.]+")]
        public static partial Regex EmailLocaleRegEx();
    }
}
