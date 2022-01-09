using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WePing.Gir.Checkings;

namespace WePing.Gir
{
    public static class GirExtensions
    {
        public static bool Preparer(this Rencontre rencontre, out string message, string club = null)
        {
            message = "";


            if (rencontre.EstPrete) return true;
            List<string> clubs = new List<string>();
            if (club == null)
                clubs.AddRange(rencontre.Joueurs.Select(x => x.NumeroClub).Distinct());
            else
                clubs.Add(club);
            List<string> messages = new();
            foreach (var c in clubs)
            {
                if (rencontre.Joueurs.Count(j => j.NumeroClub == c) > (rencontre.Modele.NombreDejoueur+rencontre.Modele.NombreRemplacant))
                {
                    messages.Add($"Le Nombre de joueurs inscris sur le club {c} est Superieur au nombre de joueurs + remplacants");
                    continue;
                }
                rencontre.SetJoueurToLetter(c);
            }
            if (messages.Count > 0)
            {
                message = string.Join("\n", messages);
                return false;
            }


            rencontre.EstPrete = !rencontre.Joueurs.Where(x => string.IsNullOrEmpty(x.Ordre)).Any();

            if (rencontre.EstPrete)
                rencontre.CreateParties();
            return true;
        }

        private static void SetJoueurToLetter(this Rencontre rencontre, string club)
        {
            var parties = rencontre.Modele.Parties.ToArray();
            int i = 0;
            foreach (var joueur in rencontre.Joueurs.Where(j => j.NumeroClub == club).Take(rencontre.Modele.NombreDejoueur).ToList())
            {

                joueur.Ordre = (joueur.NumeroClub == rencontre.EquipeDomicile) ? parties[i++].JoueurA : parties[i++].JoueurB;
            }
            foreach (var (index,joueur) in rencontre.Joueurs.Where(j => j.NumeroClub == club).Skip(rencontre.Modele.NombreDejoueur).Take(rencontre.Modele.NombreRemplacant).ToList().Select((value,index)=>(index,value)))
            {

                joueur.Ordre = $"Re{index + 1}";
                joueur.EstRemplacant = true;
            }
        }

        private static void CreateParties(this Rencontre rencontre)
        {
            rencontre.Parties.Clear();
            int index = 1;
            foreach (var p in rencontre.Modele.Parties)
            {
                Partie partie = null;
                if (p.EstDouble)
                {
                    if (rencontre.Modele.NombreDejoueur == 2)
                    {
                        var joueursA = rencontre.GetJoueurClub(rencontre.EquipeDomicile);
                        var joueursB = rencontre.GetJoueurClub(rencontre.EquipeAdverse);

                        partie=
                        new Partie()
                        {
                            EstDouble = true,
                            JoueurA = string.Join("", joueursA.Select(x => x.Ordre)),
                            JoueurB = string.Join("", joueursB.Select(x => x.Ordre)),
                            NomJoueurA = string.Join("-", joueursA.Select(x => $"{x.Nom} {x.Prenom}")),
                            NomJoueurB = string.Join("-", joueursB.Select(x => $"{x.Nom} {x.Prenom}"))
                        };
                    }
                    else
                    {
                        partie=new Partie() { EstDouble = true };
                    }
                }
                else
                {
                    Joueur ja = rencontre.GetJoueur(p.JoueurA);
                    Joueur jb = rencontre.GetJoueur(p.JoueurB);
                    partie = new Partie() { JoueurA = p.JoueurA, JoueurB = p.JoueurB, NomJoueurA = $"{ja.Nom} {ja.Prenom}", NomJoueurB = $"{jb.Nom} {jb.Prenom}" };
                }
                if (partie != null)
                {
                    partie.Index = index++;
                    partie.NombreMancheGagnante = rencontre.Modele.NombreMancheGagnante;
                    partie.Scores = new List<Score>((partie.NombreMancheGagnante * 2) - 1);
                    //partie.Scores = new int[(partie.NombreMancheGagnante * 2) - 1] ;
                    rencontre.Parties.Add(partie);

                }
            }
        }

        public static Joueur GetJoueur(this Rencontre rencontre, string lettre)
            => rencontre.Joueurs.Where(j => j.Ordre == lettre).FirstOrDefault();

        public static List<Joueur> GetJoueurClub(this Rencontre rencontre, string club) => rencontre.Joueurs.Where(j => j.NumeroClub == club).ToList();

        public static string GetJoueurNomPrenom(this Joueur joueur,int? length=null) =>(length!=null && length>1)? $"{joueur.Nom} {joueur.Prenom}".Substring(0,length??2) : $"{joueur.Nom} {joueur.Prenom}";

        public static void SetScore(this Partie partie, int manche, int score)
        {
            manche = (manche > (partie.NombreMancheGagnante * 2 - 1)) ? (partie.NombreMancheGagnante * 2 - 1) : manche;
            if (partie.Scores.Count < manche)
            {
                partie.Scores.Add(new Score() { Valeur = score });
                return;
            }
            partie.Scores[manche - 1] = new Score() { Valeur = score };
        }
        public static void SetScore(this Partie partie, int manche, int score,out int realManche )
        {
            manche = (manche > (partie.NombreMancheGagnante * 2 - 1)) ? (partie.NombreMancheGagnante * 2 - 1) : manche;
            if (partie.Scores.Count < manche)
            {
                partie.Scores.Add(new Score() { Valeur = score});
                realManche = partie.Scores.Count;
                return;
            }
            partie.Scores[manche - 1] = new Score() { Valeur = score };
            realManche = manche;
        }

        

    }
}
