using WePing.Domain.Equipes.Domain;

namespace WePing.Domain.Equipes.Queries
{
    public class GetEquipe : IQuery<Equipe>
    {
        public Guid Id { get; set; }
    }
}
