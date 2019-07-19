using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class EquipObject : ClientPacket
    {
        public ushort ObjectId { get; set; }

        public byte Data { get; set; }

        public EquipObject(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.EquipObject;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ObjectId = message.ReadUInt16();
            Data = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.EquipObject);
            message.Write(ObjectId);
            message.Write(Data);
        }
    }
}
