using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class AddBuddy : ClientPacket
    {
        public string PlayerName { get; set; }

        public AddBuddy(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.AddBuddy;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerName = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.AddBuddy);
            message.Write(PlayerName);
        }
    }
}
