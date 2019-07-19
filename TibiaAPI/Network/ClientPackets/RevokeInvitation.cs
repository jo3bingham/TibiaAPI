using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RevokeInvitation : ClientPacket
    {
        public uint PlayerId { get; set; }

        public RevokeInvitation(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.RevokeInvitation;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RevokeInvitation);
            message.Write(PlayerId);
        }
    }
}
