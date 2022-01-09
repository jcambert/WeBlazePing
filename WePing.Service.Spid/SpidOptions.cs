namespace WePing.Service.Spid
{
    public sealed class SpidOptions
    {
        public  const string SPID= "spid";
        internal const string CLUB_DETAIL = "club_detail";
        internal const string CLUB_LISTE = "club_liste";
        internal const string DIVISION = "division";
        internal const string EPREUVES = "epreuves";
        internal const string ORGANISMES = "organismes";
        internal const string RESULTAT_EQUIPE_RENCONTRES = "resultat_equipe";
        internal const string RESULTAT_EQUIPE_POULES = "resultat_equipe";
        internal const string RESULTAT_EQUIPE_CLASSEMENTS = "resultat_equipe";
        internal const string EQUIPES = "equipe_liste";
        internal const string RESULTAT_INDIVIDUEL_POULES = "resultat_indiv";
        internal const string RESULTAT_INDIVIDUEL_CLASSEMENTS = "resultat_indiv";
        internal const string RESULTAT_INDIVIDUEL_PARTIES = "resultat_indiv";
        internal const string CLASSEMENT_JOUEUR = "classement";
        internal const string JOUEURS_CLASSEMENT = "joueur_liste_cla";
        internal const string JOUEURS_LICENCE= "joueur_liste_spid";
        internal const string JOUEUR_DETAIL_CLASSEMENT = "joueur_detail_classement";
        internal const string JOUEUR_DETAIL_LICENCE = "joueur_detail_licence";
        internal const string LICENCE = "joueur_licence_spid";
        internal const string PARTIES = "joueur_partie_mysql";
        internal const string PARTIES_ = "joueur_partie_spid";
        internal const string ACTUALITES = "actu_fftt";
        internal const string HISTO_CLASSEMENT = "joueur_historique_cla";
        internal const string RENCONTRES = "rencontre";
        internal const string RENCONTRE_DETAIL = "rencontre_detail";
        public string AppId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Serie { get; set; } = string.Empty;
        public string EndPoint { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public Dictionary<string, string> Api { get; set; } = new Dictionary<string, string>();

        public SpidOptions()
        {



        }
    }
}