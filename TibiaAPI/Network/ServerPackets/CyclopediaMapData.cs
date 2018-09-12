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
            else if (CyclopediaMapDataType == 9)
            {
                //---- Example 1
                //DD // CyclopediaMapData: Thais (Rookgaard)

                //09 // Type

                //40 42 0F 00 00 00 00 00 07

                //16 00 00 64 00 00 00 00 00 00 00 05 00 00 1F 02 00 00 00 00 00 00 1B 00 00 70 00 00 00 00 00 00 00 13 00 00 01 00 00 00 00 00 00 00 18 00 00

                //01 00 00 00 00 00 00 00 // Donated gold

                //02 00 00 01 00 00 00 00 00 00 00 09 00 01 00 00 00 00 00 00 00 00

                //---- Example 2
                //DD // CyclopediaMapData: Thais (Rookgaard)

                //09 // Type

                //40 42 0F 00 00 00 00 00 08 0E 00 00 0B 00 00 00 00 00 00 00

                //16 00 00 64 00 00 00 00 00 00 00 05 00 00 1F 02 00 00 00 00 00 00 1B 00 00 70 00 00 00 00 00 00 00 13 00 00 01 00 00 00 00 00 00 00 18 00 00

                //04 00 00 00 00 00 00 00 // Donated gold

                //02 00 00 01 00 00 00 00 00 00 00 09 00 01 00 00 00 00 00 00 00 00
            }
            else if (CyclopediaMapDataType == 10)
            {
                //---- Example 1
                //DD // CyclopediaMapData: Thais (Rookgaard)

                //0A //Type

                //18 00 // Unknown
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
