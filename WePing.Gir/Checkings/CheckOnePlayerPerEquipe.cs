using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WePing.Domain.Joueurs.Domain;
using WePing.Service.Spid;

namespace WePing.Gir.Checkings
{
    public record ErrorMessage(bool Resultat, string Message);
    public interface ICheck
    {

    }

    public interface ITry
    {
    }
    internal abstract class BaseCheck : ICheck
    {
        public BaseCheck(IOptions<GirOptions> options,ILogger<BaseCheck> logger)
        {
            this.Options = options.Value;
            this.Logger = logger;
        }

        public GirOptions Options { get; }
        public ILogger<BaseCheck> Logger { get; }

        protected string GetMessage(string key,string defaultMessage=null)
        {
            if(Options.Messages.TryGetValue(key,out var res))
                return res;
            return defaultMessage ?? key;
        }
    }
    internal class CheckUniquePlayerPerEquipe:ICheck
    {
        public Task<ErrorMessage> Check(Rencontre rencontre)
        {
            
            return Task.Run(()=> new ErrorMessage(true,String.Empty));
        }
    }

    public interface ITryAddJoueur : ITry
    {
        Task<string> TryAddAsync(Rencontre rencontre, string licence);
        string TryAdd(Rencontre rencontre, JoueurLicence joueur);
    }
    internal class TryAddJoueurIntoEquipe : ITryAddJoueur
    {
        public TryAddJoueurIntoEquipe(ISpidRequest request)
        {
            this.Request = request;
        }

        public ISpidRequest Request { get; }

        public async Task<string> TryAddAsync(Rencontre rencontre, string licence)
        {
            if (rencontre.Joueurs.Any(j => j.Licence == licence))
                return "Already exist";
            var joueur=await Request.GetJoueurDetailLicenceAsync(new Domain.Joueurs.Queries.GetJoueurLicence() { Licence = licence });
            if (joueur == null)
                return "does not exist";
            return String.Empty;
        }

        public string TryAdd(Rencontre rencontre, JoueurLicence joueur)
        {
            if (joueur == null)
                return "does not exist";
            if (rencontre.Joueurs.Any(j => j.Licence == joueur.Licence))
                return "Already exist";
            return String.Empty;

        }
    }
}
