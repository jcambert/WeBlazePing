using System.Web;
using WePing.Domain.Rencontres.Domain;

namespace WePing.Domain.Rencontres.Queries
{
    public class GetRencontre : IQuery<Rencontre>
    {
        string _lien;
        public string Is_Retour { get; set; }
        public string Phase { get; set; }
        public string Res_1 { get; set; }
        public string Res_2 { get; set; }
        public string Renc_Id { get; set; }
        public string Equip_1 { get; set; }
        public string Equip_2 { get; set; }
        public string Equip_Id1 { get; set; }
        public string Equip_Id2 { get; set; }
        [Ignore]
        public string Lien
        {
            get => _lien;
            set
            {
                var parsed = HttpUtility.ParseQueryString(value);
                foreach (string key in parsed.Keys)
                {
                    var v = parsed[key];
                    this.SetPropertyByName(key.ToTitle(), v);
                    Console.WriteLine($"{key.ToTitle()}:{v}");
                }
            
                
                _lien = value;
            }
        }

    }
}
