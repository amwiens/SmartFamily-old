using SmartFamily.Core.Guards;

namespace SmartFamily.Core.Extensions
{
    public static class GuardExtensions
    {
        /// <summary>
        /// Checks if the string is a valid email address.
        /// </summary>
        /// <param name="guard"></param>
        /// <returns>String value.</returns>
        public static Guard.ArgumentInfo<string> IsAValidEmail(this Guard.ArgumentInfo<string> guard) =>
            guard.NotNull()
                 .NotWhiteSpace()
                 .Matches("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");

        /// <summary>
        /// Checks if the string is a valid string.
        /// </summary>
        /// <param name="guard"></param>
        /// <returns>String value.</returns>
        public static Guard.ArgumentInfo<string> IsAValidString(this Guard.ArgumentInfo<string> guard) =>
            guard.NotNull()
                 .NotWhiteSpace()
                 .Modify(s => s.Trim());
    }
}