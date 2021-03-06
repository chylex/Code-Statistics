﻿using System;
using System.Linq;

namespace CodeStatisticsCore.Handling.Utils{
    public static class ParseUtils{
        /// <summary>
        /// Returns the amount of characters in a string after converting leading spaces to tabs. Amount of spaces per tab has to be equal to or larger than 1.
        /// </summary>
        public static int CountCharacters(string line, int spacesPerTab = 4){
            if (spacesPerTab < 1)throw new ArgumentException("Argument must be equal to or larger than 1.", "spacesPerTab");

            int spaces = line.Length-line.SkipWhile(chr => chr == ' ').Count();
            return line.Length-spaces*(spacesPerTab-1)/spacesPerTab;
        }
    }
}
