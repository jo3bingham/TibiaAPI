using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class SellObject : ClientPacket
    {
        public ushort ObjectId { get; set; }

        public byte Amount { get; set; }
        public byte Data { get; set; }

        public bool KeepEquipped { get; set; }

        public SellObject()
        {
            Type = ClientPacketType.SellObject;
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
            KeepEquipped = message.ReadBool();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.SellObject);
            message.Write(ObjectId);
            message.Write(Data);
            message.Write(Amount);
            message.Write(KeepEquipped);
        }
    }
}
