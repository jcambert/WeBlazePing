using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WePing.Domain.ClubDetails.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeClubdetails
    {
        [XmlElement(ElementName = "club")]
        public ClubDetail Club { get; set; } = new ClubDetail();
    }

    public class ClubDetail
    {




        #region public properties

        [XmlElement(ElementName = "idclub")]
        public string Id { get; set; }
        [XmlElement(ElementName = "numero")]
        public string Numero { get; set; }
        [XmlElement(ElementName = "nomsalle")]
        public string NomSalle { get; set; }
        [XmlElement(ElementName = "adressesalle1")]
        public string AdresseSalle1 { get; set; }
        [XmlElement(ElementName = "adressesalle2")]
        public string AdresseSalle2 { get; set; }
        [XmlElement(ElementName = "adressesalle3")]
        public string AdresseSalle3 { get; set; }
        [XmlElement(ElementName = "codepsalle")]
        public string CodePostalSalle { get; set; }
        [XmlElement(ElementName = "villesalle")]
        public string VilleSalle { get; set; }
        [XmlElement(ElementName = "web")]
        public string Web { get; set; }
        [XmlElement(ElementName = "nomcor")]
        public string NomCorrespondant { get; set; }
        [XmlElement(ElementName = "prenomcor")]
        public string PrenomCorrespondant { get; set; }
        [XmlElement(ElementName = "mailcor")]
        public string MailCorrespondant { get; set; }
        [XmlElement(ElementName = "telcor")]
        public string TelephoneCorrespondant { get; set; }
        [XmlElement(ElementName = "latitude")]
        public string Latitude { get; set; }
        [XmlElement(ElementName = "longitude")]
        public string Longitude { get; set; }



        #endregion


        #region Constructeur
        public ClubDetail()

        {

        }
        #endregion



    }
}
