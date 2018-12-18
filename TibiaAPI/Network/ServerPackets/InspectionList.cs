using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class InspectionList : ServerPacket
    {
        public bool IsPlayer { get; set; }

        public InspectionList(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.InspectionList;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.InspectionList)
            {
                return false;
            }

            IsPlayer = message.ReadBool();

            var numberOfObjects = message.ReadByte();
            for (var i = 0; i < numberOfObjects; ++i)
            {
                var objectName = message.ReadString();

                if (IsPlayer)
                {
                    var slotId = message.ReadByte();
                }

                var Item = message.ReadObjectInstance(Client);

                var imbuementSlots = message.ReadByte();
                for (var x = 0; x < imbuementSlots; ++x)
                {
                    var imbuementId = message.ReadUInt16();
                }

                var numberOfDetails = message.ReadByte();
                for (var n = 0; n < numberOfDetails; ++n)
                {
                    var detailName = message.ReadString();
                    var description = message.ReadString();
                }
            }

            if (IsPlayer)
            {
                var playerName = message.ReadString();
                var playerOutfit = message.ReadCreatureOutfit(Client);

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
