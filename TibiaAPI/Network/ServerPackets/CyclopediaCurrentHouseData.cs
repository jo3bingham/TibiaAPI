using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CyclopediaCurrentHouseData : ServerPacket
    {
        public CyclopediaCurrentHouseData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.CyclopediaCurrentHouseData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO
            
            //DD 9D 00 00 01 00 00 00 00 05 

            //DC 9D 00 00 
            //01 
            //02 state
            //0C 00 4D 61 78 73 6B 79 77 61 6C 6B 65 72 owner name
            //1A FA 4C 5E rent paid until timestamp
            //00 

            //D3 9D 00 00
            //01
            //00
            //06 00 52 61 65 70 73 73
            //00 05
            //90 90 53 5E auction end timestamp
            //00 00 00 00 00 00 00 00 highest bid

            var count = message.ReadUInt16();
            for (var i = 0; i < count; ++i)
            {
                message.ReadUInt32(); //house id
                message.ReadByte(); //?
                var state = message.ReadByte(); // 2 = rented, 0 = auctioned?
                var playerName = message.ReadString();
                if (state == 2)
                {
                    var rentPaidUntilTimestamp = message.ReadUInt32();
                    message.ReadByte(); //?
                }
                else
                {
                    message.ReadBytes(2); //always 0x00 0x05
                    if (!string.IsNullOrEmpty(playerName))
                    {
                        var auctionEndTimestamp = message.ReadUInt32();
                        var highestBid = message.ReadUInt64();
                    }
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.CyclopediaCurrentHouseData);
        }
    }
}
