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

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.AddBuddy)
            {
                return false;
            }

            PlayerName = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.AddBuddy);
            message.Write(PlayerName);
        }
    }
}
