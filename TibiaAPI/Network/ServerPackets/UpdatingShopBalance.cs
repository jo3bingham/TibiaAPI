using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class UpdatingShopBalance : ServerPacket
    {
        public bool IsUpdating { get; set; }

        public UpdatingShopBalance()
        {
            PacketType = ServerPacketType.UpdatingShopBalance;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.UpdatingShopBalance)
            {
                return false;
            }

            IsUpdating = message.ReadBool();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.UpdatingShopBalance);
            message.Write(IsUpdating);
        }
    }
}
