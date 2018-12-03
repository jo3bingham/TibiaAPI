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
            PacketType = ClientPacketType.InspectObject;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.InspectObject)
            {
                return false;
            }

            InspectionType = message.ReadByte();
            if (InspectionType == 3) // Cyclopedia
            {
                ObjectId = message.ReadUInt16();
                // TODO: Figure out this unknown. Always 0 in my tests. Probably "data".
                Unknown = message.ReadByte();
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
