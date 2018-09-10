using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class InspectionState : ServerPacket
    {
        public uint PlayerId { get; set; }

        public byte InspectionStateType { get; set; }

        public InspectionState()
        {
            PacketType = ServerPacketType.InspectionState;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.InspectionState)
            {
                return false;
            }

            PlayerId = message.ReadUInt32();
            InspectionStateType = message.ReadByte();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.InspectionState);
            message.Write(PlayerId);
            message.Write(InspectionStateType);
        }
    }
}
