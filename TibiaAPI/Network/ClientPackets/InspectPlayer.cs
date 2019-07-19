using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class InspectPlayer : ClientPacket
    {
        public uint PlayerId { get; set; }

        public byte InspectionState { get; set; }

        public InspectPlayer(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.InspectPlayer;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO: Determine inspection states.
            InspectionState = message.ReadByte();
            if (InspectionState <= 5)
            {
                PlayerId = message.ReadUInt32();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.InspectPlayer);
            message.Write(InspectionState);
            if (InspectionState <= 5)
            {
                message.Write(PlayerId);
            }
        }
    }
}
