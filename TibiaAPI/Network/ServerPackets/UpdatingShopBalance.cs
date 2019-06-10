using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class UpdatingShopBalance : ServerPacket
    {
        public bool IsUpdating { get; set; }

        public UpdatingShopBalance(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.UpdatingShopBalance;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            IsUpdating = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.UpdatingShopBalance);
            message.Write(IsUpdating);
        }
    }
}
