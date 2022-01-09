using WePing.Domain.Equipes.Domain;

namespace WePing.Domain.Equipes.Queries
{
    public class BrowseEquipes : PagedQueryBase, IQuery<PagedResult<Equipe>>
    {
        string[] availableTypes = { "M", "F", "A", "" };
        private string _type = "A";
        public string NumClu { get; set; }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = Array.IndexOf(availableTypes, value.Trim().ToUpper()) >= 0 ? value.Trim().ToUpper() : "A";
            }
        }
    }
}

