using JetBrains.Annotations;

using System.Diagnostics;

namespace SmartFamily.Core.Guards
{
    /// <content>
    /// Provides preconditions for <see cref="Uri"/> arguments.
    /// </content>
    public static partial class Guard
    {
        /// <summary>
        /// The URI scheme for Hypertext Transfer Protocol (HTTP).
        /// </summary>
        private const string HttpUriScheme = "http"; // Uri.UriSchemeHttp

        /// <summary>
        /// The URI scheme for Secure Hypertext Transfer Protocol (HTTPS).
        /// </summary>
        private const string HttpsUriScheme = "https"; // Uri.UriSchemeHttps

        /// <summary>
        /// Requires the argument value to ben an absolute URI.
        /// </summary>
        /// <param name="argument">The URI argument.</param>
        /// <param name="message">
        ///     The factory to initialize the message of the exception that will be thrown if the
        ///     precondition is not satisfied.
        /// </param>
        /// <returns><paramref name="argument"/>.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument"/> value is neither <c>null</c> nor an absolute URI.
        /// </exception>
        [AssertionMethod]
        [DebuggerStepThrough]
        [GuardFunction("Uri", "gabs")]
        public static ref readonly ArgumentInfo<Uri> Absolute(
            in this ArgumentInfo<Uri> argument, Func<Uri, string> message = null)
        {
            if (argument.HasValue() && !argument.Value.IsAbsoluteUri)
            {
                var m = message?.Invoke(argument.Value) ?? Messages.UriAbsolute(argument);
                throw Fail(new ArgumentException(m, argument.Name));
            }

            return ref argument;
        }


    }
}