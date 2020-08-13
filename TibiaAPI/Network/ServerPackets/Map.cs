using System.Collections.Generic;

using OXGaming.TibiaAPI.Appearances;
using OXGaming.TibiaAPI.Constants;
using OXGaming.TibiaAPI.Utilities;

namespace OXGaming.TibiaAPI.Network.ServerPackets
{
    public class Map : ServerPacket
    {
        public List<(int TilesToSkip, List<ObjectInstance> Objects, Position Position)> Fields { get; } =
            new List<(int TilesToSkip, List<ObjectInstance> Objects, Position Position)>();

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            foreach (var (TilesToSkip, Objects, _) in Fields)
            {
                foreach (var obj in Objects)
                {
                    if (obj.Id == (int)CreatureInstanceType.UnknownCreature ||
                        obj.Id == (int)CreatureInstanceType.OutdatedCreature ||
                        obj.Id == (int)CreatureInstanceType.Creature)
                    {
                        var creature = Client.CreatureStorage.GetCreature(obj.Data);
                        if (creature == null)
                        {
                            continue;
                        }

                        message.Write((ushort)creature.InstanceType);
                        message.Write(creature, creature.InstanceType);
                    }
                    else
                    {
                        message.Write(obj);
                    }
                }

                message.Write((ushort)(TilesToSkip + 0xFF00));
            }
        }
    }
}
