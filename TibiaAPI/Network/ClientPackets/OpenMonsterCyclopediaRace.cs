using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenMonsterCyclopediaRace : ClientPacket
    {
        public ushort Id { get; set; }

        public OpenMonsterCyclopediaRace()
        {
            PacketType = ClientPacketType.OpenMonsterCyclopediaRace;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.OpenMonsterCyclopediaRace)
            {
                return false;
            }

            Id = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenMonsterCyclopediaRace);
            message.Write(Id);
        }
    }
}
