using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BuyObject : ClientPacket
    {
        public ushort ObjectId { get; set; }

        public byte Amount { get; set; }
        public byte Data { get; set; }

        public bool IgnoreCapacity { get; set; }
        public bool WithBackpacks { get; set; }

        public BuyObject()
        {
            Type = ClientPacketType.BuyObject;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            ObjectId = message.ReadUInt16();
            Data = message.ReadByte();
            Amount = message.ReadByte();
            IgnoreCapacity = message.ReadBool();
            WithBackpacks = message.ReadBool();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.BuyObject);
            message.Write(ObjectId);
            message.Write(Data);
            message.Write(Amount);
            message.Write(IgnoreCapacity);
            message.Write(WithBackpacks);
        }
    }
}
