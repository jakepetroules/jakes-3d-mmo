namespace MMO3D.Server
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Text;
    using System.Linq;

    /// <summary>
    /// Contains methods for the game's chat filter.
    /// </summary>
    public static class ChatFilter
    {
        /// <summary>
        /// The character used to censor offending words.
        /// </summary>
        private const char CensorCharacter = '*';

        /// <summary>
        /// List of words to be censored.
        /// </summary>
        private static StringCollection words;

        /// <summary>
        /// Initializes the Censor class with the words from the database to be censored.
        /// </summary>
        public static void Initialize()
        {
            ChatFilter.words = ChatFilter.GetCensorList();
        }

        /// <summary>
        /// Gets the censored version of a string.
        /// </summary>
        /// <param name="input">The string to censor.</param>
        /// <returns>The censored version of the string with offending characters replaced with asterisks.</returns>
        public static string CensorString(string input)
        {
            // Convert string to lower case
            input = input.ToLower(CultureInfo.CurrentCulture);

            // Replace all censored words
            for (int i = 0; i < words.Count; i++)
            {
                input = input.Replace(words[i], ChatFilter.GetAsterisks(words[i].Length));
            }

            // Convert first character to upper case
            if (input[0] >= 'a' && input[0] <= 'z')
            {
                input = input[0].ToString().ToUpper(CultureInfo.CurrentCulture) + input.Substring(1);
            }

            return input;
        }

        /// <summary>
        /// Gets a string containing the censor character repeated <code>amount</code> times.
        /// </summary>
        /// <param name="amount">The number of times the censor character occurs in the returned string.</param>
        /// <returns>See summary.</returns>
        private static string GetAsterisks(int amount)
        {
            StringBuilder builder = new StringBuilder();
            while (amount-- > 0)
            {
                builder = builder.Append(ChatFilter.CensorCharacter);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets a list of words from the database that should be censored by the chat filter.
        /// </summary>
        /// <returns>See summary.</returns>
        private static StringCollection GetCensorList()
        {
            var wordsDic = from p in GameServer.Instance.DbManager.FilteredWords select p;

            StringCollection words = new StringCollection();
            foreach (var word in wordsDic)
            {
                words.Add(word.Word);
            }

            return words;
        }
    }
}
