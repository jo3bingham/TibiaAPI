using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class InspectionState : ServerPacket
    {
        public uint PlayerId { get; set; }

        public byte InspectionStateType { get; set; }

        public InspectionState(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.InspectionState;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PlayerId = message.ReadUInt32();
            InspectionStateType = message.ReadByte();
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.InspectionState);
            message.Write(PlayerId);
            message.Write(InspectionStateType);
        }
    }
}
