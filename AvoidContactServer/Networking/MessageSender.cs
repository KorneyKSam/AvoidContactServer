using AvoidContactCommon.Sign.Messages;
using AvoidContactCommon.Validation;
using Riptide;

namespace AvoidContactServer.Database.Networking
{
    public class MessageSender
    {
        private Server m_Server;

        public MessageSender(Server server)
        {
            m_Server = server;
        }

        public void SendSignInResult(ushort playerId, SignInResult result, string authorizationToken)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ServerSignMessage.SignInResult);
            message.AddByte((byte)result);
            message.AddString(authorizationToken);
            m_Server.Send(message, playerId);
        }

        public void SendSignUpResult(ushort playerId, SignUpResult result)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ServerSignMessage.SignUpResult);
            message.AddByte((byte)result);
            m_Server.Send(message, playerId);
        }
    }
}