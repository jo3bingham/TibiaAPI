using System;

using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace OXGaming.TibiaAPI.Network
{
    /// <summary>
    /// The <see cref="Rsa"/> class contains methods for encrypting and decrypting 128-byte blocks of data.
    /// </summary>
    /// <remarks>
    /// Decryption can only be done on data encrypted with the Open-Tibia private key.
    /// Encryption can be done on data with both the Open-Tibia and Tibia public keys.
    /// </remarks>
    public class Rsa
    {
        private const string Exponent = "65537";
        private const string OpenTibiaModulus = "109120132967399429278860960508995541528237502902798129123468757937266291492576446330739696001110603907230888610072655818825358503429057592827629436413108566029093628212635953836686562675849720620786279431090218017681061521755056710823876476444260558147179707119674283982419152118103759076030616683978566631413";
        private const string OpenTibiaP = "14299623962416399520070177382898895550795403345466153217470516082934737582776038882967213386204600674145392845853859217990626450972452084065728686565928113";
        private const string OpenTibiaQ = "7630979195970404721891201847792002125535401292779123937207447574596692788513647179235335529307251350570728407373705564708871762033017096809910315212884101";
        private const string OpenTibiaDP = "11141736698610418925078406669215087697114858422461871124661098818361832856659225315773346115219673296375487744032858798960485665997181641221483584094519937";
        private const string OpenTibiaDQ = "4886309137722172729208909250386672706991365415741885286554321031904881408516947737562153523770981322408725111241551398797744838697461929408240938369297973";
        private const string OpenTibiaInverseQ = "5610960212328996596431206032772162188356793727360507633581722789998709372832546447914318965787194031968482458122348411654607397146261039733584248408719418";
        private const string TibiaModulus = "132127743205872284062295099082293384952776326496165507967876361843343953435544496682053323833394351797728954155097012103928360786959821132214473291575712138800495033169914814069637740318278150290733684032524174782740134357629699062987023311132821016569775488792221429527047321331896351555606801473202394175817";

        private const int BlockSize = 128;

        private readonly RsaEngine openTibiaDecryptEngine = new RsaEngine();
        private readonly RsaEngine openTibiaEncryptEngine = new RsaEngine();
        private readonly RsaEngine tibiaEncryptEngine = new RsaEngine();

        /// <summary>
        /// Initializes a new instance of the <see cref="Rsa"/> class.
        /// </summary>
        public Rsa()
        {
            var openTibiaModulus = new BigInteger(OpenTibiaModulus);
            var tibiaModulus = new BigInteger(TibiaModulus);
            var exponent = new BigInteger(Exponent);
            var p = new BigInteger(OpenTibiaP);
            var q = new BigInteger(OpenTibiaQ);
            var dP = new BigInteger(OpenTibiaDP);
            var dQ = new BigInteger(OpenTibiaDQ);
            var qInv = new BigInteger(OpenTibiaInverseQ);

            var openTibiaRsaPrivateParameters = new RsaPrivateCrtKeyParameters(openTibiaModulus, exponent, exponent, p, q, dP, dQ, qInv);
            openTibiaDecryptEngine.Init(false, openTibiaRsaPrivateParameters);

            var openTibiaRsaPublicParameters = new RsaKeyParameters(false, openTibiaModulus, exponent);
            openTibiaEncryptEngine.Init(true, openTibiaRsaPublicParameters);

            var tibiaRsaPublicParamters = new RsaKeyParameters(false, tibiaModulus, exponent);
            tibiaEncryptEngine.Init(true, tibiaRsaPublicParamters);
        }

        /// <summary>
        /// Decrypts a 128-byte block of data using the Open-Tibia public key.
        /// </summary>
        /// <param name="message">The <see cref="NetworkMessage"/> containing the block of data to be decrypted.</param>
        /// <param name="index">Index of the underlying <see cref="NetworkMessage"/> buffer to start the decryption from.</param>
        public void OpenTibiaDecrypt(NetworkMessage message, int index) => ProcessBlock(message, index, openTibiaDecryptEngine);

        /// <summary>
        /// Decrypts a 128-byte block of data using the Tibia public key.
        /// </summary>
        /// <param name="message">The <see cref="NetworkMessage"/> containing the block of data to be decrypted.</param>
        /// <param name="index">Index of the underlying <see cref="NetworkMessage"/> buffer to start the decryption from.</param>
        public void OpenTibiaEncrypt(NetworkMessage message, int index) => ProcessBlock(message, index, openTibiaEncryptEngine);

        /// <summary>
        /// Encrypts a 128-byte block of data using the Open-Tibia private key.
        /// </summary>
        /// <param name="message">The <see cref="NetworkMessage"/> containing the block of data to be encrypted.</param>
        /// <param name="index">Index of the underlying <see cref="NetworkMessage"/> buffer to start the encryption from.</param>
        public void TibiaEncrypt(NetworkMessage message, int index) => ProcessBlock(message, index, tibiaEncryptEngine);

        /// <summary>
        /// Processes a 128-byte block of data using the given engine.
        /// </summary>
        /// <param name="message">The <see cref="NetworkMessage"/> containing the block of data to be processed.</param>
        /// <param name="index">Index of the underlying <see cref="NetworkMessage"/> buffer to start from.</param>
        /// <param name="engine">The RSA engine to use.</param>
        private static void ProcessBlock(NetworkMessage message, int index, RsaEngine engine)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "[Rsa.ProcessBlock] Index cannot be less than 0.");
            }

            try
            {
                var buffer = message.GetBuffer();
                var data = engine.ProcessBlock(buffer, index, BlockSize);
                Array.Clear(buffer, index, BlockSize);
                Array.Copy(data, 0, buffer, (index + (BlockSize - data.Length)), data.Length);
            }
            catch
            {
                throw;
            }
        }
    }
}
