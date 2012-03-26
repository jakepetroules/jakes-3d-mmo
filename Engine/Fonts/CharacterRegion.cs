namespace MMO3D.Engine
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Encapsulates the starting and ending characters in a region of characters.
    /// </summary>
    public sealed class CharacterRegion
    {
        /// <summary>
        /// Initializes a new instance of the CharacterRegion class.
        /// </summary>
        public CharacterRegion()
        {
            this.Start = ' ';
            this.End = '~';
        }

        /// <summary>
        /// Initializes a new instance of the CharacterRegion class.
        /// </summary>
        /// <param name="start">The first character in the range.</param>
        /// <param name="end">The last character in the range.</param>
        public CharacterRegion(char start, char end)
        {
            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Gets or sets the first character in the range. The default is SPACE (U+0020, #32).
        /// </summary>
        /// <value>See summary.</value>
        public char Start
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last character in the range. The default is TILDE (U+007E, #126).
        /// </summary>
        /// <value>See summary.</value>
        public char End
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the data of the CharacterRegion as a string of XML.
        /// </summary>
        /// <returns>See summary.</returns>
        public string ToXmlString()
        {
            return string.Format(CultureInfo.InvariantCulture, "<CharacterRegion><Start>&#{0};</Start><End>&#{1};</End></CharacterRegion>", Convert.ToInt32(this.Start), Convert.ToInt32(this.End));
        }
    }
}
