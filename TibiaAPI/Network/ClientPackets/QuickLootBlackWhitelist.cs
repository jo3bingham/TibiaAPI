using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class QuickLootBlackWhitelist : ClientPacket
    {
        public byte LootListType { get; set; }

        // This is more-than-likely a count variable.
        public ushort Unknown { get; set; }

        public QuickLootBlackWhitelist()
        {
            Type = ClientPacketType.QuickLootBlackWhitelist;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            LootListType = message.ReadByte();
            Unknown = message.ReadUInt16();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.QuickLootBlackWhitelist);
            message.Write(LootListType);
            message.Write(Unknown);
        }
    }
}
