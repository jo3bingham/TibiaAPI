using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RemoveGraphicalEffect : ServerPacket
    {
        public RemoveGraphicalEffect()
        {
            PacketType = ServerPacketType.RemoveGraphicalEffect;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.RemoveGraphicalEffect)
            {
                return false;
            }

            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RemoveGraphicalEffect);
        }
    }
}
