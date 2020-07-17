using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DepotTileState : ServerPacket
    {
        public bool EnableShowInMarket { get; set; }
        public bool EnableStow { get; set; }

        public DepotTileState(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SpecialContainersAvailable;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            EnableStow = message.ReadBool();
            if (Client.VersionNumber >= 12087995)
            {
                EnableShowInMarket = message.ReadBool();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SpecialContainersAvailable);
            message.Write(EnableStow);
            if (Client.VersionNumber >= 12087995)
            {
                message.Write(EnableShowInMarket);
            }
        }
    }
}
