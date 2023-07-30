namespace AvoidContactServer.Interfaces.Networking
{
    public interface IServerController
    {
        public void Start(ushort port, ushort maxClientCount);
        public void Stop();
    }
}