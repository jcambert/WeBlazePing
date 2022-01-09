using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePing.Domain.ClubDetails.Dto
{
    public sealed class ClubDetailDto
    {
        #region public properties


        public string Id { get; set; }

        public string Numero { get; set; }

        public string NomSalle { get; set; }

        public string AdresseSalle1 { get; set; }

        public string AdresseSalle2 { get; set; }

        public string AdresseSalle3 { get; set; }

        public string CodePostalSalle { get; set; }

        public string VilleSalle { get; set; }

        public string Web { get; set; }

        public string NomCorrespondant { get; set; }

        public string PrenomCorrespondant { get; set; }

        public string MailCorrespondant { get; set; }

        public string TelephoneCorrespondant { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }



        #endregion
    }
}
