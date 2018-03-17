//
// AUTO-GENERATED
//
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitwise
{
    public static partial class Bits
    {
        /// <summary>
        /// Size of the <see cref="ushort"/> type in bits
        /// </summary>
        internal const int SizeOfUInt16InBits = sizeof(ushort) * 8;

        /// <summary>
        /// Determines whether <paramref name="value"/> has any of the same bits set as <paramref name="flags"/>
        /// </summary>
        public static bool HasAnyFlag(this ushort value, ushort flags) => (value & flags) != 0;

        /// <summary>
        /// Determines whether <paramref name="value"/> has all of the bits set that are set in <paramref name="flags"/>
        /// </summary>
        public static bool HasAllFlags(this ushort value, ushort flags) => (value & flags) == flags;

        /// <summary>
        /// Determines whether the <paramref name="index"/>th bit is set in <paramref name="value"/>
        /// </summary>
        public static bool GetBit(this ushort value, int index)
        {
            if ((index & ~(SizeOfUInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return value.HasAnyFlag((ushort)(((ushort)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit set
        /// </summary>
        public static ushort SetBit(this ushort value, int index)
        {
            if ((index & ~(SizeOfUInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ushort)(value | (ushort)(((ushort)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th bit cleared
        /// </summary>
        public static ushort ClearBit(this ushort value, int index)
        {
            if ((index & ~(SizeOfUInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ushort)(value & unchecked((ushort)~(((ushort)1) << index)));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the <paramref name="index"/>th flipped
        /// </summary>
        public static ushort FlipBit(this ushort value, int index)
        {
            if ((index & ~(SizeOfUInt16InBits - 1)) != 0) { ThrowIndexOutOfRange(); }

            return (ushort)(value ^ (ushort)(((ushort)1) << index));
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the least significant bit cleared
        /// </summary>
        public static ushort ClearLeastSignificantBit(ushort value) => (ushort)(value & unchecked(value - 1));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the least significant bit
        /// </summary>
        public static ushort ClearAllButLeastSignificantBit(ushort value) => (ushort)(value & unchecked((ushort)0 - value));

        /// <summary>
        /// Returns <paramref name="value"/> with all bits cleared EXCEPT the most significant bit
        /// </summary>
        [MemberFor(typeof(ushort))]
        public static ushort ClearAllButMostSignificantBit(ushort value)
        {
            // the idea here is to steadily set all bits less significant than the most significant bit,
            // and then follow up by clearing them all. See https://stackoverflow.com/questions/28846601/java-integer-highestonebit-in-c-sharp

            value = (ushort)(value | (value >> 1));
            value = (ushort)(value | (value >> 2));
            value = (ushort)(value | (value >> 4));

            // to simplify codegen for smaller integral types, we fork on sizeof().
            // The compiler will remove these branches so that no additional inefficiency
            // is incurred
#pragma warning disable 0162
            if (sizeof(ushort) > 1)
            {
                value = (ushort)(value | (value >> 8));
                if (sizeof(ushort) > 2)
                {
                    value = (ushort)(value | (value >> 16));
                    if (sizeof(ushort) > 4)
                    {
                        value = (ushort)(value | (value >> 32));
                    }
                }
            }
#pragma warning restore 0162

            return (ushort)(value - (ushort)(value >> 1));
        }

        
    }
}
