using FluentValidation;
using FluentValidation.Results;
using WePing.Domain.ClubDetails.Domain;
using WePing.Domain.Joueurs.Domain;
using WePing.Gir.Commands;
using WePing.Service.Spid;

namespace WePing.Gir.Validations;
public class AddJoueurToRencontreCommandValidator : AbstractValidator<AddJoueurToRencontreCommand>
{
    private readonly IRepository<GirContext, Rencontre, Guid> Repository;
    private readonly ISpidRequest SpidRequest;

    public AddJoueurToRencontreCommandValidator(IRepository<GirContext,Rencontre,Guid> repo,ISpidRequest spidRequest) 
    {
        this.RuleFor(c => c.Licence).NotEmpty();
        this.RuleFor(c => c.NumeroClub).NotEmpty();
        this.RuleFor(c => c.Rencontre).NotNull();

        this.Repository = repo;
        this.SpidRequest = spidRequest;
        
    }

    public override async Task<ValidationResult> ValidateAsync(ValidationContext<AddJoueurToRencontreCommand> context, CancellationToken cancellation = default)
    {
        var validationResult= await base.ValidateAsync(context, cancellation);

        var command = context.InstanceToValidate;

        JoueurLicence joueur = await SpidRequest.GetJoueurDetailLicenceAsync(new Domain.Joueurs.Queries.GetJoueurLicence() { Licence = command.Licence},cancellation);
        ClubDetail club= await SpidRequest.GetClubDetailsAsync(new Domain.ClubDetails.Queries.GetClubDetail() { Club = command.NumeroClub });

        if (joueur == null)
            validationResult.Errors.Add(new ValidationFailure("Joueur", $"Le joueur avec la licence {command.Licence} n'existe pas"));
        if(club==null)
            validationResult.Errors.Add(new ValidationFailure("Club", $"Le club n° {command.NumeroClub} n'existe pas"));
        return validationResult;
    }
}

