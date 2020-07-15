using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Highscores : ClientPacket
    {
        public Highscores(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Highscores;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            // TODO

            // 00 00 FF FF FF FF 00 00 01 00 14

            // FF FF FF FF may be vocation selection of `(all)`
            // 14 may be number of entries per page

            // 00 06 02 00 00 00 04 00 5A 75 6E 61 01 00 14 //switch to paladin

            // 00 05 02 00 00 00 04 00 5A 75 6E 61 01 00 14 //switch to distance

            // 00 05 02 00 00 00 06 00 41 6E 74 69 63 61 01 00 14 //switch to antica

            message.ReadByte(); //?
            message.ReadByte(); //category id
            message.ReadUInt32(); //vocation id
            message.ReadString(); //world
            message.ReadBytes(3); //?
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.Highscores);
        }
    }
}
