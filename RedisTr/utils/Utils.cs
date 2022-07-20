using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisTr.utils
{
    public static class Utils
    {
        public static string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789йцукенгшщзхъдлорпавыфячсмить";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return  new String(stringChars);
        }
    }
}
