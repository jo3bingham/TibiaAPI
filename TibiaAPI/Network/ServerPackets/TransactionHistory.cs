using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class TransactionHistory : ServerPacket
    {
        public List<(uint Timestamp, byte Type, int CreditChange, string Name)> Transactions { get; } =
            new List<(uint Timestamp, byte Type, int CreditChange, string Name)>();

        public uint CurrentPage { get; set; }
        public uint NumberOfPages { get; set; }

        public TransactionHistory()
        {
            PacketType = ServerPacketType.TransactionHistory;
        }

        public override bool ParseFromNetworkMessage(Client client, NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ServerPacketType.TransactionHistory)
            {
                return false;
            }

            CurrentPage = message.ReadUInt32();
            NumberOfPages = message.ReadUInt32();
            Transactions.Capacity = message.ReadByte();
            for (var i = 0; i < Transactions.Capacity; ++i)
            {
                var timestamp = message.ReadUInt32();
                var type = message.ReadByte();
                var creditChange = message.ReadInt32();
                var name = message.ReadString();
                Transactions.Add((timestamp, type, creditChange, name));
            }
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ServerPacketType.TransactionHistory);
            message.Write(CurrentPage);
            message.Write(NumberOfPages);
            var count = Math.Min(Transactions.Count, byte.MaxValue);
            message.Write((byte)count);
            for (var i = 0; i < count; ++i)
            {
                var transaction = Transactions[i];
                message.Write(transaction.Timestamp);
                message.Write(transaction.Type);
                message.Write(transaction.CreditChange);
                message.Write(transaction.Name);
            }
        }
    }
}
