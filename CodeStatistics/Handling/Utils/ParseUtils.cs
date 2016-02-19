using System.Linq;

namespace CodeStatistics.Handling.Utils{
    public static class ParseUtils{
        /// <summary>
        /// Returns the amount of characters in a string after converting leading spaces to tabs.
        /// </summary>
        public static int CountCharacters(string line, int spacesPerTab = 4){
            int spaces = line.Length-line.SkipWhile(chr => chr == ' ').Count();
            return line.Length-spaces*(spacesPerTab-1)/spacesPerTab;
        }
    }
}
