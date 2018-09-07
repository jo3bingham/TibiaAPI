using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class StoreCategories : ServerPacket
    {
        public StoreCategories()
        {
            PacketType = ServerPacketType.StoreCategories;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.StoreCategories)
            {
                return false;
            }

            // TODO
            var numberOfShopCategories = message.ReadUInt16();
            for (var i = 0; i < numberOfShopCategories; ++i)
            {
                var categoryName = message.ReadString();
                //var categoryDescription = message.ReadString();
                var highlightState = message.ReadByte();

                var numberOfIconIdentifiers = message.ReadByte();
                for (var j = 0; j < numberOfIconIdentifiers; ++j)
                {
                    var iconName = message.ReadString();
                }

                var parentName = message.ReadString();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.StoreCategories);
            // TODO
        }
    }
}
