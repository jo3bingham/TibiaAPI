using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class StashAction : ClientPacket
    {
        public Position Position { get; set; }

        public uint ItemCount { get; set; }

        public ushort ItemId { get; set; }

        public byte StashType { get; set; }

        public StashAction(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.StashAction;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            StashType = message.ReadByte();
            // TODO: Are there more stash types? 3 = retrieve?
            if (StashType == 0 || StashType == 1 || StashType == 2)
            {
                // This hasn't been validated as 100% accurate.
                // Based on the following data: 01 FF FF 40 00 0A 3D 17 0A
                Position = message.ReadPosition();
                ItemId = message.ReadUInt16();
                message.ReadByte();
                if (StashType == 0)
                {
                    message.ReadByte();
                }
            }
            else if (StashType == 3)
            {
                ItemId = message.ReadUInt16();
                ItemCount = message.ReadUInt32();
                if (Client.VersionNumber >= 12200000)
                {
                    message.ReadByte();
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ClientPacketType.StashAction);
            // message.Write(StashType);
            // if (StashType == 0 || StashType == 1 || StashType == 2)
            // {
            //     message.Write(Position);
            //     message.Write(ItemId);
            //     //message.Write(Unknown);
            //     if (StashType == 0)
            //     {
            //         //message.Write(Unknown);
            //     }
            // }
            // else if (StashType == 3)
            // {
            //     message.Write(ItemId);
            //     message.Write(ItemCount);
            //     if (Client.VersionNumber >= 12200000)
            //     {
            //         //message.Write(Unknown);
            //     }
            // }
        }
    }
}
