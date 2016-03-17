﻿using System;
using System.Runtime.CompilerServices;
using Bridge.Test;
using Bridge.ClientTest;

namespace Bridge.ClientTest.SimpleTypes
{
    [Category(Constants.MODULE_UINT64)]
    [TestFixture(TestNameFormat = "UInt64 - {0}")]
    public class UInt64Tests
    {
        [Test]
        public void TypePropertiesAreCorrect()
        {
            Assert.True((object)(ulong)0 is ulong);
            Assert.False((object)0.5 is ulong);
            Assert.AreEqual("Bridge.ULong", typeof(ulong).GetClassName());
            object l = (ulong)0;
            Assert.True(l is ulong);
            Assert.True(l is IComparable<ulong>);
            Assert.True(l is IEquatable<ulong>);
            Assert.True(l is IFormattable);
        }

        [Test]
        public void CastsWork()
        {
            long i2 = 0, i3 = 234, i4 = 9223372036854775000;
            long? ni2 = 0, ni3 = 234, ni4 = 9223372036854775000, ni6 = null;

            unchecked
            {
                Assert.True(0 == (ulong)i2, "0 unchecked");
                Assert.True(234 == (ulong)i3, "234 unchecked");
                Assert.True(9223372036854775000 == (ulong)i4, "9223372036854775000 unchecked");

                Assert.True(0 == (ulong?)ni2, "nullable 0 unchecked");
                Assert.True(234 == (ulong?)ni3, "nullable 234 unchecked");
                Assert.True(9223372036854775000 == (ulong?)ni4, "nullable 9223372036854775000 unchecked");
                Assert.True(null == (ulong?)ni6, "null unchecked");
            }

            checked
            {
                Assert.True(0 == (ulong)i2, "0 checked");
                Assert.True(234 == (ulong)i3, "234 checked");
                Assert.True(9223372036854775000 == (ulong)i4, "9223372036854775000 checked");

                Assert.True(0 == (ulong?)ni2, "nullable 0 checked");
                Assert.True(234 == (ulong?)ni3, "nullable 234 checked");
                Assert.True(9223372036854775000 == (ulong?)ni4, "nullable 9223372036854775000 checked");
                Assert.True(null == (ulong?)ni6, "null checked");
            }
        }

        [Test]
        public void OverflowWork()
        {
            ulong min = ulong.MinValue;
            ulong max = ulong.MaxValue;

            unchecked
            {
                Assert.True((max + 1) == min, "max + 1 unchecked");
                Assert.True((min - 1) == max, "min - 1 unchecked");
            }

            checked
            {
                Assert.Throws(() => { var l = max + 1; }, err => err is OverflowException, "max + 1 should be OverflowException");
                Assert.Throws(() => { var l = min - 1; }, err => err is OverflowException, "min - 1 should be OverflowException");
                Assert.Throws(() => { var l = max * max; }, err => err is OverflowException, "max * max should be OverflowException");
            }
        }

        private T GetDefaultValue<T>()
        {
            return default(T);
        }

        [Test]
        public void DefaultValueIs0()
        {
            Assert.True(0 == GetDefaultValue<ulong>());
        }

        [Test]
        public void DefaultConstructorReturnsZero()
        {
            Assert.True(0 == new ulong());
        }

        [IgnoreTest(Until = Constants.IGNORE_DATE)]
        [Test]
        public void CreatingInstanceReturnsZero()
        {
            Assert.True(0 == Activator.CreateInstance<ulong>());
        }

        [Test]
        public void ConstantsWork()
        {
            Assert.True(0 == ulong.MinValue);
        }

        [Test]
        public void FormatWorks()
        {
            Assert.AreEqual("123", ((ulong)0x123).Format("x"));
        }

        [Test]
        public void IFormattableToStringWorks()
        {
            Assert.AreEqual("123", ((ulong)0x123).ToString("x"));
        }

        [Test]
        public void CastingOfLargeValuesToUInt64Works()
        {
            double d1 = 5e9 + 0.5, d2 = -d1;
            Assert.True(5000000000 == (ulong)d1, "Positive");
            Assert.False((ulong)d2 > int.MaxValue, "Negative");
        }

        [Test]
        public void DivisionOfLargeUInt64Works()
        {
            ulong v1 = 50000000000L, v2 = 3;
            Assert.True(16666666666 == (v1 / v2));
        }

        [Test]
        public void TryParseWorks()
        {
            ulong numberResult;
            bool result = ulong.TryParse("23445", out numberResult);
            Assert.True(result);
            Assert.True(23445 == numberResult);

            result = ulong.TryParse("", out numberResult);
            Assert.False(result);
            Assert.True(0 == numberResult);

            result = ulong.TryParse(null, out numberResult);
            Assert.False(result);
            Assert.True(0 == numberResult);

            result = ulong.TryParse("notanumber", out numberResult);
            Assert.False(result);
            Assert.True(0 == numberResult);

            result = ulong.TryParse("-1", out numberResult);
            Assert.False(result);
            Assert.True(0 == numberResult);

            result = ulong.TryParse("2.5", out numberResult);
            Assert.False(result);
            Assert.True(0 == numberResult);

            result = ulong.TryParse("100000000000000000000", out numberResult);
            Assert.False(result);
            Assert.True(numberResult == 0);
        }

        [Test]
        public void ParseWorks()
        {
            Assert.True(23445 == ulong.Parse("23445"));
            Assert.Throws(() => ulong.Parse(""));
            Assert.Throws(() => ulong.Parse(null));
            Assert.Throws(() => ulong.Parse("notanumber"));
            Assert.Throws(() => ulong.Parse("-1"));
            Assert.Throws(() => ulong.Parse("2.5"));
            Assert.Throws(() => ulong.Parse("100000000000000000000"));
        }

        [Test]
        public void ToStringWithoutRadixWorks()
        {
            Assert.AreEqual("123", ((ulong)123).ToString());
        }

        [Test]
        public void ToStringWithRadixWorks()
        {
            Assert.AreEqual("123", ((ulong)123).ToString(10));
            Assert.AreEqual("123", ((ulong)0x123).ToString(16));
        }

        [Test]
        public void GetHashCodeWorks()
        {
            Assert.AreEqual(((ulong)0).GetHashCode(), ((ulong)0).GetHashCode());
            Assert.AreEqual(((ulong)1).GetHashCode(), ((ulong)1).GetHashCode());
            Assert.AreNotEqual(((ulong)1).GetHashCode(), ((ulong)0).GetHashCode());
        }

        [Test]
        public void EqualsWorks()
        {
            Assert.True(((ulong)0).Equals((object)(ulong)0));
            Assert.False(((ulong)1).Equals((object)(ulong)0));
            Assert.False(((ulong)0).Equals((object)(ulong)1));
            Assert.True(((ulong)1).Equals((object)(ulong)1));
        }

        [Test]
        public void IEquatableEqualsWorks()
        {
            Assert.True(((ulong)0).Equals((ulong)0));
            Assert.False(((ulong)1).Equals((ulong)0));
            Assert.False(((ulong)0).Equals((ulong)1));
            Assert.True(((ulong)1).Equals((ulong)1));

            Assert.True(((IEquatable<ulong>)((ulong)0)).Equals((ulong)0));
            Assert.False(((IEquatable<ulong>)((ulong)1)).Equals((ulong)0));
            Assert.False(((IEquatable<ulong>)((ulong)0)).Equals((ulong)1));
            Assert.True(((IEquatable<ulong>)((ulong)1)).Equals((ulong)1));
        }

        [Test]
        public void CompareToWorks()
        {
            Assert.True(((ulong)0).CompareTo((ulong)0) == 0);
            Assert.True(((ulong)1).CompareTo((ulong)0) > 0);
            Assert.True(((ulong)0).CompareTo((ulong)1) < 0);
        }

        [Test]
        public void IComparableCompareToWorks()
        {
            Assert.True(((IComparable<ulong>)((ulong)0)).CompareTo((ulong)0) == 0);
            Assert.True(((IComparable<ulong>)((ulong)1)).CompareTo((ulong)0) > 0);
            Assert.True(((IComparable<ulong>)((ulong)0)).CompareTo((ulong)1) < 0);
        }
    }
}
