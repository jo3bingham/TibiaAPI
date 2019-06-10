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

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Slot = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DeleteInventory);
            message.Write(Slot);
        }
    }
}
