using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;
using OXGaming.TibiaAPI.WorldMap;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class FieldData : ServerPacket
    {
        public (Field, Position) Field { get; set; }

        public FieldData()
        {
            PacketType = ServerPacketType.FieldData;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.FieldData)
            {
                return false;
            }

            var fields = new System.Collections.Generic.List<(Field, Position)>();
            var position = message.ReadPosition();
            var mapPosition = client.WorldMapStorage.ToMap(position);
            client.WorldMapStorage.ResetField(mapPosition.X, mapPosition.Y, mapPosition.Z);
            message.ReadField(client, mapPosition.X, mapPosition.Y, mapPosition.Z, fields);
            
            if (fields.Count > 0)
            {
                Field = fields[0];
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.FieldData);
            // TODO
        }
    }
}
