using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Greet : ClientPacket
    {
        public uint NpcId { get; set; }

        public Greet(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Greet;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            NpcId = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Greet);
            message.Write(NpcId);
        }
    }
}
