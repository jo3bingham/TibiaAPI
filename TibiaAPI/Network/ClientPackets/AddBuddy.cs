using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class AddBuddy : ClientPacket
    {
        public string PlayerName { get; set; }

        public AddBuddy()
        {
            Type = ClientPacketType.AddBuddy;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            PlayerName = message.ReadString();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.AddBuddy);
            message.Write(PlayerName);
        }
    }
}
