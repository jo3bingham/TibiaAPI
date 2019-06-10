using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class StoreSuccess : ServerPacket
    {
        public string Text { get; set; }

        public int ConfirmedCreditBalance { get; set; }
        public int CurrentCreditBalance { get; set; }

        public byte ReasonType { get; set; }

        public StoreSuccess(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.StoreSuccess;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ReasonType = message.ReadByte();
            Text = message.ReadString();
            CurrentCreditBalance = message.ReadInt32();
            ConfirmedCreditBalance = message.ReadInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.StoreSuccess);
            message.Write(ReasonType);
            message.Write(Text);
            message.Write(CurrentCreditBalance);
            message.Write(ConfirmedCreditBalance);
        }
    }
}
