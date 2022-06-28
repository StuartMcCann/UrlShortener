using System;
using System.Linq;

namespace UrlShorterner.Helpers
{
    public static class EncodingHelper
    {
        public static readonly string AlphaNumericSequence = "abcdefghijklmnopqrstuvwxyz0123456789";
        public static readonly int Base = AlphaNumericSequence.Length;

        /*
         * Method to encode shortened url subdirectory leveraging a bijective function using letters of alphabet 
         * and base 10 numbers. More info: https://stackoverflow.com/questions/742013/how-do-i-create-a-url-shortener
         */
        public static string Encode(int id)
        {
            var encode = string.Empty;

            while (id > 0)
            {
                encode += AlphaNumericSequence[id % Base];
                id = id / Base;
            }

            return string.Join(string.Empty, encode.Reverse());
        }

        /*
        * Method to decode urls encoded using bijective function 
        */
        public static int Decode(string stringToDecode)
        {
            var id = 0;

            foreach (var character in stringToDecode)
            {
                id = (id * Base) + AlphaNumericSequence.IndexOf(character);
            }

            return id;
        }

        public static string Base64Encode(int id)
        {
            var plainTextBytes = BitConverter.GetBytes(id);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static int Base64Decode(string stringToDecode)
        {
            return BitConverter.ToInt32(Convert.FromBase64String(stringToDecode));
        }

    }
}
