using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class ShareExperience : ClientPacket
    {
        public bool EnableSharedExperience { get; set; }

        public ShareExperience(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.ShareExperience;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            EnableSharedExperience = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ShareExperience);
            message.Write(EnableSharedExperience);
        }
    }
}
