namespace WePing.Gir
{
    public sealed class GirOptions
    {
        public const string GIR = "gir";

        public Dictionary<string, ModeleRencontre> Modele { get; set; } = new Dictionary<string, ModeleRencontre>();

        public Dictionary<string,string> Messages { get; set; }=new Dictionary<string,string>();

        public GirOptions()
        {

        }
    }
}
