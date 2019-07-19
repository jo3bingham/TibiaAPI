using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class RequestPurchaseData : ServerPacket
    {
        public List<string> TournamentContinents { get; } = new List<string>();
        public List<string> TournamentTowns { get; } = new List<string>();

        public List<byte> TournamentVocations { get; } = new List<byte>();

        public string PlayerName { get; set; }

        public uint PurchaseData { get; set; }

        public byte RequestType { get; set; }

        public RequestPurchaseData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.RequestPurchaseData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            PurchaseData = message.ReadUInt32();
            RequestType = message.ReadByte();
            if (RequestType == 11) // Tournament Info
            {
                PlayerName = message.ReadString();
                TournamentContinents.Capacity = message.ReadByte();
                for (var i = 0; i < TournamentContinents.Capacity; ++i)
                {
                    TournamentContinents.Add(message.ReadString());
                }
                TournamentVocations.Capacity = message.ReadByte();
                for (var i = 0; i < TournamentVocations.Capacity; ++i)
                {
                    TournamentVocations.Add(message.ReadByte());
                }
                TournamentTowns.Capacity = message.ReadByte();
                for (var i = 0; i < TournamentTowns.Capacity; ++i)
                {
                    TournamentTowns.Add(message.ReadString());
                }
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.RequestPurchaseData);
            message.Write(PurchaseData);
            message.Write(RequestType);
            if (RequestType == 11)
            {
                message.Write(PlayerName);
                var count = Math.Min(TournamentContinents.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(TournamentContinents[i]);
                }
                count = Math.Min(TournamentVocations.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(TournamentVocations[i]);
                }
                count = Math.Min(TournamentTowns.Count, byte.MaxValue);
                message.Write((byte)count);
                for (var i = 0; i < count; ++i)
                {
                    message.Write(TournamentTowns[i]);
                }
            }
        }
    }
}
