using Mhasb.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Notifications
{
    public interface IInvitationService
    {
        bool CreateInvitation(Invitation Invite);
        bool AcceptInvitation(Invitation Invite);

        bool UpdateInvitation(Invitation Invite);

        bool DeleteInvitation(int Id);

        Invitation GetSingleInvitation(int Id);
        List<Invitation> GetAllInvitation();
        List<Invitation> GetAllInvitationByCompany(int companyId);
    }
}
