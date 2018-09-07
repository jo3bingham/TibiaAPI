using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class OwnOffer : ServerPacket
    {
        public string PlayerName { get; set; }

        public OwnOffer()
        {
            PacketType = ServerPacketType.OwnOffer;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.OwnOffer)
            {
                return false;
            }

            PlayerName = message.ReadString();
            // TODO
            var tradeItemCount = message.ReadByte();
            for (var i = 0; i < tradeItemCount; ++i)
            {
                //message.ReadObjectInstance();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.OwnOffer);
            message.Write(PlayerName);
            // TODO
        }
    }
}
