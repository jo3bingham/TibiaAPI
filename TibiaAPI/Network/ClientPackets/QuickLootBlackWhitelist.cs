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
            PacketType = ClientPacketType.QuickLootBlackWhitelist;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.QuickLootBlackWhitelist)
            {
                return false;
            }

            LootListType = message.ReadByte();
            Unknown = message.ReadUInt16();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.QuickLootBlackWhitelist);
            message.Write(LootListType);
            message.Write(Unknown);
        }
    }
}
