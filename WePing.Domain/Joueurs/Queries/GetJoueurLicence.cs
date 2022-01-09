using WePing.Domain.Joueurs.Domain;

namespace WePing.Domain.Joueurs.Queries
{
    public class GetJoueurLicence :IQuery
    {
        
        public string Licence { get; set; }

        public string Club { get; set; }

    }
   
}
