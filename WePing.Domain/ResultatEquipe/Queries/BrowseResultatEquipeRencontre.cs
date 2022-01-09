using WePing.Domain.ResultatEquipe.Domain;

namespace WePing.Domain.ResultatEquipe.Queries
{
    public class BrowseResultatEquipeRencontres : PagedQueryBase, IQuery<PagedResult<ResultatEquipeRencontre>>
    {
        public string Action { get; } = "";

        public string Auto { get; } = "1";
        [NoneCase]
        public string D1 { get; set; }

        public string Cx_poule { get; set; }
    }
}
