namespace WePing.Gir.Commands;

public class CreateRencontreCommand : IValidateableCreateCommand<Rencontre, Guid>, IValidateable
{
    public ModeleRencontre Modele { get; set; }
    public string EquipeDomicile { get; set; }
    public string EquipeAdverse { get; set; }
}

