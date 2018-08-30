using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class UseOnCreature : ClientPacket
    {
        //public Position Position { get; set; }

        public uint CreatureId { get; set; }

        public ushort ObjectId { get; set; }

        public byte StackPositionOrData { get; set; }

        public UseOnCreature()
        {
            Type = ClientPacketType.UseOnCreature;
        }

        public override bool ParseMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)Type)
            {
                return false;
            }

            //Position = message.ReadPosition();
            ObjectId = message.ReadUInt16();
            StackPositionOrData = message.ReadByte();
            CreatureId = message.ReadUInt32();
            return true;
        }

        public override void AppendToMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.UseOnCreature);
            //message.WritePosition(Position);
            message.Write(ObjectId);
            message.Write(StackPositionOrData);
            message.Write(CreatureId);
        }
    }
}
