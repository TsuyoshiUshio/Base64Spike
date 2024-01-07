using System.Collections.Frozen;
using System.Text;

namespace Base64Spike
{
    public class Base64Utils
    {
        static readonly FrozenDictionary<string, string> _sixBitsConverter = new Dictionary<string, string>()
        {
            {"000000", "A"},
            {"000001", "B"},
            {"000010", "C"},
            {"000011", "D"},
            {"000100", "E"},
            {"000101", "F"},
            {"000110", "G"},
            {"000111", "H"},
            {"001000", "I"},
            {"001001", "J"},
            {"001010", "K"},
            {"001011", "L"},
            {"001100", "M"},
            {"001101", "N"},
            {"001110", "O"},
            {"001111", "P"},
            {"010000", "Q"},
            {"010001", "R"},
            {"010010", "S"},
            {"010011", "T"},
            {"010100", "U"},
            {"010101", "V"},
            {"010110", "W"},
            {"010111", "X"},
            {"011000", "Y"},
            {"011001", "Z"},
            {"011010", "a"},
            {"011011", "b"},
            {"011100", "c"},
            {"011101", "d"},
            {"011110", "e"},
            {"011111", "f"},
            {"100000", "g"},
            {"100001", "h"},
            {"100010", "i"},
            {"100011", "j"},
            {"100100", "k"},
            {"100101", "l"},
            {"100110", "m"},
            {"100111", "n"},
            {"101000", "o"},
            {"101001", "p"},
            {"101010", "q"},
            {"101011", "r"},
            {"101100", "s"},
            {"101101", "t"},
            {"101110", "u"},
            {"101111", "v"},
            {"110000", "w"},
            {"110001", "x"},
            {"110010", "y"},
            {"110011", "z"},
            {"110100", "0"},
            {"110101", "1"},
            {"110110", "2"},
            {"110111", "3"},
            {"111000", "4"},
            {"111001", "5"},
            {"111010", "6"},
            {"111011", "7"},
            {"111100", "8"},
            {"111101", "9"},
            {"111110", "+"},
            {"111111", "/"}
        }.ToFrozenDictionary();

        public static string Encode(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            var binaryString = new List<string>();
            foreach(byte b in bytes)
            {
                binaryString.Add(Convert.ToString(b, 2).PadLeft(8, '0'));
            }

            var sixBitString = GetSixBits(binaryString);

            StringBuilder sb = new();
            GetFourLetters(sixBitString, 0, sb);
            return sb.ToString();
        }

        private static void GetFourLetters(List<string> sixBitsString, int startIndex, StringBuilder result)
        {
            int count = 4;
            if (startIndex + count - 1 < sixBitsString.Count)
            {
               var fourSixBits = sixBitsString.GetRange(startIndex, count);
                foreach(var fourSixBit in fourSixBits)
                {
                    result.Append(_sixBitsConverter[fourSixBit]);
                }
                GetFourLetters(sixBitsString, startIndex + count, result);
            } 
            else
            {
                int remains = sixBitsString.Count - startIndex;
                if (remains > 0)
                {
                    foreach(var remainsBit in sixBitsString.GetRange(startIndex, remains))
                    {
                        result.Append(_sixBitsConverter[remainsBit]);
                    }

                    int numbersToFill = count - remains;
                    for(int i = 0; i < numbersToFill; i++)
                    {
                        result.Append("=");
                    }
                }
            }
        }

        private static List<string> GetSixBits(List<string> binaryStrings)
        {
            StringBuilder builder = new StringBuilder();
            foreach(string s in binaryStrings)
            {
                builder.Append(s);
            }

            string concatedString = builder.ToString();

            int start = 0;
            var sixBitsStrings = new List<string>();
            while (true)
            {
                if (start + 5 < concatedString.Length)
                {
                    sixBitsStrings.Add(concatedString.Substring(start, 6));
                    start += 6;
                }
                else
                {
                    if (start < concatedString.Length)
                    {
                        string remains = concatedString.Substring(start);
                        string lastSixBits = remains.PadRight(6, '0');
                        sixBitsStrings.Add(lastSixBits);
                    }
                    break;
                }
            }
            return sixBitsStrings;
        }
    }
}
