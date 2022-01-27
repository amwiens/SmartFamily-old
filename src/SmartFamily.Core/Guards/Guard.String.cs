using JetBrains.Annotations;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFamily.Core.Guards
{
    /// <content>
    /// Provides preconditions for <see cref="string"/> arguments.
    /// </content>
    public static partial class Guard
    {
        /// <summary>
        /// Requires the argument to have an empty string value.
        /// </summary>
        /// <param name="argument">The string argument.</param>
        /// <param name="message">
        ///     The factory to initialize the message of the exception that will be thrown if the
        ///     precondition is not satisfied.
        /// </param>
        /// <returns><paramref name="argument"/>.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument"/> value is not <c>null</c> and contains one or more characters.
        /// </exception>
        [AssertionMethod]
        [DebuggerStepThrough]
        [GuardFunction("String", "gem")]
        public static ref readonly ArgumentInfo<string> Empty(
            in this ArgumentInfo<string> argument, Func<string, string> message = null)
        {
            if (argument.Value?.Length > 0)
            {
                var m = message?.Invoke(argument.Value) ?? Messages.StringEmpty(argument);
                throw Fail(new ArgumentException(m, argument.Name));
            }

            return ref argument;
        }

        /// <summary>
        /// Requires the argument to have a non-empty string value.
        /// </summary>
        /// <param name="argument">The string argument.</param>
        /// <param name="message">
        ///     The message of the exception that will be thrown if the precondition is not satisfied.
        /// </param>
        /// <returns><paramref name="argument"/>.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument"/> value is not <c>null</c> and does not contain any characters.
        /// </exception>
        [AssertionMethod]
        [DebuggerStepThrough]
        [GuardFunction("String", "gnem")]
        public static ref readonly ArgumentInfo<string> NotEmpty(
            in this ArgumentInfo<string> argument, string message = null)
        {
            if (argument.Value?.Length == 0)
            {
                var m = message ?? Messages.StringNotEmpty(argument);
                throw Fail(new ArgumentException(m, argument.Name));
            }

            return ref argument;
        }

        /// <summary>
        /// Requires the argument to have a string value that ocnsists only of white-space characters.
        /// </summary>
        /// <param name="argument">The string argument.</param>
        /// <param name="message">
        ///     The factory to initialize the message of the exception that will be thrown if the
        ///     precondition is not satisfied.
        /// </param>
        /// <returns><paramref name="argument"/>.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument"/> value is not <c>null</c> and contains one or more
        ///     characters that are not white-space.
        /// </exception>
        [AssertionMethod]
        [DebuggerStepThrough]
        [GuardFunction("String", "gw")]
        public static ref readonly ArgumentInfo<string> WhiteSpace(
            in this ArgumentInfo<string> argument, Func<string, string> message = null)
        {
            if (argument.Value != null && !string.IsNullOrWhiteSpace(argument.Value))
            {
                var m = message?.Invoke(argument.Value) ?? Messages.StringWhiteSpace(argument);
                throw Fail(new ArgumentException(m, argument.Name));
            }

            return ref argument;
        }

        /// <summary>
        /// Requires the argument to have a string value that does not consist only of
        /// white-space characters.
        /// </summary>
        /// <param name="argument">The string argument.</param>
        /// <param name="message">
        ///     The factory to initialize the message of the exception that will be thrown if the
        ///     precondition is not satisfied.
        /// </param>
        /// <returns><paramref name="argument"/>.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument"/> value is not <c>null</c> and contains only of
        ///     white-space characters.
        /// </exception>
        [AssertionMethod]
        [DebuggerStepThrough]
        [GuardFunction("String", "gnw")]
        public static ref readonly ArgumentInfo<string> NotWhiteSpace(
            in this ArgumentInfo<string> argument, Func<string, string> message = null)
        {
            if (argument.Value != null && string.IsNullOrWhiteSpace(argument.Value))
            {
                var m = message?.Invoke(argument.Value) ?? Messages.StringNotWhiteSpace(argument);
                throw Fail(new ArgumentException(m, argument.Name));
            }

            return ref argument;
        }

        /// <summary>
        /// Requires the argument to have a string value that does not consist only of
        /// white-space characters.
        /// </summary>
        /// <param name="argument">The string argument.</param>
        /// <param name="message">
        ///     The message of the exception that will be thrown if the precondition is not satisfied.
        /// </param>
        /// <returns><paramref name="argument"/>.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument"/> value is not <c>null</c> and contains only of
        ///     white-space characters.
        /// </exception>
        [AssertionMethod]
        [DebuggerStepThrough]
        [GuardFunction("String", "gnw")]
        public static ref readonly ArgumentInfo<string> NotWhiteSpace(
            in this ArgumentInfo<string> argument, string message)
        {
            if (argument.Value != null && string.IsNullOrWhiteSpace(argument.Value))
            {
                var m = message ?? Messages.StringNotWhiteSpace(argument);
                throw Fail(new ArgumentException(m, argument.Name));
            }

            return ref argument;
        }
    }
}