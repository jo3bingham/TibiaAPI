using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class InspectObject : ClientPacket
    {
        public Position Position { get; set; }

        public ushort ObjectId { get; set; }

        public byte InspectionType { get; set; }
        public byte Unknown { get; set; }

        public InspectObject()
        {
            Type = ClientPacketType.InspectObject;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.InspectObject)
            {
                return false;
            }

            InspectionType = message.ReadByte();
            if (InspectionType == 3) // cyclopedia
            {
                ObjectId = message.ReadUInt16();
                Unknown = message.ReadByte(); // always 0 in my tests
            }
            else
            {
                Position = message.ReadPosition();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.InspectObject);
            message.Write(InspectionType);
            if (InspectionType == 3)
            {
                message.Write(ObjectId);
                message.Write(Unknown);
            }
            else
            {
                message.Write(Position);
            }
        }
    }
}
