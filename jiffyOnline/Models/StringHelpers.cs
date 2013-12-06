using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Configuration;


namespace jiffyOnline.Models
{
    public static class StringHelpers
    {
        public static string ImagePath  = ConfigurationManager.AppSettings["ImagePath"].ToString();
        public static IList<string> ConvertStringToList(string str)
        {
            List<string> namesList = null;

            if (!string.IsNullOrEmpty(str) && str.Contains(','))
            {
                string[] namesArray = str.Split(',');
                namesList = new List<string>(namesArray.Length);
                namesList.AddRange(namesArray);
                namesList.Reverse();
            }
            else
            {
                namesList = new List<string>(1);
                namesList.Add(str);
            }

            return namesList;
        }

        public static IList<string> ConvertStringToList(string str, char seperator)
        {
            List<string> namesList = null;

            if (!string.IsNullOrEmpty(str) && str.Contains(seperator))
            {
                string[] namesArray = str.Split(seperator);
                namesList = new List<string>(namesArray.Length);
                namesList.AddRange(namesArray);
                namesList.Reverse();
            }
            else
            {
                namesList = new List<string>(1);
                namesList.Add(str);
            }

            return namesList;
        }

        public static string ConvertByteToString(byte[] byteArray)
        {
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            string str = encoding.GetString(byteArray);
            return str;
        }

        public static string ConvertListToString(ICollection<string> list)
        {
            string[] arr = null;
            string str = string.Empty;

            if (list != null && list.Count > 0)
            {
                arr = list.ToArray();
                str = string.Join(",", arr);
            }

            return str;
        }

        public static string Substring(byte[] byteArray, int start, int lenght)
        {
            string str = ConvertByteToString(byteArray);
            return str.Substring(0, Math.Min(str.Length, 20));
        }

        public static string Substring(string value, string str)
        {
            string result = string.Empty;

            int startIndex = value.IndexOf(str) + str.Length;
            int endIndex = value.IndexOf(";", startIndex);
            int length = endIndex - startIndex;

            if (startIndex > 0)
            {
                result = value.Substring(startIndex, length);
            }

            return result;
        }

        public static bool OnlyNumberInString(string test)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b[0-9]+\b\Z");
        }

        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }

        public static string FormatDecimal(decimal? d)
        {
            string result = string.Empty;

            if (d.HasValue)
            {
                result = d.Value.ToString("#,##0.00");
            }

            return result;
        }

        public static string ConvertToString(object d)
        {
            string result = string.Empty;

            if (d != null)
            {
                result = d.ToString();
            }

            return result;
        }
    }
}