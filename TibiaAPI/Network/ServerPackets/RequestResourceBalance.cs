using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RequestResourceBalance : ServerPacket
    {
        public ResourceType ResourceType { get; set; }

        public long Balance { get; set; }

        public RequestResourceBalance(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.RequestResourceBalance;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ResourceType = (ResourceType)message.ReadByte();
            if (ResourceType == ResourceType.CharmPoints && Client.VersionNumber > 11586239)
            {
                Balance = message.ReadUInt32();
            }
            else
            {
                Balance = message.ReadInt64();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RequestResourceBalance);
            message.Write((byte)ResourceType);
            if (ResourceType == ResourceType.CharmPoints && Client.VersionNumber > 11586239)
            {
                message.Write((uint)Balance);
            }
            else
            {
                message.Write(Balance);
            }
        }
    }
}
