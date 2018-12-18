using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DeleteInventory : ServerPacket
    {
        public byte Slot { get; set; }

        public DeleteInventory(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.DeleteInventory;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.DeleteInventory)
            {
                return false;
            }

            Slot = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DeleteInventory);
            message.Write(Slot);
        }
    }
}
