using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;
using System.Text;

namespace WePing.Service.Spid
{
    internal static class CryptoExtensions
    {
        internal static String CreateSHA1Hash(this string text, string key)
        {
            UTF8Encoding encoding = new UTF8Encoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA1 hash = new HMACSHA1(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        internal static string InternalAsQueryString(this SpidAuthenticatorBuilder builder, IDictionary<string, string> opts = null)
        {
            var dico = new Dictionary<string, string>(opts ?? new Dictionary<string,string>());
            var auth = builder.Build();
            dico["id"] = builder.Options.AppId;
            dico["serie"] = builder.Options.Serie;
            dico["tm"] = auth.Tm;
            dico["tmc"] = auth.Tmc;
            var result = QueryHelpers.AddQueryString("", dico);
            return result;
        }
    }
}
