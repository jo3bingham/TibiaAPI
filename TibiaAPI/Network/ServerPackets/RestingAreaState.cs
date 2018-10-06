using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RestingAreaState : ServerPacket
    {
        public string Text { get; set; }

        public byte Unknown { get; set; }

        public bool IsInRestingArea { get; set; }

        public RestingAreaState()
        {
            PacketType = ServerPacketType.RestingAreaState;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.RestingAreaState)
            {
                return false;
            }

            IsInRestingArea = message.ReadBool();
            // TODO: Figure out this unknown.
            Unknown = message.ReadByte();
            Text = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RestingAreaState);
            message.Write(IsInRestingArea);
            message.Write(Unknown);
            message.Write(Text);
        }
    }
}
