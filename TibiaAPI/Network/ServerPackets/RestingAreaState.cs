﻿using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RestingAreaState : ServerPacket
    {
        public string Text { get; set; }

        public bool HasAnActiveBonus { get; set; }
        public bool IsInRestingArea { get; set; }

        public RestingAreaState(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.RestingAreaState;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.RestingAreaState)
            {
                return false;
            }

            IsInRestingArea = message.ReadBool();
            HasAnActiveBonus = message.ReadBool();
            Text = message.ReadString();
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RestingAreaState);
            message.Write(IsInRestingArea);
            message.Write(HasAnActiveBonus);
            message.Write(Text);
        }
    }
}
