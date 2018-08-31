using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class InviteToParty : ClientPacket
    {
        public uint PlayerId { get; set; }

        public InviteToParty()
        {
            Type = ClientPacketType.InviteToParty;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.InviteToParty)
            {
                return false;
            }

            PlayerId = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.InviteToParty);
            message.Write(PlayerId);
        }
    }
}
