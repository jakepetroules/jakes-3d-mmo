namespace Petroules.Synteza.Math
{
    using System;

    /// <summary>
    /// Provides methods to convert between different information units.
    /// </summary>
    public static class InformationUnitConverter
    {
        /// <summary>
        /// The number of bits in one byte.
        /// </summary>
        public const int BitsPerByte = 8;

        /// <summary>
        /// The number of semi-nibbles in one byte.
        /// </summary>
        public const int SemiNibblesPerByte = 4;

        /// <summary>
        /// The number of nibbles in one byte.
        /// </summary>
        public const int NibblesPerByte = 2;

        /// <summary>
        /// The factor of multiplication from one unit to the next.
        /// </summary>
        public const int ConversionFactor = 1024;

        /// <summary>
        /// Converts a value in <paramref name="sourceUnit"/> to its equivalent in <paramref name="destinationUnit"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="sourceUnit">The unit associated with <paramref name="value"/>.</param>
        /// <param name="destinationUnit">The unit type to convert to.</param>
        /// <returns>The <paramref name="destinationUnit"/> equivalent of <paramref name="value"/>.</returns>
        public static decimal Convert(decimal value, InformationUnit sourceUnit, InformationUnit destinationUnit)
        {
            // First convert the source value to bytes
            decimal byteCount = 0;
            if (sourceUnit == InformationUnit.Bit)
            {
                byteCount = value / InformationUnitConverter.BitsPerByte;
            }
            else if (sourceUnit == InformationUnit.SemiNibble)
            {
                byteCount = value / InformationUnitConverter.SemiNibblesPerByte;
            }
            else if (sourceUnit == InformationUnit.Nibble)
            {
                byteCount = value / InformationUnitConverter.NibblesPerByte;
            }
            else
            {
                byteCount = value * (decimal)Math.Pow(InformationUnitConverter.ConversionFactor, (int)sourceUnit);
            }

            decimal tenativeResult = byteCount / (decimal)Math.Pow(InformationUnitConverter.ConversionFactor, Math.Max((int)InformationUnit.Byte, (int)destinationUnit));

            if (destinationUnit == InformationUnit.Bit)
            {
                tenativeResult *= InformationUnitConverter.BitsPerByte;
            }
            else if (destinationUnit == InformationUnit.SemiNibble)
            {
                tenativeResult *= InformationUnitConverter.SemiNibblesPerByte;
            }
            else if (destinationUnit == InformationUnit.Nibble)
            {
                tenativeResult *= InformationUnitConverter.NibblesPerByte;
            }

            return tenativeResult;
        }
    }
}
