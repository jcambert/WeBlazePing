using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WePing.Domain.ClubDetails.Dto;

namespace WePing.Domain.ClubDetails.Queries
{
    public class GetClubDetail : IQuery
    {
        public string Club { get; set; }
    }
}
