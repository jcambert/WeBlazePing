using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WePing.Domain;
using WePing.Domain.Club.Domain;
using WePing.Domain.Club.Queries;
using WePing.Domain.ClubDetails.Domain;
using WePing.Domain.ClubDetails.Queries;
using WePing.Domain.Equipes.Domain;
using WePing.Domain.Equipes.Queries;
using WePing.Domain.Joueurs.Domain;
using WePing.Domain.Joueurs.Queries;
using WePing.Domain.Organismes.Domain;
using WePing.Domain.Organismes.Queries;
using WePing.Domain.Rencontres.Domain;
using WePing.Domain.Rencontres.Queries;
using WePing.Domain.ResultatEquipe.Domain;
using WePing.Domain.ResultatEquipe.Queries;

namespace WePing.Service.Spid
{
    public interface IQueryExecutor:IDisposable
    {
        Task<HttpContent> ExecuteAsync<TQuery>(TQuery query, string api,CancellationToken cancellation);
    }
    public class QueryExecutor : IQueryExecutor
    {
        private bool disposedValue;

        protected HttpClient Client { get; }
        protected ILogger<QueryExecutor> Logger { get; }
        protected SpidOptions Options { get; }
        protected ISpidAuthenticatorBuilder Auth { get; }
        public QueryExecutor(IHttpClientFactory httpFactory, IOptions<SpidOptions> options, ISpidAuthenticatorBuilder auth, ILogger<QueryExecutor> logger)
        {
            this.Options = options.Value;
            this.Logger = logger;
            this.Auth = auth;
            this.Client = httpFactory.CreateClient(SpidOptions.SPID);
        }
        public async Task<HttpContent> ExecuteAsync<TQuery>(TQuery query, string api, CancellationToken cancellation)
        {

            var opt = query.ToDictionnary();
            var url = string.Format(Options.Url, Options.Api[api]) + Auth.AsQueryString(opt);
            Logger.LogInformation($"Request Spid:{url}");
            var resp = await Client.GetAsync(url,cancellation).ConfigureAwait(false);
            return resp.Content;

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Logger.LogInformation($"Disposing {nameof(QueryExecutor)}");
                    // TODO: supprimer l'état managé (objets managés)
                    Client.Dispose();
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~QueryExecutor()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
    public interface ISpidRequest
    {
        Task<ClubDetail> GetClubDetailsAsync(GetClubDetail query, CancellationToken cancellation = default);
        Task<List<Club>> GetClubsAsync(BrowseClubs query, CancellationToken cancellation = default);

        Task<List<Equipe>> GetEquipesAsync(BrowseEquipes query, CancellationToken cancellation = default);
        Task<List<Organisme>> GetOrganismesAsync(BrowseOrganismes query, CancellationToken cancellation = default);
        Task<List<ResultatEquipeClassement>> GetResultatEquipeClassementAsync(BrowseResultatEquipeClassements query, CancellationToken cancellation = default);
        Task<List<ResultatEquipePoule>> GetResultatEquipePouleAsync(BrowseResultatEquipePoules query, CancellationToken cancellation = default);
        Task<List<ResultatEquipeRencontre>> GetResultatEquipeRencontreAsync(BrowseResultatEquipeRencontres query, CancellationToken cancellation = default);
        Task<List<Joueur>> GetJoueursLicenceAsync(BrowseJoueur query, CancellationToken cancellation = default);
        Task<List<Joueur>> GetJoueursClassementAsync(BrowseJoueur query, CancellationToken cancellation = default);

        Task<JoueurClassement> GetJoueurDetailClassementAsync(GetJoueurLicence query, CancellationToken cancellation = default);
        Task<JoueurLicence> GetJoueurDetailLicenceAsync(GetJoueurLicence query, CancellationToken cancellation = default);
        Task<List<JoueurLicence>> GetJoueursDetailLicenceAsync(GetJoueurLicence query, CancellationToken cancellation = default);
        Task<Rencontre> GetDetailRencontreAsync(GetRencontre query, CancellationToken cancellation = default);
    }
    internal sealed class SpidRequest:ISpidRequest,IDisposable
    {
        private bool disposedValue;

        public SpidRequest(IQueryExecutor queryExecutor, ILogger<SpidRequest> logger)
        {

            // this.Options = options.Value;
            this.QueryExecutor = queryExecutor;
            this.Logger = logger;
            //this.Auth = auth;
           //this.Client = httpFactory.CreateClient(SpidOptions.SPID);
            
        }
       // private HttpClient Client { get; }
        
      //  public SpidOptions Options { get; }
        private IQueryExecutor QueryExecutor { get; }
        public ILogger<SpidRequest> Logger { get; }
       // public ISpidAuthenticatorBuilder Auth { get; }

        private async Task<byte[]> ExecuteAsync<TQuery>(TQuery query, string api, CancellationToken cancellation)
        {
            var content = await QueryExecutor.ExecuteAsync(query, api,cancellation).ConfigureAwait(false);
            Logger.LogInformation( await content.ReadAsStringAsync());
            return await content.ReadAsByteArrayAsync();
        }

        public async Task< ClubDetail> GetClubDetailsAsync(GetClubDetail query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.CLUB_DETAIL, cancellation);
            var res= data.Deserialize<ListeClubdetails>().Club;
            Logger.LogInformation($"{nameof(GetClubDetailsAsync)} executed");
            if (res.Numero == null)
                return null;
            return res;
        }
        public async Task<List<Club>> GetClubsAsync(BrowseClubs query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.CLUB_LISTE, cancellation);
            var res= data.Deserialize<ListeClubs>().Clubs;
            Logger.LogInformation($"{nameof(GetClubsAsync)} executed");
            return res;
        }

        public async Task<List<Equipe>> GetEquipesAsync(BrowseEquipes query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.EQUIPES, cancellation);
            var res = data.Deserialize<ListeEquipes>().Equipes;
            Logger.LogInformation($"{nameof(GetEquipesAsync)} executed");
            return res;
        }

        public async Task<List<Organisme>> GetOrganismesAsync(BrowseOrganismes query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.ORGANISMES, cancellation);
            var res = data.Deserialize<ListeOrganismes>().Organismes;
            Logger.LogInformation($"{nameof(GetOrganismesAsync)} executed");
            return res;
        }
        public async Task<List<ResultatEquipeClassement>> GetResultatEquipeClassementAsync(BrowseResultatEquipeClassements query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.RESULTAT_EQUIPE_CLASSEMENTS, cancellation);
            var res = data.Deserialize<ListeResultatEquipeClassements>().Classements;
            Logger.LogInformation($"{nameof(GetResultatEquipeClassementAsync)} executed");
            return res;
        }
        public async Task<List<ResultatEquipePoule>> GetResultatEquipePouleAsync(BrowseResultatEquipePoules query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.RESULTAT_EQUIPE_POULES, cancellation);
            var res = data.Deserialize<ListeResultatEquipePoules>().Poules;
            Logger.LogInformation($"{nameof(GetResultatEquipePouleAsync)} executed");
            return res;
        }
        public async Task<List<ResultatEquipeRencontre>> GetResultatEquipeRencontreAsync(BrowseResultatEquipeRencontres query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.RESULTAT_EQUIPE_RENCONTRES, cancellation);
            var res = data.Deserialize<ListeResultatEquipeRencontres>().Rencontres;
            Logger.LogInformation($"{nameof(GetResultatEquipeRencontreAsync)} executed");
            return res;
        }
        public async Task<List<Joueur>> GetJoueursLicenceAsync(BrowseJoueur query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.JOUEURS_LICENCE, cancellation);
            var res = data.Deserialize<ListeJoueurs>().Joueurs;
            Logger.LogInformation($"{nameof(GetJoueursLicenceAsync)} executed");
            return res;
        }
        public async Task<List<Joueur>> GetJoueursClassementAsync(BrowseJoueur query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.JOUEURS_CLASSEMENT, cancellation);
            var res = data.Deserialize<ListeJoueurs>().Joueurs;
            Logger.LogInformation($"{nameof(GetJoueursClassementAsync)} executed");
            return res;
        }
        public async Task<JoueurClassement> GetJoueurDetailClassementAsync(GetJoueurLicence query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.JOUEUR_DETAIL_CLASSEMENT, cancellation);
            var res = data.Deserialize<ListeJoueursClassement>().Joueurs;
            Logger.LogInformation($"{nameof(GetJoueurDetailClassementAsync)} executed");
            return res.FirstOrDefault();
        }
        public async Task<JoueurLicence> GetJoueurDetailLicenceAsync(GetJoueurLicence query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.JOUEUR_DETAIL_LICENCE, cancellation).ConfigureAwait(false);
            var res = data.Deserialize<ListeJoueurLicence>().Licence;
            Logger.LogInformation($"{nameof(GetJoueurDetailLicenceAsync)} executed");
            return res.FirstOrDefault();
        }
        public async Task<List<JoueurLicence>> GetJoueursDetailLicenceAsync(GetJoueurLicence query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.JOUEUR_DETAIL_LICENCE, cancellation);
            var res = data.Deserialize<ListeJoueurLicence>().Licence;
            Logger.LogInformation($"{nameof(GetJoueurDetailLicenceAsync)} executed");
            return res;
        }
        public async Task<Rencontre> GetDetailRencontreAsync(GetRencontre query, CancellationToken cancellation = default)
        {
            var data = await ExecuteAsync(query, SpidOptions.RENCONTRE_DETAIL, cancellation);
            var res = data.Deserialize<Rencontre>();
            Logger.LogInformation($"{nameof(GetDetailRencontreAsync)} executed");
            return res;
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Logger.LogInformation($"Disposing {nameof(SpidRequest)}");
                    // TODO: supprimer l'état managé (objets managés)
                    QueryExecutor?.Dispose();
                }

                // TODO: libérer les ressources non managées (objets non managés) et substituer le finaliseur
                // TODO: affecter aux grands champs une valeur null
                disposedValue = true;
            }
        }

        // // TODO: substituer le finaliseur uniquement si 'Dispose(bool disposing)' a du code pour libérer les ressources non managées
        // ~Request()
        // {
        //     // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ne changez pas ce code. Placez le code de nettoyage dans la méthode 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    internal static class RequestExtension
    {
        private static Dictionary<object, Dictionary<string, string>> _cache = new Dictionary<object,  Dictionary<string, string>>();
        
        private static Dictionary<Type, Func<string, string>> _stringConverter = new Dictionary<Type, Func<string, string>>()
        {
            {typeof(UpperCaseAttribute),new UpperCaseAttribute().Convert },
            {typeof(LowerCaseAttribute),new LowerCaseAttribute().Convert },
            {typeof(NoneCaseAttribute),new NoneCaseAttribute().Convert },
        };
        public static Dictionary<string, string> ToDictionnary(this object o)
        {
            if (_cache.ContainsKey(o))
                return _cache[0];
            var result = new Dictionary<string, string>();
            var props = o.GetType().GetProperties().Where(p=>!p.CustomAttributes.Any(a=>typeof(IgnoreAttribute).IsAssignableFrom(a.AttributeType)));
            props.ToList().ForEach(p =>
            {

                var type = p.CustomAttributes.Where(attr => typeof(StringConverterAttribute).IsAssignableFrom(attr.AttributeType)).FirstOrDefault()?.AttributeType ?? typeof(LowerCaseAttribute);
                string key = _stringConverter[type](p.Name);
                string value = p.GetGetMethod().Invoke(o, null)?.ToString();
                if (value != null)
                    result[key] = value;
            });
            _cache[o] = result;
            return result;
        }
    }
}
