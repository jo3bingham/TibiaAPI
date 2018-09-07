using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class FieldData : MapBase
    {
        public Position Position { get; set; }

        public FieldData()
        {
            PacketType = ServerPacketType.FieldData;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.FieldData)
            {
                return false;
            }

            // TODO
            //var fields = new System.Collections.Generic.List<WorldMap.Field>();

            //Position = message.ReadPosition();
            //var mapPosition = client.WorldMapStorage.ToMap(Position);
            //client.WorldMapStorage.ResetField(mapPosition.X, mapPosition.Y, mapPosition.Z);
            //ReadField(message, mapPosition.X, mapPosition.Y, mapPosition.Z, fields);
            //client.WorldMapStorage.OnMapUpdated(fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.FieldData);
            // TODO
        }
    }
}
