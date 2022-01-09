namespace WePing.Gir;

public class Rencontre : BaseEntity
{

    public ModeleRencontre Modele { get; set; }
    public ICollection<Joueur> Joueurs { get; set; } = new List<Joueur>();

    public List<Partie> Parties { get; set; } = new List<Partie>();

    public int NombreDePartie { get; set; }

    public bool ScoreAquis { get; set; }

    public bool Locked { get; set; }

    public DateTime? LockOn { get; set; }

    public string LockBy { get; set; }

    public string EquipeDomicile { get; set; }
    public string EquipeAdverse { get; set; }


    public bool EstPrete { get; set; }



    public int ScoreA => Parties.Where(p => p.JoueurAGagne).Count();

    public int ScoreB => Parties.Where(p => p.JoueurBGagne).Count();
}




public class Joueur : BaseEntity
{
    public string Ordre { get; set; }
    public bool Absent { get; set; }
    public string Licence { get; set; }
    public string NumeroClub { get; set; }
    public string Nom { get; set; }

    public string Prenom { get; set; }

    public double Points { get; set; }

    public bool Capitaine { get; set; }
    public bool EstRemplacant { get; set; } = false;
}

public class Partie : BaseEntity
{
    public int Index { get; set; }
    public string JoueurA { get; set; }
    public string JoueurB { get; set; }

    public string NomJoueurA { get; set; }

    public string NomJoueurB { get; set; }

    public List<Score> Scores { get; set; } = new();
    //public int[] Scores { get; set; }

    public bool EnCours => !EstFinie && Scores.Where(s => s != null).Any();
    public bool EstFinie => JoueurAGagne || JoueurBGagne;
    public bool EstDouble { get; set; }
    public int NombreMancheGagnante { get; set; } = 3;
    public bool JoueurAGagne => Scores.Where(s => s.Valeur != null).Count(s => s.Valeur >= 0) >= NombreMancheGagnante;
    public bool JoueurBGagne => Scores.Where(s => s.Valeur != null).Count(s => s.Valeur < 0) >= NombreMancheGagnante;

}

public class Score : BaseEntity
{

    public int? Valeur { get; set; } = null;
}

