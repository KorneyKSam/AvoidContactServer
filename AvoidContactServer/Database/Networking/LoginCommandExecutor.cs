using AvoidContactServer.Database.Networking.Enums;
using AvoidContactServer.Database.Networking.Models;
using AvoidContactServer.Database.Repositories;

namespace AvoidContactServer.Database.Networking
{
    internal class LoginCommandExecutor
    {
        private readonly LoginRepository m_LoginRepository;

        public LoginCommandExecutor(LoginRepository loginRepository)
        {
            m_LoginRepository = loginRepository;
        }

        internal void SignIn(ushort playerId, SignInModel signInModel)
        {

        }

        internal void SignUp(ushort playerId, SignUpModel signUpModel)
        {
            m_LoginRepository.AddPlayer(signUpModel);
        }

        internal void SignOut(ushort playerId)
        {

        }
    }
}