using System.Xml.Serialization;

namespace WePing.Domain.Club.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeClubs
    {
        [XmlElement(ElementName = "club")]
        public List<Club> Clubs { get; set; } = new List<Club>();
    }

    public class Club
    {



        #region public properties

        [XmlElement(ElementName = "idclub")]
        public string Id { get; set; }

        [XmlElement(ElementName = "numero")]
        public string Numero { get; set; }

        [XmlElement(ElementName = "nom")]
        public string Nom { get; set; }

        [XmlElement(ElementName = "validation")]
        public string Validation { get; set; }

        #endregion

        #region Constructeur
        public Club()
        {


        }
        #endregion



    }
}
