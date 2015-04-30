using Mhasb.Domain.Notifications;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Notifications
{
    public class InvitationService : IInvitationService
    {
        private readonly CrudOperation<Invitation> inviteRep = new CrudOperation<Invitation>();
        public bool CreateInvitation(Invitation invite)
        {
            try
            {
                invite.State = ObjectState.Added;
                inviteRep.AddOperation(invite);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool AcceptInvitation(Invitation invite)
        {
            try
            {
                invite.State = ObjectState.Added;
                inviteRep.AddOperation(invite);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }


        public bool UpdateInvitation(Invitation Invite)
        {
            throw new NotImplementedException();
        }

        public bool DeleteInvitation(int InviteId)
        {
            try
            {

                inviteRep.DeleteOperation(InviteId);
                return true;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }

        }

        public Invitation GetSingleInvitation(int InviteId)
        {
            try {
             var invitationObj=inviteRep.GetOperation()
                 .Filter(c => c.Id == InviteId)
                 .Get().SingleOrDefault();
             return invitationObj;
            }catch(Exception Ex){
                var msg = Ex.Message;
                return null;

            }
        }

        public List<Invitation> GetAllInvitation()
        {
            try
            {
                var invitationObj = inviteRep.GetOperation()
                    .Get().ToList();
                return invitationObj;
            }
            catch (Exception Ex)
            {
                var msg = Ex.Message;
                return null;

            }
        }
    }
}
