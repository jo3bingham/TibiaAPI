using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenMonsterCyclopediaRace : ClientPacket
    {
        public ushort Id { get; set; }

        public OpenMonsterCyclopediaRace(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.OpenMonsterCyclopediaRace;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Id = message.ReadUInt16();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenMonsterCyclopediaRace);
            message.Write(Id);
        }
    }
}
