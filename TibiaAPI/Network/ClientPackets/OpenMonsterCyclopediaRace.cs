using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class OpenMonsterCyclopediaRace : ClientPacket
    {
        public ushort Id { get; set; }

        public OpenMonsterCyclopediaRace()
        {
            Type = ClientPacketType.OpenMonsterCyclopediaRace;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            Id = message.ReadUInt16();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.OpenMonsterCyclopediaRace);
            message.Write(Id);
        }
    }
}
