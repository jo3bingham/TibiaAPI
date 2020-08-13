using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LootContainer : ClientPacket
    {
        Position Position { get; set; }

        public LootContainerType Type { get; set; }

        public ushort ObjectId { get; set; }

        public byte Index { get; set; }
        public byte ItemCategory { get; set; }

        public bool UseMainContainerAsFallback { get; set; }

        public LootContainer(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.LootContainer;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Type = (LootContainerType)message.ReadByte();
            if (Type == LootContainerType.Add)
            {
                ItemCategory = message.ReadByte();
                Position = message.ReadPosition();
                ObjectId = message.ReadUInt16();
                Index = message.ReadByte();
            }
            else if (Type == LootContainerType.Remove || Type == LootContainerType.Open)
            {
                ItemCategory = message.ReadByte();
            }
            else if (Type == LootContainerType.UseMainContainerAsFallback)
            {
                UseMainContainerAsFallback = message.ReadBool();
            }
            else
            {
                Client.Logger.Error($"[LootContainer.ParseFromNetworkMessage] Invalid type: {Type}");
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LootContainer);
            message.Write((byte)Type);
            if (Type == LootContainerType.Add)
            {
                message.Write(ItemCategory);
                message.Write(Position);
                message.Write(ObjectId);
                message.Write(Index);
            }
            else if (Type == LootContainerType.Remove || Type == LootContainerType.Open)
            {
                message.Write(ItemCategory);
            }
            else if (Type == LootContainerType.UseMainContainerAsFallback)
            {
                message.Write(UseMainContainerAsFallback);
            }
        }
    }
}
