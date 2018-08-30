using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class PassLeadership : ClientPacket
    {
        public uint PlayerId { get; set; }

        public PassLeadership()
        {
            Type = ClientPacketType.PassLeadership;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            PlayerId = message.ReadUInt32();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.PassLeadership);
            message.Write(PlayerId);
        }
    }
}
