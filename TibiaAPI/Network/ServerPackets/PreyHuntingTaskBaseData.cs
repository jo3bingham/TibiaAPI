using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PreyHuntingTaskBaseData : ServerPacket
    {
        public uint Unknown { get; set; }

        public PreyHuntingTaskBaseData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PreyHuntingTaskBaseData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Unknown = message.ReadUInt32();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.PreyHuntingTaskBaseData);
            message.Write(Unknown);
        }
    }
}
