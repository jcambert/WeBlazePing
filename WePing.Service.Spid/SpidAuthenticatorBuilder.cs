using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WePing.Service.Spid
{
    public record SpidAuthOption(string AppId, string Password, string Serie, string Key);
    public record SpidAuthenticator(string Tm, string Tmc);
    public class SpidAuthenticatorBuilder : ISpidAuthenticatorBuilder
    {
        private readonly SpidAuthOption _options;

        public SpidAuthOption Options => _options;

        public SpidAuthenticatorBuilder(string appid, string password, string serie)
        {
            this._options = new SpidAuthOption(appid, password, serie, CreateKey(password));
        }
        private string CreateKey(string cle)
        {
            var ccle = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(cle));
            return BitConverter.ToString(ccle).Replace("-", string.Empty).ToLower();
        }



        public SpidAuthenticator Build()
        {
            var tm = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            return new SpidAuthenticator(tm, tm.CreateSHA1Hash(_options.Key));
        }

        public string AsQueryString(IDictionary<string, string> opts = null)
        => this.InternalAsQueryString(opts);

    }

    public interface ISpidAuthenticatorBuilder
    {
        SpidAuthenticator Build();
        SpidAuthOption Options { get; }
        string AsQueryString(IDictionary<string, string> opts = null);
    }
}
