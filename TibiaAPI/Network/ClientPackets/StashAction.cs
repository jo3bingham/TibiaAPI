using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class StashAction : ClientPacket
    {
        public uint ItemCount { get; set; }

        public ushort ItemId { get; set; }

        public byte StashType { get; set; }
        public byte Unknown { get; set; }

        public StashAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.StashAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            StashType = message.ReadByte();
            // TODO: Are there more stash types? 3 = retrieve?
            if (StashType == 3)
            {
                ItemId = message.ReadUInt16();
                ItemCount = message.ReadUInt32();
                if (Client.VersionNumber >= 12200000)
                {
                    Unknown = message.ReadByte();
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.StashAction);
            message.Write(StashType);
            if (StashType == 3)
            {
                message.Write(ItemId);
                message.Write(ItemCount);
                if (Client.VersionNumber >= 12200000)
                {
                    message.Write(Unknown);
                }
            }
        }
    }
}
