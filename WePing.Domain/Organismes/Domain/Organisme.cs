using System.Xml.Serialization;

namespace WePing.Domain.Organismes.Domain
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeOrganismes
    {
        [XmlElement(ElementName = "organisme")]
        public List<Organisme> Organismes { get; init; } = new List<Organisme>();
    }

    public class Organisme
    {
        public Organisme()
        {

        }
        #region public properties

        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "libelle")]
        public string Libelle { get; set; }
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }

        #endregion


    }
}
