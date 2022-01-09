using AutoMapper;

namespace WePing.Gir.Handlers;
public class CreateRencontreHandler : BaseValidateableCommandHandler<GirContext,Rencontre,Guid, CreateRencontreCommand, Rencontre>
{
    

    public CreateRencontreHandler(IRepository<GirContext, Rencontre, Guid> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public override Task<ValidateableResponse<Rencontre>> Handle(CreateRencontreCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

