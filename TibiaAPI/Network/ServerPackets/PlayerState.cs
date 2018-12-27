using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PlayerState : ServerPacket
    {
        public uint State { get; set; }

        public PlayerState(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PlayerState;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.PlayerState)
            {
                return false;
            }

            if (Client.VersionNumber >= 11400000)
            {
                State = message.ReadUInt32();
            }
            else
            {
                State = message.ReadUInt16();
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PlayerState);
            if (Client.VersionNumber >= 11400000)
            {
                message.Write(State);
            }
            else
            {
                message.Write((ushort)State);
            }
        }
    }
}
