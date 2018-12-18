using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class MoveCreature : ServerPacket
    {
        public Position FromPosition { get; set; }
        public Position ToPosition { get; set; }

        public uint CreatureId { get; set; }

        public byte StackPosition { get; set; }

        public MoveCreature(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.MoveCreature;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.MoveCreature)
            {
                return false;
            }

            var x = message.ReadUInt16();
            if (x != ushort.MaxValue)
            {
                FromPosition = message.ReadPosition(x);
                StackPosition = message.ReadByte();
            }
            else
            {
                CreatureId = message.ReadUInt32();
            }

            ToPosition = message.ReadPosition();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.MoveCreature);
            if (FromPosition.X != ushort.MaxValue)
            {
                message.Write(FromPosition);
                message.Write(StackPosition);
            }
            else
            {
                message.Write(ushort.MaxValue);
                message.Write(CreatureId);
            }
            message.Write(ToPosition);
        }
    }
}
