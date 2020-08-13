using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SpecialContainersAvailable : ServerPacket
    {
        public bool IsMarketAvailable { get; set; }
        public bool IsSupplyStashAvailable { get; set; }

        public SpecialContainersAvailable(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SpecialContainersAvailable;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            IsSupplyStashAvailable = message.ReadBool();
            IsMarketAvailable = message.ReadBool();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SpecialContainersAvailable);
            message.Write(IsSupplyStashAvailable);
            message.Write(IsMarketAvailable);
        }
    }
}
