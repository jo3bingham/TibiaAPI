using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CyclopediaMapData : ServerPacket
    {
        public CyclopediaMapDataType DataType { get; set; }

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

            DataType = (CyclopediaMapDataType)message.ReadByte();
            if (DataType == CyclopediaMapDataType.DiscoveryData)
            {
                var numberOfMainAreas = message.ReadUInt16();
                for (var i = 0; i < numberOfMainAreas; ++i)
                {
                    var areaId = message.ReadUInt16();
                    var status = message.ReadByte(); // 0 = not started, 1 = partial discovery w/o NPCs, 2 = partial discover w/ NPCs, 3 = completed
                    var progress = message.ReadByte(); // 0-100%, or 0xFF (cannot be discovered)
                }
                var numberOfDiscoveredSubAreas = message.ReadUInt16();
                for (var i = 0; i < numberOfDiscoveredSubAreas; ++i)
                {
                    var areaId = message.ReadUInt16();
                }
                var numberOfDiscoverableSubAreas = message.ReadUInt16();
                for (var i = 0; i < numberOfDiscoverableSubAreas; ++i)
                {
                    var areaId = message.ReadUInt16();
                }
            }
            else if (DataType == CyclopediaMapDataType.ActiveRaid)
            {
                var position = message.ReadPosition();
                var state = message.ReadByte(); // 0 = active (show exclamation mark on map) 
            }
            else if (DataType == CyclopediaMapDataType.ImminentRaidMainArea)
            {
                var areaId = message.ReadUInt16();
                var numberOfRaids = message.ReadByte();
            }
            else if (DataType == CyclopediaMapDataType.ImminentRaidSubArea)
            {
                var areaId = message.ReadUInt16();
                var numberOfRaids = message.ReadByte();
            }
            else if (DataType == CyclopediaMapDataType.SetDiscoveryArea)
            {
                var areaId = message.ReadUInt32();
                var numberOfPointsOfInterestToDiscover = message.ReadByte();
                var numberOfPointsOfInterest = message.ReadByte();
                for (var i = 0; i < numberOfPointsOfInterest; ++i)
                {
                    var position = message.ReadPosition();
                    var state = message.ReadByte(); // 0 = hidden, 1 = discovered?
                }
            }
            else if (DataType == CyclopediaMapDataType.Passage)
            {
                var position = message.ReadPosition();
                var type = message.ReadByte(); // 0 = w/o subarea id, 1 = w/ subarea id?
                var areaId = message.ReadUInt16();
            }
            else if (DataType == CyclopediaMapDataType.SubAreaMonsters)
            {
                var numberOfSubAreas = message.ReadByte();
                for (var i = 0; i < numberOfSubAreas; ++i)
                {
                    var areaId = message.ReadUInt16();
                    var numberOfMonsters = message.ReadByte();
                    for (var j = 0; j < numberOfMonsters; ++j)
                    {
                        var raceId = message.ReadUInt16();
                        var isKnown = message.ReadBool();
                        var rarity = message.ReadByte(); // 0 = common, 1 = rare, 2 = varying
                    }
                }
            }
            else if (DataType == CyclopediaMapDataType.Donations)
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
            else if (DataType == CyclopediaMapDataType.SetCurrentArea)
            {
                var areaId = message.ReadUInt16();
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
