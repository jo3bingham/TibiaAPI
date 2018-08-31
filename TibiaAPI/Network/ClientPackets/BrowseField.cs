using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class BrowseField : ClientPacket
    {
        //public Position Position { get; set; }

        public BrowseField()
        {
            Type = ClientPacketType.BrowseField;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.BrowseField)
            {
                return false;
            }

            //Position = message.ReadPosition();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.BrowseField);
            //message.WritePosition(Position);
        }
    }
}
