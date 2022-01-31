using SmartFamily.Core.Guards;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace SmartFamily.Core.Tests.Guards
{
    public sealed class EnumerableTests : BaseTests
    {
        [Flags]
        public enum CollectionOptions
        {
            Null = 0,

            Empty = 1,

            NotEmpty = 2,

            HasCount = 4,

            HasContains = 8,

            HasNullElement = 16,

            HasDuplicateElements = 32
        }

        [Theory(DisplayName = "Enumerable: Empty/NotEmpty")]
        [InlineData(CollectionOptions.Null, CollectionOptions.Null)]
        [InlineData(CollectionOptions.Empty, CollectionOptions.NotEmpty)]
        [InlineData(CollectionOptions.Empty | CollectionOptions.HasCount, CollectionOptions.HasCount)]
        public void Empty(CollectionOptions emptyOptions, CollectionOptions nonEmptyOptions)
        {
            var empty = GetEnumerable<int>(emptyOptions);
            var emptyArg = Guard.Argument(() => empty).Empty();
            CheckAndReset(empty, countCalled: true, enumerationCount: 0, enumerated: true);

            var nonEmpty = GetEnumerable<int>(nonEmptyOptions);
            var nonEmptyArg = Guard.Argument(() => nonEmpty).NotEmpty();
            CheckAndReset(nonEmpty, countCalled: true, enumerationCount: 1);

            if (empty is null)
            {
                emptyArg.NotEmpty();
                nonEmptyArg.Empty();
                return;
            }

            ThrowsArgumentException(
                nonEmptyArg,
                arg => arg.Empty(),
                (arg, message) => arg.Empty(e =>
                {
                    Assert.Same(nonEmpty, e);
                    return message;
                }));

            CheckAndReset(nonEmpty, countCalled: true, enumerationCount: 2);

            ThrowsArgumentException(
                emptyArg,
                arg => arg.NotEmpty(),
                (arg, message) => arg.NotEmpty(e =>
                {
                    Assert.Same(empty, e);
                    return message;
                }));

            CheckAndReset(empty, countCalled: true, enumerationCount: 0, enumerated: true);
        }



        private static ITestEnumerable<T> GetEnumerable<T>(CollectionOptions options, int maxCount = 10)
        {
            if (options == CollectionOptions.Null)
                return null;

            IEnumerable<T> items;
            if (options.HasFlag(CollectionOptions.Empty))
            {
                items = Array.Empty<T>();
            }
            else
            {
                var addCount = 0;
                if (options.HasFlag(CollectionOptions.HasNullElement))
                    addCount++;

                if (options.HasFlag(CollectionOptions.HasDuplicateElements))
                    addCount++;

                var range = Enumerable.Range(1, maxCount - addCount);
                var type = typeof(T);
                if (type == typeof(int))
                {
                    items = range as IEnumerable<T>;
                }
                else if (type == typeof(int?))
                {
                    items = range.Select(i => i as int?) as IEnumerable<T>;
                }
                else if(type == typeof(char))
                {
                    items = range.Select(i => (char)i) as IEnumerable<T>;
                }
                else if (type == typeof(char?))
                {
                    items = range.Select(i => (char)i as char?) as IEnumerable<T>;
                }
                else if (type == typeof(string))
                {
                    items = range.Select(i => i.ToString()) as IEnumerable<T>;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }

            var list = items.ToList();
            if (options.HasFlag(CollectionOptions.HasNullElement))
                list.Insert(RandomUtils.Current.Next(list.Count), default);

            if (options.HasFlag(CollectionOptions.HasDuplicateElements))
                list.Insert(RandomUtils.Current.Next(list.Count), list[RandomUtils.Current.Next(list.Count)]);

            var hasCount = options.HasFlag(CollectionOptions.HasCount);
            var hasContains = options.HasFlag(CollectionOptions.HasContains);

            if (hasCount && hasContains)
                return new TestEnumerableWithCountAndContains<T>(list);

            if (hasCount)
                return new TestEnumerableWithCount<T>(list);

            if (hasContains)
                return new TestEnumerableWithContains<T>(list);

            return new TestEnumerable<T>(list);
        }

        public class TestEnumerable<T> : ITestEnumerable<T>
        {
            public TestEnumerable(IEnumerable<T> items) => Items = items;

            public IEnumerable<T> Items { get; }

            public bool Enumerated { get; private set; }

            public int EnumerationCount { get; private set; }

            public IEnumerator<T> GetEnumerator()
            {
                Enumerated = true;
                foreach (var item in Items)
                {
                    EnumerationCount++;
                    yield return item;
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public virtual void Reset()
            {
                Enumerated = false;
                EnumerationCount = 0;
            }
        }

        public class TestEnumerableWithCount<T> : TestEnumerable<T>, ITestEnumerableWithCount<T>
        {
            private readonly int _count;

            public TestEnumerableWithCount(IEnumerable<T> items)
                : base(items) => _count = items.Count();

            public int Count
            {
                get
                {
                    CountCalled = true;
                    return _count;
                }
            }

            public bool CountCalled { get; private set; }

            public override void Reset()
            {
                base.Reset();
                {
                    base.Reset();
                    CountCalled = false;
                }
            }

        }

        public class TestEnumerableWithContains<T> : TestEnumerable<T>, ITestEnumerableWithContains<T>
        {
            public TestEnumerableWithContains(IEnumerable<T> items)
                : base(items)
            {
            }

            public bool ContainsCalled { get; private set; }

            public bool Contains(T item)
            {
                ContainsCalled = true;
                return Items.Contains(item);
            }

            public override void Reset()
            {
                base.Reset();
                ContainsCalled = false;
            }
        }

        public class TestEnumerableWithCountAndContains<T>
            : TestEnumerableWithCount<T>, ITestEnumerableWithCount<T>, ITestEnumerableWithContains<T>
        {
            public TestEnumerableWithCountAndContains(IEnumerable<T> items)
                : base(items)
            {
            }

            public bool ContainsCalled { get; private set; }

            public bool Contains(T item)
            {
                ContainsCalled = true;
                return Items.Contains(item);
            }

            public override void Reset()
            {
                base.Reset();
                ContainsCalled = false;
            }
        }
    }
}