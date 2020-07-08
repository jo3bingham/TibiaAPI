using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class DepotTileState : ServerPacket
    {
        public byte State { get; set; }

        public DepotTileState(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.DepotTileState;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            State = message.ReadByte();
            if (Client.VersionNumber >= 12087995)
            {
                message.ReadByte();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.DepotTileState);
            // message.Write(State);
            // if (Client.VersionNumber >= 12087995)
            // {
            //     //message.Write(Unknown);
            // }
        }
    }
}
