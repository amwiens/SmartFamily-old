﻿using JetBrains.Annotations;

using System.Diagnostics;

namespace SmartFamily.Core.Guards
{
    /// <content>
    /// Provides preconditions for <see cref="bool"/> arguments.
    /// </content>
    public static partial class Guard
    {
        /// <summary>
        /// Requires the boolean argument to be <c>true</c>.
        /// </summary>
        /// <param name="argument">The boolean argument.</param>
        /// <param name="message">
        ///     The message of the exception that will be thrown if the precondition is not satisfied.
        /// </param>
        /// <returns><paramref name="argument"/>.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument"/> value is <c>false</c>.
        /// </exception>
        [AssertionMethod]
        [DebuggerStepThrough]
        [GuardFunction("Boolean", "gtrue")]
        public static ref readonly ArgumentInfo<bool> True(
            in this ArgumentInfo<bool> argument, string? message = null)
        {
            if (!argument.Value)
            {
                var m = message ?? Messages.True(argument);
                throw Fail(new ArgumentException(m, argument.Name));
            }

            return ref argument;
        }

        /// <summary>
        /// Requires the nullable boolean argument to be <c>true</c> or <c>null</c>.
        /// </summary>
        /// <param name="argument">The boolean argument.</param>
        /// <param name="message">
        ///     The message of the exception that will be thrown if the precondition is not satisfied.
        /// </param>
        /// <returns><paramref name="argument"/>.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument"/> value is <c>false</c>.
        /// </exception>
        [AssertionMethod]
        [DebuggerStepThrough]
        [GuardFunction("Boolean", "gtrue")]
        public static ref readonly ArgumentInfo<bool?> True(
            in this ArgumentInfo<bool?> argument, string? message = null)
        {
            if (argument.Value == false)
            {
                var m = message ?? Messages.True(argument);
                throw Fail(new ArgumentException(m, argument.Name));
            }

            return ref argument;
        }

        /// <summary>
        /// Requires the boolean argument to be <c>false</c>.
        /// </summary>
        /// <param name="argument">The boolean argument.</param>
        /// <param name="message">
        ///     The message of the exception that will be thrown if the precondition is not satisfied.
        /// </param>
        /// <returns><paramref name="argument"/>.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument"/> value is <c>true</c>.
        /// </exception>
        [AssertionMethod]
        [DebuggerStepThrough]
        [GuardFunction("Boolean", "gfalse")]
        public static ref readonly ArgumentInfo<bool> False(
            in this ArgumentInfo<bool> argument, string? message = null)
        {
            if (argument.Value)
            {
                var m = message ?? Messages.False(argument);
                throw Fail(new ArgumentException(m, argument.Name));
            }

            return ref argument;
        }

        /// <summary>
        /// Requires the nullable boolean argument to be <c>false</c> or <c>null</c>.
        /// </summary>
        /// <param name="argument">The boolean argument.</param>
        /// <param name="message">
        ///     The message of the exception that will be thrown if the precondition is not satisfied.
        /// </param>
        /// <returns><paramref name="argument"/>.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="argument"/> value is <c>true</c>.
        /// </exception>
        [AssertionMethod]
        [DebuggerStepThrough]
        [GuardFunction("Boolean", "gfalse")]
        public static ref readonly ArgumentInfo<bool?> False(
            in this ArgumentInfo<bool?> argument, string? message = null)
        {
            if (argument.GetValueOrDefault())
            {
                var m = message ?? Messages.False(argument);
                throw Fail(new ArgumentException(m, argument.Name));
            }

            return ref argument;
        }
    }
}