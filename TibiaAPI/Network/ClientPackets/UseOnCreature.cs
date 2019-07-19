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

        public UseOnCreature(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.UseOnCreature;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Position = message.ReadPosition();
            ObjectId = message.ReadUInt16();
            StackPositionOrData = message.ReadByte();
            CreatureId = message.ReadUInt32();
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
