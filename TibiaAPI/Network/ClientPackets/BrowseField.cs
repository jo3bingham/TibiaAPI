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

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            //Position = message.ReadPosition();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.BrowseField);
            //message.WritePosition(Position);
        }
    }
}
