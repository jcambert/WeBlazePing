using System.Xml.Serialization;

namespace WePing.Domain.Equipes.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeEquipes
    {
        [XmlElement(ElementName = "equipe")]
        public List<Equipe> Equipes { get; set; } = new List<Equipe>();
    }
    public class Equipe
    {
        public Equipe()
        {

        }
        #region public properties
        [XmlElement(ElementName = "libequipe")]
        public string Nom { get; set; }
        [XmlElement(ElementName = "libdivision")]
        public string Division { get; set; }
        [XmlElement(ElementName = "idepr")]
        public string Id { get; set; }
        [XmlElement(ElementName = "libepr")]
        public string Epreuve { get; set; }
        [XmlElement(ElementName = "liendivision")]
        public string Lien { get; set; }

        #endregion

    }
}
