using System;
using System.Collections.Generic;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class PreyHuntingTaskData : ServerPacket
    {
        public List<(ushort RaceId, bool IsUnlocked)> Selection { get; } =
            new List<(ushort RaceId, bool IsUnlocked)>();
        public List<(ushort RaceId, bool IsUnlocked)> ListSelection { get; } =
            new List<(ushort RaceId, bool IsUnlocked)>();

        public uint TimeLeftUntilFreeReroll { get; set; }

        public ushort CurrentKills { get; set; }
        public ushort RaceId { get; set; }
        public ushort RequiredKills { get; set; }

        public byte Index { get; set; }
        public byte Stars { get; set; }
        public byte State { get; set; }
        public byte UnlockOption { get; set; }

        public PreyHuntingTaskData(Client client)
        {
            Client = client;
            PacketType = ServerPacketType.PreyHuntingTaskData;
        }

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            Index = message.ReadByte();
            State = message.ReadByte();
            switch (State)
            {
                case 0:
                    {
                        UnlockOption = message.ReadByte();
                    }
                    break;
                case 1:
                    break;
                case 2:
                    {
                        Selection.Capacity = message.ReadUInt16();
                        for (var i = 0; i < Selection.Capacity; ++i)
                        {
                            var raceId = message.ReadUInt16();
                            var isUnlocked = message.ReadBool();
                            Selection.Add((raceId, isUnlocked));
                        }
                    }
                    break;
                case 3:
                    {
                        ListSelection.Capacity = message.ReadUInt16();
                        for (var i = 0; i < ListSelection.Capacity; ++i)
                        {
                            var raceId = message.ReadUInt16();
                            var isUnlocked = message.ReadBool();
                            ListSelection.Add((raceId, isUnlocked));
                        }
                    }
                    break;
                case 4:
                    {
                        RaceId = message.ReadUInt16();
                        // TODO
                        message.ReadByte();
                        RequiredKills = message.ReadUInt16();
                        CurrentKills = message.ReadUInt16();
                        Stars = message.ReadByte();
                    }
                    break;
                case 5:
                    {
                        RaceId = message.ReadUInt16();
                        // TODO
                        message.ReadByte();
                        RequiredKills = message.ReadUInt16();
                        CurrentKills = message.ReadUInt16();
                    }
                    break;
                default:
                    {
                        throw new Exception($"[PreyHuntingTaskData.ParseFromNetworkMessage] Unknown state: {State}");
                    }
            }
            if (Client.VersionNumber >= 125110194)
            {
                TimeLeftUntilFreeReroll = message.ReadUInt32();
            }
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            // TODO
            // message.Write((byte)ServerPacketType.PreyHuntingTaskData);
            // message.Write(Index);
            // message.Write(State);
            // switch (State)
            // {
            //     case 0:
            //         message.Write(UnlockOption);
            //         break;
            //     case 1:
            //         break;
            //     case 2:
            //         {
            //             var count = Math.Min(ushort.MaxValue, Selection.Capacity);
            //             message.Write((ushort)count);
            //             for (var i = 0; i < count; ++i)
            //             {
            //                 var (RaceId, IsUnlocked) = Selection[i];
            //                 message.Write(RaceId);
            //                 message.Write(IsUnlocked);
            //             }
            //         }
            //         break;
            //     case 3:
            //         {
            //             var count = Math.Min(ushort.MaxValue, ListSelection.Capacity);
            //             message.Write((ushort)count);
            //             for (var i = 0; i < count; ++i)
            //             {
            //                 var (RaceId, IsUnlocked) = ListSelection[i];
            //                 message.Write(RaceId);
            //                 message.Write(IsUnlocked);
            //             }
            //         }
            //         break;
            //     case 4:
            //         {
            //             message.Write(RaceId);
            //             //message.Write(Unknown);
            //             message.Write(RequiredKills);
            //             message.Write(CurrentKills);
            //             message.Write(Stars);
            //         }
            //         break;
            //     case 5:
            //         {
            //             message.Write(RaceId);
            //             //message.Write(Unknown);
            //             message.Write(RequiredKills);
            //             message.Write(CurrentKills);
            //         }
            //         break;
            //     default:
            //         {
            //             throw new Exception($"[PreyHuntingTaskData.AppendToNetworkMessage] Unknown state: {State}");
            //         }
            // }
        }
    }
}
