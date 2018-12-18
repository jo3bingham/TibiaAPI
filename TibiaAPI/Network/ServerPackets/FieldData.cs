using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class FieldData : Map
    {
        public FieldData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.FieldData;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.FieldData)
            {
                return false;
            }

            var position = message.ReadPosition();
            var mapPosition = Client.WorldMapStorage.ToMap(position);
            Client.WorldMapStorage.ResetField(mapPosition.X, mapPosition.Y, mapPosition.Z);
            message.ReadField(Client, mapPosition.X, mapPosition.Y, mapPosition.Z, Fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.FieldData);
            // TODO
        }
    }
}
