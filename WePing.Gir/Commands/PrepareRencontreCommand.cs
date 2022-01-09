using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePing.Gir.Commands
{
    public class PrepareRencontreCommand : IRequest<ValidateableResponse<Rencontre>>, IValidateable
    {
    }
}
