using System.Xml.Serialization;

namespace WePing.Domain.ResultatEquipe.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeResultatEquipePoules
    {
        [XmlElement(ElementName = "tour")]
        public List<ResultatEquipePoule> Poules { get; set; } = new List<ResultatEquipePoule>();
    }
    public class ResultatEquipePoule
    {
        public ResultatEquipePoule()
        {

        }
        #region public properties
        [XmlElement(ElementName = "libelle")]
        public string Libelle { get; set; }
        [XmlElement(ElementName = "lien")]
        public string Lien { get; set; }

        #endregion

    }
}
