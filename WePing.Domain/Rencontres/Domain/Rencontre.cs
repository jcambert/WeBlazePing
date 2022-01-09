using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace WePing.Domain.Rencontres.Domain
{

    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRoot(ElementName = "liste")]
    public class Rencontre
    {

        [XmlElement(ElementName = "resultat")]
        public List<RencontreResultat> Resultat { get; init; } = new List<RencontreResultat>();
        [XmlElement(ElementName = "joueur")]
        public List<RencontreJoueur> Joueurs { get; init; } = new List<RencontreJoueur>();
        [XmlElement(ElementName = "partie")]
        public List<RencontrePartie> Parties { get; init; } = new List<RencontrePartie>();

        public (int, int) TotalPoints
        {
            get
            {
                int pta = 0, ptb = 0;
                foreach (var joueur in Joueurs)
                {
                    pta += joueur.PointsA;
                    ptb += joueur.PointsB;
                }
                return (pta, ptb);
            }
        }
        public ((int,int),(int,int)) SetsGagnesPerdu
        {
            get
            {
                int pga=0, ppa=0;
                foreach (var partie in Parties)
                {
                    pga += partie.Sets.Count(s => s > 0);
                    ppa += partie.Sets.Count(s => s < 0);

                }
                return ((pga, ppa), (ppa, pga));
            }
        }

    }

    public class RencontreResultat
    {
        public RencontreResultat()
        {

        }
        #region public properties

        [XmlElement(ElementName = "equa")]
        public string EquipeA { get; set; }
        [XmlElement(ElementName = "equb")]
        public string EquipeB { get; set; }
        [XmlElement(ElementName = "resa")]
        public string ResultatA { get; set; }
        [XmlElement(ElementName = "resb")]
        public string ResultatB { get; set; }

        #endregion

        public int ScoreA
        {
            get
            {
                if (Int32.TryParse(ResultatA, out int score))
                    return score;
                return 0;
            }
        }
        public int ScoreB
        {
            get
            {
                if (Int32.TryParse(ResultatB, out int score))
                    return score;
                return 0;
            }
        }
    }

    public class RencontreJoueur
    {
        private string _pointsA, _pointsB;
        const string _pattern = @"(?<sexe>M|F)\s+(?<points>\d+)";
        private readonly Regex _re;
        public RencontreJoueur()
        {
            _re = new Regex(_pattern, RegexOptions.IgnoreCase);
        }
        #region public properties

        [XmlElement(ElementName = "xja")]
        public string JoueurA { get; set; }
        [XmlElement(ElementName = "xjb")]
        public string JoueurB { get; set; }
        [XmlElement(ElementName = "xca")]
        public string Xca
        {
            get => _pointsA;
            set
            {
                var matches = _re.Matches(value);
                if (matches.Count == 1)
                {
                    if (Int32.TryParse(matches[0].Groups["points"].Value, out var pta))
                        PointsA = pta;
                    SexeA = matches[0].Groups["sexe"].Value;
                }
                _pointsA = value;
            }
        }
        [XmlElement(ElementName = "xcb")]
        public string Xcb
        {
            get => _pointsB;
            set
            {
                var matches = _re.Matches(value);
                if (matches.Count == 1)
                {
                    if (Int32.TryParse(matches[0].Groups["points"].Value, out var ptb))
                        PointsB = ptb;
                    SexeB = matches[0].Groups["sexe"].Value;
                }
                _pointsB = value;
            }
        }
        [XmlIgnore]
        public int PointsA { get; private set; }
        [XmlIgnore]
        public int PointsB { get; private set; }
        [XmlIgnore]
        public string SexeA { get; private set; }
        [XmlIgnore]
        public string SexeB { get; private set; }
        [XmlIgnore]
        public double PointsGagnesPerdusA { get; set; } = 0;
        [XmlIgnore]
        public double PointsGagnesPerdusB { get; set; } = 0;
        #endregion


    }

    public class RencontrePartie
    {
        int[] _sets = new int[0];
        string _detail;
        public RencontrePartie()
        {

        }
        #region public properties

        [XmlElement(ElementName = "ja")]
        public string JoueurA { get; set; }
        [XmlElement(ElementName = "jb")]
        public string JoueurB { get; set; }
        [XmlElement(ElementName = "scorea")]
        public string ScoreA { get; set; }
        [XmlElement(ElementName = "scoreb")]
        public string ScoreB { get; set; }
        [XmlElement(ElementName = "detail")]
        public string Detail
        {
            get => _detail;
            set
            {

                var values = value.Split(' ');
                List<int> temp = new List<int>();
                foreach (var item in values)
                {
                    if (Int32.TryParse(item, out var v))
                        temp.Add(v);
                }
                _sets = temp.ToArray();
                _detail = value;
            }
        }

        [XmlIgnore]
        public int[] Sets => _sets;

        [XmlIgnore]
        public double PointsGagnesPerdusA { get; set; }

        [XmlIgnore]
        public double PointsGagnesPerdusB { get; set; }

        [XmlIgnore]
        public bool IsDouble { get; set; } = false;
        #endregion


    }
}
