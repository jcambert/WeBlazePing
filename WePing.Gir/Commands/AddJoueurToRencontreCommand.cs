
namespace WePing.Gir.Commands;
public class AddJoueurToRencontreCommand: IRequest<ValidateableResponse<Joueur>>, IValidateable
{
    public string NumeroClub { get; set; }
    public string Licence { get; set; }
    public Rencontre Rencontre { get; set; }
}

