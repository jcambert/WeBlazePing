using WePing.Domain.ResultatEquipe.Domain;

namespace WePing.Domain.ResultatEquipe.Queries
{
    public class BrowseResultatEquipeClassements : PagedQueryBase, IQuery<PagedResult<ResultatEquipeClassement>>
    {
        public string Action { get; } = "classement";

        public string Auto { get; } = "1";

        [NoneCase()]
        public string D1 { get; set; }

        public string Cx_poule { get; set; }
    }
}
