using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DepotTileState : ServerPacket
    {
        public byte State { get; set; }

        public DepotTileState()
        {
            PacketType = ServerPacketType.DepotTileState;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.DepotTileState)
            {
                return false;
            }

            State = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.DepotTileState);
            message.Write(State);
        }
    }
}
