using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class UseOnCreature : ClientPacket
    {
        public Position Position { get; set; }

        public uint CreatureId { get; set; }

        public ushort ObjectId { get; set; }

        public byte StackPositionOrData { get; set; }

        public UseOnCreature()
        {
            PacketType = ClientPacketType.UseOnCreature;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.UseOnCreature)
            {
                return false;
            }

            Position = message.ReadPosition();
            ObjectId = message.ReadUInt16();
            StackPositionOrData = message.ReadByte();
            CreatureId = message.ReadUInt32();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.UseOnCreature);
            message.Write(Position);
            message.Write(ObjectId);
            message.Write(StackPositionOrData);
            message.Write(CreatureId);
        }
    }
}
