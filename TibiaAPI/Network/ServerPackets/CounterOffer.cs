using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class CounterOffer : ServerPacket
    {
        public string PlayerName { get; set; }

        public CounterOffer()
        {
            PacketType = ServerPacketType.CounterOffer;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.CounterOffer)
            {
                return false;
            }

            PlayerName = message.ReadString();
            var tradeItemCount = message.ReadByte();
            for (var i = 0; i < tradeItemCount; ++i)
            {
                // TODO
                //message.ReadObjectInstance();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.CounterOffer);
            message.Write(PlayerName);
            // TODO
            //var count = (byte)Math.Max(TradeItems.Count byte.MaxValue);
            //message.WriteByte(count);
            //for (var i = 0; i < count; ++i)
            //{
            //    message.WriteObjectInstance(TradeItems[i]);
            //}
        }
    }
}
