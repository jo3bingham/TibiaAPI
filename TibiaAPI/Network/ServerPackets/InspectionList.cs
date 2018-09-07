using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class InspectionList : ServerPacket
    {
        public List<(string Name, string Description)> Details { get; } =
            new List<(string Name, string Description)>();

        //public Appearances.ObjectInstance Item { get; set; }

        public string ObjectName { get; set; }

        public byte ImbuementSlots { get; set; }
        public byte Unknown { get; set; }

        public InspectionList()
        {
            PacketType = ServerPacketType.InspectionList;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.InspectionList)
            {
                return false;
            }

            // TODO
            var isPlayer = message.ReadBool();

            var numberOfObjects = message.ReadByte();
            for (var i = 0; i < numberOfObjects; ++i)
            {
                ObjectName = message.ReadString();

                if (isPlayer)
                {
                    var slotId = message.ReadByte();
                }

                //Item = message.ReadObjectInstance();

                ImbuementSlots = message.ReadByte();
                for (var x = 0; x < ImbuementSlots; ++x)
                {
                    var imbuementId = message.ReadUInt16();
                }

                Details.Capacity = message.ReadByte();
                for (var n = 0; n < Details.Capacity; ++n)
                {
                    var detailName = message.ReadString();
                    var description = message.ReadString();
                    Details.Add((detailName, description));
                }
            }

            if (isPlayer)
            {
                var playerName = message.ReadString();
                //message.ReadCreatureOutfit();

                var numberOfDetails = message.ReadByte();
                for (var n = 0; n < numberOfDetails; ++n)
                {
                    var detailName = message.ReadString();
                    var description = message.ReadString();
                }
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.InspectionList);
            // TODO
        }
    }
}
