using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class EquipObject : ClientPacket
    {
        public ushort ObjectId { get; set; }

        public byte Data { get; set; }

        public EquipObject()
        {
            Type = ClientPacketType.EquipObject;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.EquipObject)
            {
                return false;
            }

            ObjectId = message.ReadUInt16();
            Data = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.EquipObject);
            message.Write(ObjectId);
            message.Write(Data);
        }
    }
}
