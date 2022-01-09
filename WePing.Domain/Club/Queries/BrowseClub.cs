using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WePing.Domain.Club.Dto;

namespace WePing.Domain.Club.Queries
{
    public class BrowseClubs : PagedQueryBase, IQuery<PagedResult<ClubDto>>
    {
        [Default]
        [Description("Département")]
        public string Dep { get; set; }

        public string Code { get; set; }

        public string Ville { get; set; }

        public string Numero { get; set; }
    }
}
