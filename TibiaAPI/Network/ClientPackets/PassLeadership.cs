using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PassLeadership : ClientPacket
    {
        public uint PlayerId { get; set; }

        public PassLeadership()
        {
            PacketType = ClientPacketType.PassLeadership;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.PassLeadership)
            {
                return false;
            }

            PlayerId = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.PassLeadership);
            message.Write(PlayerId);
        }
    }
}
