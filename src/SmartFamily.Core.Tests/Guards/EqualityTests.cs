using SmartFamily.Core.Guards;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace SmartFamily.Core.Tests.Guards
{
    public sealed class EqualityTests : BaseTests
    {
        [Theory(DisplayName = "Equality: Default/NotDefault")]
        [InlineData(null, null)]
        [InlineData(0, 1)]
        public void Default(int? @default, int? nonDefault)
        {
            var nullableDefaultArg = Guard.Argument(() => @default).Default();
            var nullableNonDefaultArg = Guard.Argument(() => nonDefault).NotDefault();
            if (!@default.HasValue)
            {
                nullableDefaultArg.NotDefault();
                nullableNonDefaultArg.Default();
                return;
            }

            ThrowsArgumentException(
                nullableNonDefaultArg,
                arg => arg.Default(),
                (arg, message) => arg.Default(i =>
                {
                    Assert.Equal(nonDefault, i);
                    return message;
                }));

            ThrowsArgumentException(
                nullableDefaultArg,
                arg => arg.NotDefault(),
                (arg, message) => arg.NotDefault(message));

            var defaultArg = Guard.Argument(@default.Value, nameof(@default)).Default();
            var nonDefaultArg = Guard.Argument(nonDefault.Value, nameof(nonDefault)).NotDefault();
            ThrowsArgumentException(
                nonDefaultArg,
                arg => arg.Default(),
                (arg, message) => arg.Default(i =>
                {
                    Assert.Equal(nonDefault, i);
                    return message;
                }));

            ThrowsArgumentException(
                defaultArg,
                arg => arg.NotDefault(),
                (arg, message) => arg.NotDefault(message));
        }


    }
}