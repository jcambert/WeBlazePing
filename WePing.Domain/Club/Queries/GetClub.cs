using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePing.Domain.Club.Queries
{
    public class GetClub : IQuery
    {
        public string Numero { get; set; }
    }
}
