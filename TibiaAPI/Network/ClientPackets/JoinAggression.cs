using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class JoinAggression : ClientPacket
    {
        public uint PlayerId { get; set; }

        public JoinAggression()
        {
            Type = ClientPacketType.JoinAggression;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.JoinAggression)
            {
                return false;
            }

            PlayerId = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.JoinAggression);
            message.Write(PlayerId);
        }
    }
}
