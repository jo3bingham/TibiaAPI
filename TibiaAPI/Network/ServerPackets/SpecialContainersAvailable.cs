using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class SpecialContainersAvailable : ServerPacket
    {
        uint Unknown { get; set; }

        public SpecialContainersAvailable(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.SpecialContainersAvailable;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Unknown = message.ReadUInt32();
            Client.Logger.Debug($"Unknown: {System.BitConverter.ToString(System.BitConverter.GetBytes(Unknown)).Replace('-', ' ')}");
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.SpecialContainersAvailable);
            message.Write(Unknown);
        }
    }
}
