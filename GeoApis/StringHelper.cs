
namespace System
{


    public static class FastStringExtensions
    {

        // From JavaScript
        // string str = ReplaceAll("hiHihi", "hi", "hicIoA", false); System.Console.WriteLine(str);
        private static string ReplaceAll(this string str, string find, string newToken, bool ignoreCase)
        {
            int i = -1;

            if (string.IsNullOrEmpty(str))
                return str;

            find = ignoreCase ? find.ToLower() : find;

            while ((
                i = (ignoreCase ? str.ToLower() : str).IndexOf(
                    find, i >= 0 ? i + newToken.Length : 0
                )) != -1
            )
            {
                str = str.Substring(0, i) +
                    newToken +
                    str.Substring(i + find.Length);
            }

            return str;
        }

        // From JavaScript
        private static string ReplaceAll(this string str, string find, string newToken)
        {
            return ReplaceAll(str, find, newToken, false);
        }



        // Uses string instead of StringBuilder 
        public static string ReplaceSome(this string str, string find, string newToken, System.StringComparison comparison)
        {
            newToken = newToken ?? "";

            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(find) || find.Equals(newToken, comparison))
                return str;

            int foundAt = 0;
            while ((foundAt = str.IndexOf(find, foundAt, comparison)) != -1)
            {
                str = str.Remove(foundAt, find.Length).Insert(foundAt, newToken);
                foundAt += newToken.Length;
            }

            return str;
        }


        // Fast version of ReplaceSome
        // string str = ReplaceSomeMore("hiHihi", "hi", "hicIoA", System.StringComparison.InvariantCultureIgnoreCase);
        public static string ReplaceSomeMore(this string str, string find, string newToken, System.StringComparison comparisonType)
        {
            // Instead of checking inputs, return what makes sense...
            if (string.IsNullOrEmpty(str))
            {
                if (string.IsNullOrEmpty(find))
                    return newToken;

                return str;
            }
            
            if (string.IsNullOrEmpty(find))
                return str;

            // Prepare string builder for storing the processed string.
            // Note: StringBuilder has a better performance than String by 30-40%.
            System.Text.StringBuilder resultStringBuilder = new System.Text.StringBuilder(str.Length);

            // Analyze the replacement: replace or remove.
            bool isReplacementNullOrEmpty = string.IsNullOrEmpty(newToken);

            // Replace all values.
            const int valueNotFound = -1;
            int foundAt;
            int startSearchFromIndex = 0;
            while ((foundAt = str.IndexOf(find, startSearchFromIndex, comparisonType)) != valueNotFound)
            {
                // Append all characters until the found replacement.
                int charsUntilReplacment = foundAt - startSearchFromIndex;
                bool isNothingToAppend = charsUntilReplacment == 0;
                if (!isNothingToAppend)
                {
                    resultStringBuilder.Append(str, startSearchFromIndex, charsUntilReplacment);
                }

                // Process the replacement.
                if (!isReplacementNullOrEmpty)
                    resultStringBuilder.Append(newToken);

                // Prepare start index for the next search.
                // This needed to prevent infinite loop, otherwise method always start search 
                // from the start of the string. For example: if an oldValue == "EXAMPLE", newValue == "example"
                // and comparisonType == "any ignore case" will conquer to replacing:
                // "EXAMPLE" to "example" to "example" to "example" … infinite loop.
                startSearchFromIndex = foundAt + find.Length;
                if (startSearchFromIndex == str.Length)
                {
                    // It is end of the input string: no more space for the next search.
                    // The input string ends with a value that has already been replaced. 
                    // Therefore, the string builder with the result is complete and no further action is required.
                    return resultStringBuilder.ToString();
                }
            } // Whend 


            // Append the last part to the result.
            int charsUntilStringEnd = str.Length - startSearchFromIndex;
            resultStringBuilder.Append(str, startSearchFromIndex, charsUntilStringEnd);

            return resultStringBuilder.ToString();
        }


    }


}
