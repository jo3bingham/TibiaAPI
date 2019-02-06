using System;
using System.Collections.Generic;
using System.IO;

using OXGaming.TibiaAPI.Constants;

namespace OXGaming.TibiaAPI.Network.ClientPackets
{
    public class Login : ClientPacket
    {
        public List<uint> XteaKey { get; } = new List<uint>(4);

        public string CharacterName { get; set; }
        public string SessionKey { get; set; }

        public uint ChallengeTimeStamp { get; set; }
        public uint ClientVersion { get; set; }

        public ushort ClientType { get; set; }
        public ushort DatRevision { get; set; }
        public ushort ProtocolVersion { get; set; }

        public byte ChallengeRandom { get; set; }
        public byte ClientPreviewState { get; set; }

        public bool IsGameMaster { get; set; }

        public Login(Client client)
        {
            Client = client;
            PacketType = ClientPacketType.Login;
        }

        public override bool ParseFromNetworkMessage(NetworkMessage message)
        {
            if (message.ReadByte() != (byte)ClientPacketType.Login)
            {
                return false;
            }

            ClientType = message.ReadUInt16();
            ProtocolVersion = message.ReadUInt16();
            ClientVersion = message.ReadUInt32();
            DatRevision = message.ReadUInt16();
            ClientPreviewState = message.ReadByte();

            var rsaStartPosition = message.Position;
            if (message.ReadByte() != 0)
            {
                throw new Exception("[ClientPackets.Login.ParseFromNetworkMessage] RSA decryption failed.");
            }

            XteaKey.Clear();
            for (var i = 0; i < 4; ++i)
            {
                XteaKey.Add(message.ReadUInt32());
            }

            IsGameMaster = message.ReadBool();
            SessionKey = message.ReadString();
            CharacterName = message.ReadString();
            ChallengeTimeStamp = message.ReadUInt32();
            ChallengeRandom = message.ReadByte();

            // Skip the RSA encryption junk data.
            var rsaEndPosition = message.Position;
            message.Seek((int)(128 - (rsaStartPosition - rsaEndPosition)), SeekOrigin.Current);
            return true;
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Login);
            message.Write(ClientType);
            message.Write(ProtocolVersion);
            message.Write(ClientVersion);
            message.Write(DatRevision);
            message.Write(ClientPreviewState);
            message.Write((byte)0); // Start RSA block.

            if (XteaKey.Count != 4)
            {
                throw new Exception($"[ClientPackets.Login.ParseFromNetworkMessage] Invalid XTEA key length: {XteaKey.Count}");
            }

            foreach (var key in XteaKey)
            {
                message.Write(key);
            }

            message.Write(IsGameMaster);
            message.Write(SessionKey);
            message.Write(CharacterName);
            message.Write(ChallengeTimeStamp);
            message.Write(ChallengeRandom);
        }
    }
}
