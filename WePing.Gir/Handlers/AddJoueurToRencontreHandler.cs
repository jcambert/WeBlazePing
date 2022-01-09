namespace WePing.Gir.Handlers;
public class AddJoueurToRencontreHandler : IRequestHandler<AddJoueurToRencontreCommand, ValidateableResponse<Joueur>>
{
    public async Task<ValidateableResponse<Joueur>> Handle(AddJoueurToRencontreCommand request, CancellationToken cancellationToken)
    {
        return new ValidateableResponse<Joueur>(new Joueur() { }, null);
    }
}

