using WePing.Domain.Organismes.Domain;

namespace WePing.Domain.Organismes.Queries
{
    public class BrowseOrganismes : PagedQueryBase, IQuery<PagedResult<Organisme>>
    {
        public string Type { get; set; }
    }
}
