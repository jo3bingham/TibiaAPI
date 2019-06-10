using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class InspectObject : ClientPacket
    {
        public Position Position { get; set; }

        public ushort ObjectId { get; set; }

        public byte Data { get; set; }
        public byte InspectionType { get; set; }

        public InspectObject(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.InspectObject;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            InspectionType = message.ReadByte();
            if (InspectionType == 3) // Cyclopedia
            {
                ObjectId = message.ReadUInt16();
                Data = message.ReadByte();
            }
            else
            {
                Position = message.ReadPosition();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.InspectObject);
            message.Write(InspectionType);
            if (InspectionType == 3)
            {
                message.Write(ObjectId);
                message.Write(Data);
            }
            else
            {
                message.Write(Position);
            }
        }
    }
}
