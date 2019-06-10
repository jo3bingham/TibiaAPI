using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RestingAreaState : ServerPacket
    {
        public string Text { get; set; }

        public bool HasAnActiveBonus { get; set; }
        public bool IsInRestingArea { get; set; }

        public RestingAreaState(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.RestingAreaState;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            IsInRestingArea = message.ReadBool();
            HasAnActiveBonus = message.ReadBool();
            Text = message.ReadString();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RestingAreaState);
            message.Write(IsInRestingArea);
            message.Write(HasAnActiveBonus);
            message.Write(Text);
        }
    }
}
