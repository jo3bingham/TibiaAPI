using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SetInventory : ServerPacket
    {
        public ObjectInstance Item { get; set; }

        public byte Slot { get; set; }

        public SetInventory(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SetInventory;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Slot = message.ReadByte();
            Item = message.ReadObjectInstance();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SetInventory);
            message.Write(Slot);
            message.Write(Item);
        }
    }
}
