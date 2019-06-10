using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class FieldData : Map
    {
        public Position Position { get; set; }

        public FieldData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.FieldData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Position = message.ReadPosition();
            var mapPosition = Client.WorldMapStorage.ToMap(Position);
            Client.WorldMapStorage.ResetField(mapPosition.X, mapPosition.Y, mapPosition.Z);
            message.ReadField(mapPosition.X, mapPosition.Y, mapPosition.Z, Fields);
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.FieldData);
            message.Write(Position);
            base.AppendToNetworkMessage(message);
        }
    }
}
