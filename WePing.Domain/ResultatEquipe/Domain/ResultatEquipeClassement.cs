using System.Xml.Serialization;

namespace WePing.Domain.ResultatEquipe.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeResultatEquipeClassements
    {
        [XmlElement(ElementName = "classement")]
        public List<ResultatEquipeClassement> Classements { get; set; } = new List<ResultatEquipeClassement>();
    }
    public class ResultatEquipeClassement
    {
        public ResultatEquipeClassement()
        {

        }
        #region public properties
        [XmlElement(ElementName = "poule")]
        public string Poule { get; set; }
        [XmlElement(ElementName = "clt")]
        public string Classement { get; set; }
        [XmlElement(ElementName = "equipe")]
        public string Equipe { get; set; }
        [XmlElement(ElementName = "joue")]
        public string Joue { get; set; }
        [XmlElement(ElementName = "pts")]
        public string Points { get; set; }
        [XmlElement(ElementName = "numero")]
        public string Numero { get; set; }
        [XmlElement(ElementName = "totvic")]
        public string TotalVictoire { get; set; }
        [XmlElement(ElementName = "totdef")]
        public string TotalDefaite { get; set; }
        [XmlElement(ElementName = "idequipe")]
        public string IdEquipe { get; set; }
        [XmlElement(ElementName = "idclub")]
        public string IdClub{ get; set; }
        [XmlElement(ElementName = "vic")]
        public string Victoire { get; set; }
        [XmlElement(ElementName = "def")]
        public string Defaite { get; set; }
        [XmlElement(ElementName = "nul")]
        public string Nul { get; set; }
        [XmlElement(ElementName = "pg")]
        public string SetVictoire { get; set; }
        [XmlElement(ElementName = "pp")]
        public string SetDefaite { get; set; }

        #endregion

    }
}
