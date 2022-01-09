using WePing.Domain.ResultatEquipe.Domain;

namespace WePing.Domain.ResultatEquipe.Queries
{
    public class GetResultatEquipeRencontre : IQuery<ResultatEquipeRencontre>
    {
        public Guid Id { get; set; }
    }
}
