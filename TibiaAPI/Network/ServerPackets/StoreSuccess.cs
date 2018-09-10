using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class StoreSuccess : ServerPacket
    {
        public string Text { get; set; }

        public int ConfirmedCreditBalance { get; set; }
        public int CurrentCreditBalance { get; set; }

        public byte Unknown { get; set; }

        public StoreSuccess()
        {
            PacketType = ServerPacketType.StoreSuccess;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.StoreSuccess)
            {
                return false;
            }

            Unknown = message.ReadByte(); // The Flash client reads this, but doesn't use it.
            Text = message.ReadString();
            CurrentCreditBalance = message.ReadInt32();
            ConfirmedCreditBalance = message.ReadInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.StoreSuccess);
            message.Write(Unknown);
            message.Write(Text);
            message.Write(CurrentCreditBalance);
            message.Write(ConfirmedCreditBalance);
        }
    }
}
