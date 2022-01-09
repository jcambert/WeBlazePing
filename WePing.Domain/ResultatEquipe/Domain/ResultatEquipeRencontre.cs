using System.Xml.Serialization;

namespace WePing.Domain.ResultatEquipe.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeResultatEquipeRencontres
    {
        [XmlElement(ElementName = "tour")]
        public List<ResultatEquipeRencontre> Rencontres { get; set; } = new List<ResultatEquipeRencontre>();
    }
    public class ResultatEquipeRencontre
    {
        public ResultatEquipeRencontre()
        {

        }
        #region public properties
        [XmlElement(ElementName = "libelle")]
        public string Libelle { get; set; }
        [XmlElement(ElementName = "equa")]
        public string EquipeA { get; set; }
        [XmlElement(ElementName = "equb")]
        public string EquipeB { get; set; }
        [XmlElement(ElementName = "scorea")]
        public string ScoreA { get; set; }
        [XmlElement(ElementName = "scoreb")]
        public string ScoreB { get; set; }
        [XmlElement(ElementName = "lien")]
        public string Lien { get; set; }

        #endregion

    }
}
