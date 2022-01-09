using System.Globalization;
using System.Xml.Serialization;

namespace WePing.Domain.Joueurs.Domain
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeJoueursClassement
    {
        [XmlElement(ElementName = "joueur")]
        public List<JoueurClassement> Joueurs { get; set; } = new List<JoueurClassement>();
    }
    public class JoueurClassement
    {

        #region public properties

        [XmlElement(ElementName = "licence")]
        public string Licence { get; set; }


        [XmlElement(ElementName = "nom")]
        public string Nom { get; set; }


        [XmlElement(ElementName = "prenom")]
        public string Prenom { get; set; }


        [XmlElement(ElementName = "nclub")]
        public string NumeroClub { get; set; }


        [XmlElement(ElementName = "club")]
        public string NomClub { get; set; }

        [XmlElement(ElementName = "natio")]
        public string Nationalite { get; set; }
        [XmlElement(ElementName = "clglob")]
        public string ClassementGlobal { get; set; }
        [XmlElement(ElementName = "point")]
        public string Points { get; set; }
        [XmlElement(ElementName = "aclglob")]
        public string AncienClassementGlobal { get; set; }
        [XmlElement(ElementName = "apoint")]
        public string AncienPoints { get; set; }
        [XmlElement(ElementName = "clast")]
        public string Classement { get; set; }
        [XmlElement(ElementName = "categ")]
        public string CategorieAge { get; set; }
        [XmlElement(ElementName = "rangreg")]
        public string RangRegional{ get; set; }
        [XmlElement(ElementName = "rangdep")]
        public string RangDepartemental { get; set; }
        [XmlElement(ElementName = "valcla")]
        public string PointsOfficiels { get; set; }
        [XmlElement(ElementName = "clpro")]
        public string PropositionClassement { get; set; }
        [XmlElement(ElementName = "valinit")]
        public string PointsDebutSaison { get; set; }
        #endregion

        public double ProgressionMensuelle => double.Parse(Points, CultureInfo.InvariantCulture) - Double.Parse(AncienPoints, CultureInfo.InvariantCulture);
        public double ProgressionSaison => double.Parse(Points, CultureInfo.InvariantCulture) - Double.Parse(PointsDebutSaison, CultureInfo.InvariantCulture);

        public (bool,string) ProgressionMensuelleToChip=>(ProgressionMensuelle >= 0, ProgressionMensuelle.ToString("N1", CultureInfo.InvariantCulture));

        public (bool,string) ProgressionSaisonToChip=>(ProgressionSaison >= 0, ProgressionSaison.ToString("N1", CultureInfo.InvariantCulture));
    }
}
