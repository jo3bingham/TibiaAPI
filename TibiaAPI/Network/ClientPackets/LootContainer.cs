using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class LootContainer : ClientPacket
    {
        Position Position { get; set; }

        public ushort ObjectId { get; set; }

        public byte Index { get; set; }
        public byte ItemCategory { get; set; }
        public byte Type { get; set; }

        public bool UseMainContainerAsFallback { get; set; }

        public LootContainer(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.LootContainer;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.LootContainer)
            {
                return false;
            }

            Type = message.ReadByte();
            if (Type == 0)
            {
                ItemCategory = message.ReadByte();
                Position = message.ReadPosition();
                ObjectId = message.ReadUInt16();
                Index = message.ReadByte();
            }
            else if (Type == 1 || Type == 2)
            {
                ItemCategory = message.ReadByte();
            }
            else if (Type == 3)
            {
                UseMainContainerAsFallback = message.ReadBool();
            }
            else
            {
                throw new System.Exception($"[LootContainer.ParseFromNetworkMessage] Invalid type: {Type}");
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.LootContainer);
            message.Write(Type);
            if (Type == 0)
            {
                message.Write(ItemCategory);
                message.Write(Position);
                message.Write(ObjectId);
                message.Write(Index);
            }
            else if (Type == 1 || Type == 2)
            {
                message.Write(ItemCategory);
            }
            else if (Type == 3)
            {
                message.Write(UseMainContainerAsFallback);
            }
        }
    }
}
