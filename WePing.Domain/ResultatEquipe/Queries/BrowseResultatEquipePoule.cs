using WePing.Domain.ResultatEquipe.Domain;

namespace WePing.Domain.ResultatEquipe.Queries
{
    public class BrowseResultatEquipePoules : PagedQueryBase, IQuery<PagedResult<ResultatEquipePoule>>
    {
        public string Action { get; } = "poule";

        public string Auto { get; } = "1";
        [NoneCase]
        public string D1 { get; set; }

        public string Cx_poule { get; set; }
    }
}
