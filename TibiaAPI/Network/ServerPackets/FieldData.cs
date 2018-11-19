using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class FieldData : Map
    {
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

            var position = message.ReadPosition();
            var mapPosition = client.WorldMapStorage.ToMap(position);
            client.WorldMapStorage.ResetField(mapPosition.X, mapPosition.Y, mapPosition.Z);
            message.ReadField(client, mapPosition.X, mapPosition.Y, mapPosition.Z, Fields);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.FieldData);
            // TODO
        }
    }
}
