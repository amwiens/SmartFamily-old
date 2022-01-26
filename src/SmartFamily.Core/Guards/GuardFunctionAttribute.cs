#nullable enable

using System.Reflection;

namespace SmartFamily.Core.Guards
{
    /// <summary>
    /// Marks a target as a function of <see cref="Guard"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal sealed class GuardFunctionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuardFunctionAttribute"/> class.
        /// </summary>
        /// <param name="group">The group that the marked function belongs to.</param>
        /// <param name="shortcut">The optional shortcut for snippet creation.</param>
        /// <param name="order">The priority of a function along its overloads.</param>
        /// <exception cref="ArgumentNullException"><paramref name="group"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="group"/> consist only of white-space characters,
        ///     <paramref name="shortcut"/> does not start with "g", <paramref name="shortcut"/>
        ///     does starts with "gx" or <paramref name="shortcut"/> is shorter than two characters
        /// </exception>
        public GuardFunctionAttribute(string group, string? shortcut = null, int order = 0)
        {
            Group = Guard.Argument(group, nameof(group))
                .NotNull()
                .NotWhitespace();

            Shortcut = Guard.Argument(shortcut, nameof(shortcut))
                .StartsWith("g", StringComparison.Ordinal)
                .DoesNotStartWith("gx", StringComparison.Ordinal)
                .MinLength(2);

            Order = order;
        }

        /// <summary>
        /// The group that the function belongs to.
        /// </summary>
        public string Group { get; }

        /// <summary>
        /// The shortcut of the function snippet.
        /// </summary>
        public string Shortcut { get; }

        /// <summary>
        /// The priority of a function along its overloads.
        /// </summary>
        public int Order { get; }


#if !NETSTANDARD1_0


        public static IEnumerable<KeyValuePair<MethodInfo, GuardFunctionAttribute>> GetMethods(Assembly assembly)
        {
            return from t in assembly.ExportedTypes
                   select t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance) into methods
                   from m in methods
                   let a = m.GetCustomAttribute<GuardFunctionAttribute>()
                   where a != null
                   orderby a.Group, m.Name, a.Order
                   select new KeyValuePair<MethodInfo, GuardFunctionAttribute>(m, a);
        }

#endif
    }
}