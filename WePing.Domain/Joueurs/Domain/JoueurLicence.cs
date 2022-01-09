using System.Xml.Serialization;
using System.Globalization;
namespace WePing.Domain.Joueurs.Domain
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class ListeJoueurLicence
    {
        [XmlElement(ElementName = "licence")]
        public List<JoueurLicence> Licence { get; set; } = new List<JoueurLicence>();
    }
    public class JoueurLicence
    {

        #region public properties

        [XmlElement(ElementName = "licence")]
        public string Licence { get; set; }

        [XmlElement(ElementName = "idlicence")]
        public string LicenceId { get; set; }
        [XmlElement(ElementName = "nom")]
        public string Nom { get; set; }


        [XmlElement(ElementName = "prenom")]
        public string Prenom { get; set; }


        [XmlElement(ElementName = "numclub")]
        public string NumeroClub { get; set; }


        [XmlElement(ElementName = "nomclub")]
        public string NomClub { get; set; }

        [XmlElement(ElementName = "sexe")]
        public string Sexe { get; set; }
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "certif")]
        public string CertificatMedical { get; set; }
        [XmlElement(ElementName = "validation")]
        public string DateValidationCertificatMedical { get; set; }
        [XmlElement(ElementName = "echelon")]
        public string Echelon { get; set; }
        [XmlElement(ElementName = "place")]
        public string Place{ get; set; }
        [XmlElement(ElementName = "point")]
        public string Points { get; set; }
        [XmlElement(ElementName = "cat")]
        public string CategorieAge { get; set; }
        [XmlElement(ElementName = "pointm")]
        public string PointsMensuels { get; set; }
        [XmlElement(ElementName = "apointm")]
        public string AncienPointsMensuels { get; set; }
        
        [XmlElement(ElementName = "initm")]
        public string PointsDebutSaison { get; set; }
        [XmlElement(ElementName = "mutation")]
        public string Mutation { get; set; }

        [XmlElement(ElementName = "natio")]
        public string Nationalite{ get; set; }
        [XmlElement(ElementName = "arb")]
        public string Arbitre { get; set; }
        [XmlElement(ElementName = "ja")]
        public string JugeArbitre { get; set; }
        [XmlElement(ElementName = "tech")]
        public string Technicien { get; set; }

        public double ProgressionMensuelle =>  double.Parse(PointsMensuels, CultureInfo.InvariantCulture) - Double.Parse(AncienPointsMensuels, CultureInfo.InvariantCulture);
        public double ProgressionSaison => double.Parse(PointsMensuels, CultureInfo.InvariantCulture) - Double.Parse(PointsDebutSaison, CultureInfo.InvariantCulture);

        public (bool, string) ProgressionMensuelleToChip => (ProgressionMensuelle >= 0, ProgressionMensuelle.ToString("N1", CultureInfo.InvariantCulture));

        public (bool, string) ProgressionSaisonToChip => (ProgressionSaison >= 0, ProgressionSaison.ToString("N1", CultureInfo.InvariantCulture));
        public string Diplome => $"{Arbitre} {JugeArbitre}";
        #endregion



    }
}
