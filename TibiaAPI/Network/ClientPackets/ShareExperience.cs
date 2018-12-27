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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.ShareExperience)
            {
                return false;
            }

            EnableSharedExperience = message.ReadBool();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.ShareExperience);
            message.Write(EnableSharedExperience);
        }
    }
}
