using System.Collections.Generic;

using Newtonsoft.Json;

namespace OXGaming.TibiaAPI.Network
{
    /// <summary>
    /// The <see cref="LoginData"/> class is used to deserialize the JSON response
    /// from CipSoft's web service when the client requests a character list.
    /// </summary>
    public class LoginData
    {
        [JsonProperty("session")]
        public Session Session { get; set; }
        [JsonProperty("playdata")]
        public PlayData PlayData { get; set; }
    }

    /// <summary>
    /// The <see cref="Session"/> class is used to relevant data for a given login session.
    /// </summary>
    public class Session
    {
        [JsonProperty("sessionkey")]
        public string SessionKey { get; set; }
        [JsonProperty("lastlogintime")]
        public int LastLoginTime { get; set; }
        [JsonProperty("ispremium")]
        public bool IsPremium { get; set; }
        [JsonProperty("premiumuntil")]
        public int PremiumUntil { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("returnernotification")]
        public bool ReturnerNotification { get; set; }
        [JsonProperty("showrewardnews")]
        public bool ShowRewardNews { get; set; }
        [JsonProperty("isreturner")]
        public bool IsReturner { get; set; }
        [JsonProperty("fpstracking")]
        public bool FpsTracking { get; set; }
        [JsonProperty("optiontracking")]
        public bool OptionTracking { get; set; }
    }

    /// <summary>
    /// The <see cref="PlayData"/> class is used to store the characters, and their game worlds, of the
    /// account for a given login session.
    /// </summary>
    public class PlayData
    {
        [JsonProperty("worlds")]
        public List<World> Worlds { get; set; }
        [JsonProperty("characters")]
        public List<Character> Characters { get; set; }
    }

    /// <summary>
    /// The <see cref="World"/> class is used to store data (i.e., name, ip, port, etc.) for a game world.
    /// </summary>
    public class World
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("externaladdressprotected")]
        public string ExternalAddressProtected { get; set; }
        [JsonProperty("externalportprotected")]
        public int ExternalPortProtected { get; set; }
        [JsonProperty("externaladdressunprotected")]
        public string ExternalAddressUnprotected { get; set; }
        [JsonProperty("externalportunprotected")]
        public int ExternalPortUnprotected { get; set; }
        [JsonProperty("previewstate")]
        public int PreviewState { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("anticheatprotection")]
        public bool AntiCheatProtection { get; set; }
    }

    /// <summary>
    /// The <see cref="Character"/> class is used to store data (e.g., name) for each character of an account.
    /// </summary>
    public class Character
    {
        [JsonProperty("worldid")]
        public int WorldId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("ismale")]
        public bool IsMale { get; set; }
        [JsonProperty("tutorial")]
        public bool Tutorial { get; set; }
    }
}
