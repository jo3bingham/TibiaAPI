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
                count = message.ReadUInt16();
                for (var i = 0; i < count; ++i)
                {
                    message.ReadUInt16();
                }
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
            else if (CyclopediaMapDataType == 3)
            {
                message.ReadUInt16();
                message.ReadByte();
            }
            else if (CyclopediaMapDataType == 5)
            {
                message.ReadUInt32();
            }
            else if (CyclopediaMapDataType == 6)
            {
                message.ReadPosition();
                message.ReadByte();
                message.ReadUInt16();
            }
            else if (CyclopediaMapDataType == 7)
            {
                var count = message.ReadByte();
                for (var i = 0; i < count; ++i)
                {
                    message.ReadUInt16();
                    var count2 = message.ReadByte();
                    for (var j = 0; j < count2; ++j)
                    {
                        message.ReadUInt16();
                        message.ReadUInt16();
                    }
                }
            }
            else if (CyclopediaMapDataType == 8)
            {
                message.ReadUInt16();
                message.ReadByte();
                message.ReadUInt16();
            }
            else if (CyclopediaMapDataType == 9)
            {
                var minimumGoldDonation = message.ReadUInt64();
                var count = message.ReadByte();
                for (var i = 0; i < count; ++i)
                {
                    var areaId = message.ReadUInt16();
                    var hasImprovedSpawnRate = message.ReadBool();
                    var donatedGold = message.ReadUInt64();
                }
            }
            else if (CyclopediaMapDataType == 10)
            {
                message.ReadUInt16();
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
