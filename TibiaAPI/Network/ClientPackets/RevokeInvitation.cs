using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class RevokeInvitation : ClientPacket
    {
        public uint PlayerId { get; set; }

        public RevokeInvitation()
        {
            PacketType = ClientPacketType.RevokeInvitation;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.RevokeInvitation)
            {
                return false;
            }

            PlayerId = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.RevokeInvitation);
            message.Write(PlayerId);
        }
    }
}
