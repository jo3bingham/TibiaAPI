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

        public BuyObject(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.BuyObject;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.BuyObject)
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

        public override void AppendToNetworkMessage(NetworkMessage message)
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
