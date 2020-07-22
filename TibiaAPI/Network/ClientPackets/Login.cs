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
        public string Version { get; set; }

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

        public override void ParseFromNetworkMessage(NetworkMessage message)
        {
            ClientType = message.ReadUInt16();
            ProtocolVersion = message.ReadUInt16();
            ClientVersion = message.ReadUInt32();

            if (Client.VersionNumber >= 124010030)
            {
                Version = message.ReadString();
            }

            DatRevision = message.ReadUInt16();
            ClientPreviewState = message.ReadByte();

            if (message.ReadByte() != 0)
            {
                throw new Exception("[ClientPackets.Login.ParseFromNetworkMessage] RSA decryption failed.");
            }

            XteaKey.Clear();
            for (var i = 0; i < 4; ++i)
            {
                XteaKey.Add(message.ReadUInt32());
            }
            Client.Connection.SetXteaKey(XteaKey);

            IsGameMaster = message.ReadBool();
            SessionKey = message.ReadString();
            CharacterName = message.ReadString();
            ChallengeTimeStamp = message.ReadUInt32();
            ChallengeRandom = message.ReadByte();

            // Skip the RSA encryption junk data.
            message.Seek((int)message.Size, SeekOrigin.Begin);
        }

        public override void AppendToNetworkMessage(NetworkMessage message)
        {
            message.Write((byte)ClientPacketType.Login);
            message.Write(ClientType);
            message.Write(ProtocolVersion);
            message.Write(ClientVersion);

            if (Client.VersionNumber >= 124010030)
            {
                message.Write(Version);
            }

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
