using WePing.Domain.Joueurs.Domain;

namespace WePing.Domain.Joueurs.Queries
{
    public class BrowseJoueur : PagedQueryBase, IQuery<PagedResult<Joueur>>
    {
        public string Club { get; set; }
        public string Licence { get; set; }
      
        public string Nom { get; set; }
       
        public string Prenom { get; set; }
        
        public string Valid { get; set; } = "O";
    }
}
