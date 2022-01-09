
namespace WePing.Gir;

public class ModeleRencontre : BaseEntity
{

    public ModeleRencontre()
    {

    }

    public string Reference { get; set; }
    public string Type { get; set; }
    public int NombreDejoueur => Parties.Select(p => p.JoueurA).Where(p => !string.IsNullOrEmpty(p)).Distinct().Count();
    public int NombreMancheGagnante { get; set; } = 3;
    public int NombreDeGroupe { get; set; } = 1;

    public int NombreDePartie => Parties.Count();

    public bool ScoreAquis { get; set; }

    public int NombreDeDouble => Parties.Count(p => p.EstDouble);

    public bool ParticipationUniqueAuDouble { get; set; } = true;

    public int NombreRemplacant { get; set; } = 0;

    public int AutoriseRemplacement { get; set; }

    public string Ventilation
    {
        get
        {
            List<string> sb = new();
            int _double = 1;
            foreach (var partie in Parties)
            {

                sb.Append(partie.EstDouble ? String.Format(partie.ToString(), _double++) : partie.ToString());
            }
            return string.Join("-", sb);
        }
    }

    //public SortedList<int, ModeleRencontrePartie> Parties { get; init; } = new SortedList<int, ModeleRencontrePartie>();
    public List<ModeleRencontrePartie> Parties { get; init; } = new List<ModeleRencontrePartie>();
}

public class ModeleRencontrePartie : BaseEntity
{
    public ModeleRencontrePartie()
    {

    }
    public string JoueurA { get; set; }
    public string JoueurB { get; set; }
    public bool EstDouble { get; set; } = false;

    public override string ToString() => !EstDouble ? $"{JoueurA}{JoueurB}" : "double{0}";

}
