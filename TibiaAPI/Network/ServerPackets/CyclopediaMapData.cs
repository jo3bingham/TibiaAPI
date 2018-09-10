using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CyclopediaMapData : ServerPacket
    {
        public byte CyclopediaMapDataType { get; set; }

        public CyclopediaMapData()
        {
            PacketType = ServerPacketType.CyclopediaMapData;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CyclopediaMapData)
            {
                return false;
            }

            CyclopediaMapDataType = message.ReadByte();
            if (CyclopediaMapDataType == 1)
            {
                var count = message.ReadUInt16();
                for (var i = 0; i < count; ++i)
                {
                    message.ReadUInt32();
                }
                var unknown = message.ReadUInt16();
                count = message.ReadUInt16();
                for (var i = 0; i < count; ++i)
                {
                    message.ReadUInt16();
                }
            }
            else if (CyclopediaMapDataType == 2)
            {
                message.ReadPosition();
                message.ReadByte();
            }
            else if (CyclopediaMapDataType == 5)
            {
                message.ReadUInt32();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CyclopediaMapData);
            //TODO
        }
    }
}
