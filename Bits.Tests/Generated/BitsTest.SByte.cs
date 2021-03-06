//
// AUTO-GENERATED
//
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Medallion.Tests
{
    public partial class BitsTest
    {
        /// <summary>
        /// <see cref="Bits.ShiftLeft(sbyte, int)"/>
        /// </summary>
        [Test]
        public void TestShiftLeftSByte() => this.TestShiftSByteHelper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.ShiftRight(sbyte, int)"/>
        /// </summary>
        [Test]
        public void TestShiftRightSByte() => this.TestShiftSByteHelper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestShiftLeftSByte"/> and <see cref="TestShiftRightSByte"/>
        /// </summary>
        private void TestShiftSByteHelper(bool isLeft)
        {
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfSByteInBits; i < Bits.SizeOfSByteInBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomSByte();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(sbyte value, int positions)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = ShiftString(valueBits, positions);
                var actual = isLeft ? Bits.ShiftLeft(value, positions) : Bits.ShiftRight(value, positions);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"Shift{(isLeft ? "Left" : "Right")}({value}, {positions})");
            }

            string ShiftString(string bits, int positions)
            {
                var actualPositions = positions % bits.Length;
                if (actualPositions < 0) { actualPositions += bits.Length; }
                return isLeft 
                    ? bits.Substring(actualPositions, bits.Length - actualPositions) + new string('0', actualPositions)
                    : new string(CodeGenerator.IsUnsigned(typeof(sbyte)) ? '0' : bits[0], actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.And(sbyte, sbyte)"/>
        /// </summary>
        [Test]
        public void TestAndSByte()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomSByte();
                var randomValue2 = this.GetRandomSByte();
                Assert.AreEqual((sbyte)(randomValue1 & randomValue2), Bits.And(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Or(sbyte, sbyte)"/>
        /// </summary>
        [Test]
        public void TestOrSByte()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomSByte();
                var randomValue2 = this.GetRandomSByte();
                Assert.AreEqual((sbyte)(randomValue1 | randomValue2), Bits.Or(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.Not(sbyte)"/>
        /// </summary>
        [Test]
        public void TestNotSByte()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomSByte();
                Assert.AreEqual((sbyte)(~randomValue), Bits.Not(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.Xor(sbyte, sbyte)"/>
        /// </summary>
        [Test]
        public void TestXorSByte()
        {
            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue1 = this.GetRandomSByte();
                var randomValue2 = this.GetRandomSByte();
                Assert.AreEqual((sbyte)(randomValue1 ^ randomValue2), Bits.Xor(randomValue1, randomValue2));
            }
        }

        /// <summary>
        /// <see cref="Bits.HasAnyFlag(sbyte, sbyte)"/>
        /// </summary>
        [Test]
        public void TestHasAnyFlagSByte()
        {
            Assert.IsTrue(((sbyte)1).HasAnyFlag(1));
            Assert.IsTrue(((sbyte)3).HasAnyFlag(2));
            Assert.IsFalse(((sbyte)18).HasAnyFlag(9));
            Assert.IsFalse(sbyte.MaxValue.HasAnyFlag(0));
            Assert.IsFalse(((sbyte)0).HasAnyFlag(0));
        }

        /// <summary>
        /// <see cref="Bits.HasAllFlags(sbyte, sbyte)"/>
        /// </summary>
        [Test]
        public void TestHasAllFlagsSByte()
        {
            Assert.IsTrue(((sbyte)1).HasAllFlags(1));
            Assert.IsTrue(((sbyte)3).HasAllFlags(2));
            Assert.IsFalse(((sbyte)18).HasAllFlags(9));
            Assert.IsTrue(sbyte.MaxValue.HasAllFlags(0));
            Assert.IsTrue(((sbyte)0).HasAllFlags(0));
        }

        /// <summary>
        /// <see cref="Bits.GetBit(sbyte, int)"/>
        /// </summary>
        [Test]
        public void TestGetBitSByte()
        {
            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.IsTrue((((sbyte)1) << i).GetBit(i));
                Assert.IsFalse((~(((sbyte)1) << i)).GetBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).GetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).GetBit(Bits.SizeOfSByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.SetBit(sbyte, int)"/>
        /// </summary>
        [Test]
        public void TestSetBitSByte()
        {
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            var noBitsSet = default(sbyte);

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual((sbyte)(((sbyte)1) << i), default(sbyte).SetBit(i));
                Assert.AreEqual(((sbyte)25).SetBit(i), ((sbyte)25).SetBit(i).SetBit(i));
                Assert.AreEqual(allBitsSet, allBitsSet.SetBit(i));
                noBitsSet = noBitsSet.SetBit(i);
            }

            Assert.AreEqual(allBitsSet, noBitsSet);

            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).SetBit(Bits.SizeOfSByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearBit(sbyte, int)"/>
        /// </summary>
        [Test]
        public void TestClearBitSByte()
        {
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual((sbyte)0, default(sbyte).ClearBit(i));
                Assert.AreEqual(((sbyte)25).ClearBit(i), ((sbyte)25).ClearBit(i).ClearBit(i));
                Assert.AreEqual((sbyte)~(((sbyte)1) << i), allBitsSet.ClearBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).SetBit(Bits.SizeOfSByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.FlipBit(sbyte, int)"/>
        /// </summary>
        [Test]
        public void TestFlipBitSByte()
        {
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual((sbyte)(((sbyte)1) << i), default(sbyte).FlipBit(i));
                Assert.AreEqual(((sbyte)25), ((sbyte)25).FlipBit(i).FlipBit(i));
                Assert.AreEqual((sbyte)~(((sbyte)1) << i), allBitsSet.FlipBit(i));
            }

            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).SetBit(-1));
            Assert.Throws<IndexOutOfRangeException>(() => default(sbyte).SetBit(Bits.SizeOfSByteInBits + 1));
        }

        /// <summary>
        /// <see cref="Bits.ClearLeastSignificantOneBit(sbyte)"/>
        /// </summary>
        [Test]
        public void TestClearLeastSignificantOneBitSByte()
        {
            Assert.AreEqual((sbyte)0b101000, Bits.ClearLeastSignificantOneBit((sbyte)0b101100));
            Assert.AreEqual(default(sbyte), Bits.ClearLeastSignificantOneBit(default(sbyte)));

            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual(default(sbyte), Bits.ClearLeastSignificantOneBit((sbyte)((sbyte)1 << i)));
                allBitsSet = Bits.ClearLeastSignificantOneBit(allBitsSet);
            }

            Assert.AreEqual(default(sbyte), allBitsSet);
        }

        /// <summary>
        /// <see cref="Bits.SetLeastSignificantZeroBit(sbyte)(sbyte)"/>
        /// </summary>
        [Test]
        public void TestSetLeastSignificantZeroBitSByte()
        {
            Assert.AreEqual((sbyte)0b010111, Bits.SetLeastSignificantZeroBit((sbyte)0b010011));
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Assert.AreEqual(allBitsSet, Bits.SetLeastSignificantZeroBit(allBitsSet));

            var value = default(sbyte);
            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual(allBitsSet, Bits.SetLeastSignificantZeroBit(allBitsSet.ClearBit(i)));
                value = Bits.SetLeastSignificantZeroBit(allBitsSet);
            }

            Assert.AreEqual(allBitsSet, value);
        }

        /// <summary>
        /// <see cref="Bits.SetTrailingZeroBits(sbyte)"/>
        /// </summary>
        [Test]
        public void TestSetTrailingZeroBitsSByte()
        {
            Check(default(sbyte));
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Check(allBitsSet);
            Check(sbyte.MinValue);
            Check(sbyte.MaxValue);

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Check(Bits.ShiftLeft((sbyte)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomSByte());
            }

            void Check(sbyte value)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = SetTrailingZeroBitsString(valueBits);
                var actual = Bits.SetTrailingZeroBits(value);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"TestSetTrailingZeroBitsSByte({value} /* {valueBits} */)");
            }

            string SetTrailingZeroBitsString(string bits)
            {
                var lastOneBitIndex = bits.LastIndexOf('1');
                return bits.Substring(0, lastOneBitIndex + 1) + new string('1', bits.Length - (lastOneBitIndex + 1));
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateLeastSignificantOneBit(sbyte)"/>
        /// </summary>
        [Test]
        public void TestIsolateLeastSignificantOneBitSByte()
        {
            Assert.AreEqual((sbyte)0b000100, Bits.IsolateLeastSignificantOneBit((sbyte)0b101100));
            Assert.AreEqual(default(sbyte), Bits.IsolateLeastSignificantOneBit(default(sbyte)));

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual(default(sbyte).SetBit(i), Bits.IsolateLeastSignificantOneBit((default(sbyte).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(sbyte).SetBit(i - 1), Bits.IsolateLeastSignificantOneBit(default(sbyte).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.IsolateMostSignificantOneBit(sbyte)"/>
        /// </summary>
        [Test]
        public void TestIsolateMostSignificantOneBitSByte()
        {
            Assert.AreEqual((sbyte)0b100000, Bits.IsolateMostSignificantOneBit((sbyte)0b101100));
            Assert.AreEqual(default(sbyte), Bits.IsolateMostSignificantOneBit(default(sbyte)));

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.AreEqual(default(sbyte).SetBit(i), Bits.IsolateMostSignificantOneBit((default(sbyte).SetBit(i))));
                if (i > 0)
                {
                    Assert.AreEqual(default(sbyte).SetBit(i), Bits.IsolateMostSignificantOneBit(default(sbyte).SetBit(i - 1).SetBit(i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.BitCount(sbyte)"/>
        /// </summary>
        [Test]
        public void TestBitCountSByte()
        {
            Assert.AreEqual(0, Bits.BitCount((sbyte)0));
            Assert.AreEqual(1, Bits.BitCount((sbyte)1));
            Assert.AreEqual(2, Bits.BitCount(default(sbyte).SetBit(Bits.SizeOfSByteInBits / 2).SetBit(Bits.SizeOfSByteInBits - 1)));

            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Assert.AreEqual(Bits.SizeOfSByteInBits, Bits.BitCount(allBitsSet));
            Assert.AreEqual(Bits.SizeOfSByteInBits - 2, Bits.BitCount(allBitsSet.ClearBit(Bits.SizeOfSByteInBits / 2).ClearBit(0)));

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomSByte();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString.Count(ch => ch == '1'), Bits.BitCount(randomValue), binaryString);
            }
        }

        /// <summary>
        /// <see cref="Bits.TrailingZeroBitCount(sbyte)"/>
        /// </summary>
        [Test]
        public void TestTrailingZeroBitCountSByte()
        {
            Assert.AreEqual(Bits.SizeOfSByteInBits, Bits.TrailingZeroBitCount((sbyte)0));
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Assert.AreEqual(0, Bits.TrailingZeroBitCount(allBitsSet));

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(i + 1, Bits.TrailingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomSByte();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfSByteInBits : binaryString.Length - binaryString.TrimEnd('0').Length, Bits.TrailingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.LeadingZeroBitCount(sbyte)"/>
        /// </summary>
        [Test]
        public void TestLeadingZeroBitCountSByte()
        {
            Assert.AreEqual(Bits.SizeOfSByteInBits, Bits.LeadingZeroBitCount((sbyte)0));
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Assert.AreEqual(0, Bits.LeadingZeroBitCount(allBitsSet));

            for (var i = Bits.SizeOfSByteInBits - 1; i >= 0; --i)
            {
                allBitsSet = allBitsSet.ClearBit(i);
                Assert.AreEqual(Bits.SizeOfSByteInBits - i, Bits.LeadingZeroBitCount(allBitsSet));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomSByte();
                var binaryString = Bits.ToShortBinaryString(randomValue);
                Assert.AreEqual(binaryString == "0" ? Bits.SizeOfSByteInBits : Bits.SizeOfSByteInBits - binaryString.Length, Bits.LeadingZeroBitCount(randomValue));
            }
        }

        /// <summary>
        /// <see cref="Bits.HasSingleOneBit(sbyte)"/>
        /// </summary>
        [Test]
        public void TestHasSingleOneBitSByte()
        {
            Assert.IsTrue(Bits.HasSingleOneBit((sbyte)1));
            Assert.IsFalse(Bits.HasSingleOneBit((sbyte)0));
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Assert.IsFalse(Bits.HasSingleOneBit(allBitsSet));

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Assert.IsTrue(Bits.HasSingleOneBit(Bits.ShiftLeft((sbyte)1, i)));
                Assert.IsFalse(Bits.HasSingleOneBit(Bits.Not(Bits.ShiftLeft((sbyte)1, i))));
                if (i < Bits.SizeOfSByteInBits - 1)
                {
                    Assert.IsFalse(Bits.HasSingleOneBit(Bits.ShiftLeft((sbyte)0b11, i)));
                }
            }
        }

        /// <summary>
        /// <see cref="Bits.RotateLeft(sbyte, int)"/>
        /// </summary>
        [Test]
        public void TestRotateLeftSByte() => this.TestRotateSByteHelper(isLeft: true);

        /// <summary>
        /// <see cref="Bits.RotateRight(sbyte, int)"/>
        /// </summary>
        [Test]
        public void TestRotateRightSByte() => this.TestRotateSByteHelper(isLeft: false);

        /// <summary>
        /// Helper for <see cref="TestRotateLeftSByte"/> and <see cref="TestRotateRightSByte"/>
        /// </summary>
        private void TestRotateSByteHelper(bool isLeft)
        {
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);

            Check(10, int.MinValue);
            Check(10, int.MaxValue);

            for (var i = -Bits.SizeOfSByteInBits; i < Bits.SizeOfSByteInBits; ++i)
            {
                Check(0, i);
                Check(1, i);
                Check(allBitsSet, i);
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                var randomValue = this.GetRandomSByte();
                var randomPositions = this._random.Value.Next(int.MinValue, int.MaxValue);
                Check(randomValue, randomPositions);
            }

            void Check(sbyte value, int positions)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = RotateString(valueBits, positions);
                var actual = isLeft ? Bits.RotateLeft(value, positions) : Bits.RotateRight(value, positions);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"Rotate{(isLeft ? "Left" : "Right")}({value}, {positions})");
            }

            string RotateString(string bits, int positions)
            {
                var actualPositions = positions % bits.Length;
                if (actualPositions < 0) { actualPositions += Bits.SizeOfSByteInBits; }
                return isLeft
                    ? bits.Substring(actualPositions) + bits.Substring(0, actualPositions)
                    : bits.Substring(bits.Length - actualPositions) + bits.Substring(0, bits.Length - actualPositions);
            }
        }

        /// <summary>
        /// <see cref="Bits.Reverse(sbyte)"/>
        /// </summary>
        [Test]
        public void TestReverseSByte()
        {
            Check(default(sbyte));
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Check(allBitsSet);
            Check(sbyte.MinValue);
            Check(sbyte.MaxValue);

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Check(Bits.ShiftLeft((sbyte)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomSByte());
            }

            void Check(sbyte value)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = ReverseString(valueBits);
                var actual = Bits.Reverse(value);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"Reverse({value} /* {valueBits} */)");
            }

            string ReverseString(string bits)
            {
                var array = bits.ToCharArray();
                Array.Reverse(array);
                return new string(array);
            }
        }

        /// <summary>
        /// <see cref="Bits.ReverseBytes(sbyte)"/>
        /// </summary>
        [Test]
        public void TestReverseBytesSByte()
        {
            Check(default(sbyte));
            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Check(allBitsSet);
            Check(sbyte.MinValue);
            Check(sbyte.MaxValue);

            for (var i = 0; i < Bits.SizeOfSByteInBits; ++i)
            {
                Check(Bits.ShiftLeft((sbyte)1, i));
            }

            // fuzz testing
            for (var i = 0; i < FuzzTestIterations; ++i)
            {
                Check(this.GetRandomSByte());
            }

            void Check(sbyte value)
            {
                var valueBits = Bits.ToLongBinaryString(value);
                var expectedBits = ReverseStringBytes(valueBits);
                var actual = Bits.ReverseBytes(value);
                var actualBits = Bits.ToLongBinaryString(actual);
                Assert.AreEqual(expectedBits, actualBits, $"Reverse({value} /* {valueBits} */)");
            }

            string ReverseStringBytes(string bits)
            {
                return string.Join(
                    string.Empty,
                    Enumerable.Range(0, bits.Length / 8)
                        .Select(i => bits.Substring(i * 8, 8))
                        .Reverse()
                );
            }
        }

        /// <summary>
        /// <see cref="Bits.ToShortBinaryString(sbyte)"/>
        /// </summary>
        [Test]
        public void TestToShortBinaryStringSByte()
        {
            Assert.AreEqual("0", Bits.ToShortBinaryString((sbyte)0));
            Assert.AreEqual("1", Bits.ToShortBinaryString((sbyte)1));
            Assert.AreEqual("10101010", Bits.ToShortBinaryString(unchecked((sbyte)0b10101010)));
            Assert.AreEqual("1010101", Bits.ToShortBinaryString(unchecked((sbyte)0b01010101)));

            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfSByteInBits), Bits.ToShortBinaryString(allBitsSet));
        }

        /// <summary>
        /// <see cref="Bits.ToLongBinaryString(sbyte)"/>
        /// </summary>
        [Test]
        public void TestToLongBinaryStringSByte()
        {
            Assert.AreEqual(new string('0', Bits.SizeOfSByteInBits), Bits.ToLongBinaryString((sbyte)0));
            Assert.AreEqual(new string('0', Bits.SizeOfSByteInBits - 1) + "1", Bits.ToLongBinaryString((sbyte)1));
            Assert.AreEqual(new string('0', Bits.SizeOfSByteInBits - 8) + "10101010", Bits.ToLongBinaryString(unchecked((sbyte)0b10101010)));
            Assert.AreEqual(new string('0', Bits.SizeOfSByteInBits - 7) + "1010101", Bits.ToLongBinaryString(unchecked((sbyte)0b01010101)));

            var allBitsSet = sbyte.MinValue == default(sbyte) ? sbyte.MaxValue : unchecked((sbyte)-1);
            Assert.AreEqual(new string('1', count: Bits.SizeOfSByteInBits), Bits.ToLongBinaryString(allBitsSet));
        }

        /// <summary>
        /// Helper buffer for <see cref="GetRandomSByte"/>
        /// </summary>
        private readonly byte[] _getRandomBufferSByte = new byte[sizeof(sbyte)];

        /// <summary>
        /// Helper to generate random <see cref="sbyte"/> values for fuzz testing
        /// </summary>
        private sbyte GetRandomSByte()
        {
            this._random.Value.NextBytes(this._getRandomBufferSByte);
            sbyte value = 0;
            for (var i = 0; i < sizeof(sbyte); ++i)
            {
                value = unchecked((sbyte)((sbyte)(value << 8) & (sbyte)this._getRandomBufferSByte[i]));
            }
            return value;
        }

        
    }
}
