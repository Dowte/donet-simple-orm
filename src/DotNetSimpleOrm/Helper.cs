using System.Text;

namespace DotNetSimpleOrm
{
    public class Helper
    {
        /**
         * OldMan to old_man
         */
        public static string ToSnakeCase(string originStr, char splitChar = '_')
        {
            var sb = new StringBuilder();
            var startOfWord = true;
            foreach (var ch in originStr)
            {
                if (char.IsUpper(ch))
                {
                    if (!startOfWord)
                    {
                        sb.Append(splitChar);
                    }
                }

                sb.Append(ch.ToString().ToLower());
                startOfWord = char.IsWhiteSpace(ch);
            }

            return sb.ToString();
        }
    }
}