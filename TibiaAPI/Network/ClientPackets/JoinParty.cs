using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class JoinParty : ClientPacket
    {
        public uint PlayerId { get; set; }

        public JoinParty()
        {
            PacketType = ClientPacketType.JoinParty;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.JoinParty)
            {
                return false;
            }

            PlayerId = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.JoinParty);
            message.Write(PlayerId);
        }
    }
}
