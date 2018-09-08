using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SetInventory : ServerPacket
    {
        public ObjectInstance Item { get; set; }

        public byte Slot { get; set; }

        public SetInventory()
        {
            PacketType = ServerPacketType.SetInventory;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.SetInventory)
            {
                return false;
            }

            Slot = message.ReadByte();
            Item = message.ReadObjectInstance();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SetInventory);
            message.Write(Slot);
            message.Write(Item);
        }
    }
}
