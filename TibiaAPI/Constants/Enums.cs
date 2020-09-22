namespace OXGaming.TibiaAPI.Constants
{
    public enum MarketOfferType
    {
        Buy = 0,
        Sell = 1
    }

    public enum PacketType
    {
        Client = 0,
        Server = 1
    }

    public enum MarketRequestType
    {
        OwnOffers = 65534,
        OwnHistory = 65535
    }

    public enum CreatureInstanceType
    {
        UnknownCreature = 97,
        OutdatedCreature = 98,
        Creature = 99
    }

    public enum OutfitWindowType
    {
        SelectOutfit = 0,
        TryOutfitMount = 1,
        TryMountOld = 2,
        TryHirelingDress = 3
    }

    public enum PreyActionType
    {
        ListReroll = 0,
        BonusReroll = 1,
        MonsterSelection = 2,
        Option = 5
    }

    public enum ReportType
    {
        Name = 0,
        Statement = 1,
        Bot = 2
    }

    public enum StoreOfferDisabledState
    {
        Active = 0,
        Disabled = 1,
        Hidden = 2
    }

    public enum BugCategory
    {
        Map = 0,
        Typo = 1,
        Technical = 2,
        Other = 3
    }

    public enum DeathType
    {
        Regular = 0,
        Unfair = 1,
        Blessed = 2,
        NoPenalty = 3
    }

    public enum LootContainerType
    {
        Add = 0,
        Remove = 1,
        Open = 2,
        UseMainContainerAsFallback = 3
    }

    public enum MarketOfferTerminationReason
    {
        Active = 0,
        Cancelled = 1,
        Expired = 2,
        Accepted = 3
    }

    public enum StoreOfferHighlightState
    {
        None = 0,
        New = 1,
        Sale = 2,
        Timed = 3
    }

    public enum ConnectionState
    {
        Disconnected = 0,
        ConnectingStage1 = 1,
        ConnectingStage2 = 2,
        Pending = 3,
        Game = 4
    }

    public enum CreatureType
    {
        Player = 0,
        Monster = 1,
        Npc = 2,
        PlayerSummon = 3,
        SummonOthers = 4
    }

    public enum GraphicalEffectsType
    {
        None = 0,
        Move = 1,
        Delay = 2,
        Effect = 3,
        MissileXY = 4,
        MissileYX = 5,
        Unknown = 0x83
    }

    public enum PreyDataState
    {
        Locked = 0,
        Inactive = 1,
        Active = 2,
        Selection = 3,
        SelectionChangeMonster = 4,
        ListSelection = 5,
        WildcardSelection = 6
    }

    public enum ResourceType
    {
        BankGold = 0,
        InventoryGold = 1,
        PreyBonusRerolls = 10,
        CollectionTokens = 20,
        CharmPoints = 30,
        TournamentCoins = 40
    }

    public enum FluidColor
    {
        Transparent = 0,
        Blue = 1,
        Red = 2,
        Brown = 3,
        Green = 4,
        Yellow = 5,
        White = 6,
        Purple = 7
    }

    public enum StoreServiceType
    {
        Unknown = 0,
        CharacterNameChange = 1,
        Premium = 2,
        Outfits = 3,
        Mounts = 4,
        Blessings = 5,
        XpBoost = 6,
        Prey = 7,
        TournamentTicket = 11
    }

    public enum Direction
    {
        None = 0,
        East = 1,
        NorthEast = 2,
        North = 3,
        NorthWest = 4,
        West = 5,
        SouthWest = 6,
        South = 7,
        SouthEast = 8
    }

    public enum CyclopediaMapDataType
    {
        MinimapMarker = 0,
        DiscoveryData = 1,
        ActiveRaid = 2,
        ImminentRaidMainArea = 3,
        ImminentRaidSubArea = 4,
        SetDiscoveryArea = 5,
        Passage = 6,
        SubAreaMonsters = 7,
        MonsterBestiary = 8,
        Donations = 9,
        SetCurrentArea = 10
    }

    public enum FriendSystemDataType
    {
        SpecialEvent = 0,
        InvitationPending = 1,
        Friends = 2,
        Invitations = 3,
        Blacklist = 4,
        CharacterSearch = 5,
        Badges = 6,
        NewFriend = 7,
        Config = 8
    }

    public enum FriendGroup
    {
        Others = 0,
        Contacts = 1,
        Friends = 2,
        CloseFriends = 3
    }

    public enum MarketDetailField
    {
        Armor = 0,
        Attack = 1,
        Capacity = 2,
        Defence = 3,
        Description = 4,
        Expire = 5,
        Protection = 6,
        RestrictLevel = 7,
        RestrictMagicLevel = 8,
        RestrictProfession = 9,
        RuneSpell = 10,
        SkillBoost = 11,
        Uses = 12,
        WeaponType = 13,
        Weight = 14,
        ImbuementSlots = 15
    }

    public enum FluidType
    {
        None = 0,
        Water = 1,
        Wine = 2,
        Beer = 3,
        Mud = 4,
        Blood = 5,
        Slime = 6,
        Oil = 7,
        Urine = 8,
        Milk = 9,
        ManaFluid = 10,
        LifeFluid = 11,
        Lemonade = 12,
        Rum = 13,
        FruitJuice = 14,
        CoconutMilk = 15,
        Mead = 16,
        Tea = 17
    }

    public enum MessageModeType
    {
        None = 0,
        Say = 1,
        Whisper = 2,
        Yell = 3,
        PrivateFrom = 4,
        PrivateTo = 5,
        ChannelManagement = 6,
        Channel = 7,
        ChannelHighlight = 8,
        Spell = 9,
        NpcFromStartBlock = 10,
        NpcFrom = 11,
        NpcTo = 12,
        GamemasterBroadcast = 13,
        GamemasterChannel = 14,
        GamemasterPrivateFrom = 15,
        GamemasterPrivateTo = 16,
        Login = 17,
        Admin = 18,
        Game = 19,
        GameHighlight = 20,
        Failure = 21,
        Look = 22,
        DamageDealed = 23,
        DamageReceived = 24,
        Heal = 25,
        Exp = 26,
        DamageOthers = 27,
        HealOthers = 28,
        ExpOthers = 29,
        Status = 30,
        Loot = 31,
        TradeNpc = 32,
        Guild = 33,
        PartyManagement = 34,
        Party = 35,
        BarkLow = 36,
        BarkLoud = 37,
        Report = 38,
        HotkeyUse = 39,
        TutorialHint = 40,
        ThankYou = 41,
        Market = 42,
        Mana = 43,
        BeyondLast = 44,
        Attention = 48,
        BoostedCreature = 49,
        OfflineTraining = 50,
        Transaction = 51,
        Potion = 52
    }

    public enum ClientPacketType
    {
        Invalid = 0x00,
        Login = 0x0A,
        SecondaryLogin = 0x0B,
        EnterWorld = 0x0F,
        QuitGame = 0x14,
        ConnectionPingBack = 0x1C,
        Ping = 0x1D,
        PingBack = 0x1E,
        PerformanceMetrics = 0x1F,
        StashAction = 0x28,
        DepotSearchRetrieve = 0x29,
        TrackBestiaryRace = 0x2A,
        PartyHuntAnalyser = 0x2B,
        TeamFinderAssembleTeam = 0x2C,
        TeamFinderJoinTeam = 0x2D,
        ClientCheck = 0x63,
        GoPath = 0x64,
        GoNorth = 0x65,
        GoEast = 0x66,
        GoSouth = 0x67,
        GoWest = 0x68,
        Stop = 0x69,
        GoNorthEast = 0x6A,
        GoSouthEast = 0x6B,
        GoSouthWest = 0x6C,
        GoNorthWest = 0x6D,
        RotateNorth = 0x6F,
        RotateEast = 0x70,
        RotateSouth = 0x71,
        RotateWest = 0x72,
        Teleport = 0x73,
        CharacterTradeConfigurationAction = 0x76,
        EquipObject = 0x77,
        MoveObject = 0x78,
        LookNpcTrade = 0x79,
        BuyObject = 0x7A,
        SellObject = 0x7B,
        CloseNpcTrade = 0x7C,
        TradeObject = 0x7D,
        LookTrade = 0x7E,
        AcceptTrade = 0x7F,
        RejectTrade = 0x80,
        FriendSystemAction = 0x81,
        UseObject = 0x82,
        UseTwoObjects = 0x83,
        UseOnCreature = 0x84,
        TurnObject = 0x85,
        CloseContainer = 0x87,
        UpContainer = 0x88,
        EditText = 0x89,
        EditList = 0x8A,
        ToggleWrapState = 0x8B,
        Look = 0x8C,
        LookAtCreature = 0x8D,
        JoinAggression = 0x8E,
        QuickLoot = 0x8F,
        LootContainer = 0x90,
        QuickLootBlackWhitelist = 0x91,
        OpenDepotSearch = 0x92,
        CloseDepotSearch = 0x93,
        DepotSearchType = 0x94,
        OpenParentContainer = 0x95,
        Talk = 0x96,
        GetChannels = 0x97,
        JoinChannel = 0x98,
        LeaveChannel = 0x99,
        PrivateChannel = 0x9A,
        GuildMessage = 0x9B,
        EditGuildMessage = 0x9C,
        CloseNpcChannel = 0x9E,
        SetTactics = 0xA0,
        Attack = 0xA1,
        Follow = 0xA2,
        InviteToParty = 0xA3,
        JoinParty = 0xA4,
        RevokeInvitation = 0xA5,
        PassLeadership = 0xA6,
        LeaveParty = 0xA7,
        ShareExperience = 0xA8,
        DisbandParty = 0xA9,
        OpenChannel = 0xAA,
        InviteToChannel = 0xAB,
        ExcludeFromChannel = 0xAC,
        CyclopediaHouseAction = 0xAD,
        Highscores = 0xB1,
        PreyHuntingTaskAction = 0xBA,
        Cancel = 0xBE,
        ClaimTournamentReward = 0xC3,
        TournamentInformation = 0xC4,
        SubscribeToUpdates = 0xC6,
        TournamentLeaderboard = 0xC7,
        TournamentTicketAction = 0xC8,
        GetTransactionDetails = 0xC9,
        UpdateExivaOptions = 0xCA,
        BrowseField = 0xCB,
        SeekInContainer = 0xCC,
        InspectObject = 0xCD,
        InspectPlayer = 0xCE,
        BlessingsDialog = 0xCF,
        TrackQuestflags = 0xD0,
        MarketStatistics = 0xD1,
        GetOutfit = 0xD2,
        SetOutfit = 0xD3,
        Mount = 0xD4,
        ApplyImbuement = 0xD5,
        ApplyClearingCharm = 0xD6,
        ClosedImbuingDialog = 0xD7,
        OpenRewardWall = 0xD8,
        DailyRewardHistory = 0xD9,
        CollectDailyReward = 0xDA,
        CyclopediaMapAction = 0xDB,
        AddBuddy = 0xDC,
        RemoveBuddy = 0xDD,
        EditBuddy = 0xDE,
        BuddyGroup = 0xDF,
        MarkGameNewsAsRead = 0xE0,
        OpenMonsterCyclopedia = 0xE1,
        OpenMonsterCyclopediaMonsters = 0xE2,
        OpenMonsterCyclopediaRace = 0xE3,
        MonsterBonusEffectAction = 0xE4,
        OpenCyclopediaCharacterInfo = 0xE5,
        BugReport = 0xE6,
        ThankYou = 0xE7,
        GetOfferDescription = 0xE8,
        StoreEvent = 0xE9,
        FeatureEvent = 0xEA,
        PreyAction = 0xEB,
        SetHirelingName = 0xEC,
        RequestResourceBalance = 0xED,
        Greet = 0xEE,
        TransferCurrency = 0xEF,
        GetQuestLog = 0xF0,
        GetQuestLine = 0xF1,
        RuleViolationReport = 0xF2,
        GetObjectInfo = 0xF3,
        MarketLeave = 0xF4,
        MarketBrowse = 0xF5,
        MarketCreate = 0xF6,
        MarketCancel = 0xF7,
        MarketAccept = 0xF8,
        AnswerModalDialog = 0xF9,
        OpenIngameShop = 0xFA,
        RequestShopOffers = 0xFB,
        BuyIngameShopOffer = 0xFC,
        OpenTransactionHistory = 0xFD,
        GetTransactionHistory = 0xFE
    }

    public enum ServerPacketType
    {
        Invalid = 0x00,
        CreatureData = 0x03,
        SessionDumpStart = 0x04,
        PendingStateEntered = 0x0A,
        ReadyForSecondaryConnection = 0x0B,
        WorldEntered = 0x0F,
        LoginError = 0x14,
        LoginAdvice = 0x15,
        LoginWait = 0x16,
        LoginSuccess = 0x17,
        StoreButtonIndicators = 0x19,
        Ping = 0x1D,
        PingBack = 0x1E,
        LoginChallenge = 0x1F,
        Dead = 0x28,
        Stash = 0x29,
        //DepotTileState = 0x2A,
        SpecialContainersAvailable = 0x2A,
        PartyHuntAnalyser = 0x2B,
        //SpecialContainersAvailable = 0x2C,
        TeamFinderTeamLeader = 0x2C,
        TeamFinderTeamMember = 0x2D,
        ClientCheck = 0x63,
        FullMap = 0x64,
        TopRow = 0x65,
        RightColumn = 0x66,
        BottomRow = 0x67,
        LeftColumn = 0x68,
        FieldData = 0x69,
        CreateOnMap = 0x6A,
        ChangeOnMap = 0x6B,
        DeleteOnMap = 0x6C,
        MoveCreature = 0x6D,
        Container = 0x6E,
        CloseContainer = 0x6F,
        CreateInContainer = 0x70,
        ChangeInContainer = 0x71,
        DeleteInContainer = 0x72,
        FriendSystemData = 0x74,
        ScreenshotEvent = 0x75,
        InspectionList = 0x76,
        InspectionState = 0x77,
        SetInventory = 0x78,
        DeleteInventory = 0x79,
        NpcOffer = 0x7A,
        PlayerGoods = 0x7B,
        CloseNpcTrade = 0x7C,
        OwnOffer = 0x7D,
        CounterOffer = 0x7E,
        CloseTrade = 0x7F,
        CharacterTradeConfiguration = 0x80,
        Ambiente = 0x82,
        GraphicalEffects = 0x83,
        RemoveGraphicalEffect = 0x84,
        MissileEffect = 0x85,
        Trappers = 0x87,
        CreatureUpdate = 0x8B,
        CreatureHealth = 0x8C,
        CreatureLight = 0x8D,
        CreatureOutfit = 0x8E,
        CreatureSpeed = 0x8F,
        CreatureSkull = 0x90,
        CreatureParty = 0x91,
        CreatureUnpass = 0x92,
        CreatureMarks = 0x93,
        //CreaturePvpHelpers = 0x94,
        DepotSearchResults = 0x94,
        CreatureType = 0x95,
        EditText = 0x96,
        EditList = 0x97,
        ShowGameNews = 0x98,
        DepotSearchDetailList = 0x99,
        CloseDepotSearch = 0x9A,
        BlessingsDialog = 0x9B,
        Blessings = 0x9C,
        SwitchPreset = 0x9D,
        PremiumTrigger = 0x9E,
        PlayerDataBasic = 0x9F,
        PlayerDataCurrent = 0xA0,
        PlayerSkills = 0xA1,
        PlayerState = 0xA2,
        ClearTarget = 0xA3,
        SpellDelay = 0xA4,
        SpellGroupDelay = 0xA5,
        MultiUseDelay = 0xA6,
        SetTactics = 0xA7,
        SetStoreButtonDeeplink = 0xA8,
        RestingAreaState = 0xA9,
        Talk = 0xAA,
        Channels = 0xAB,
        OpenChannel = 0xAC,
        PrivateChannel = 0xAD,
        EditGuildMessage = 0xAE,
        Highscores = 0xB1,
        OpenOwnChannel = 0xB2,
        CloseChannel = 0xB3,
        Message = 0xB4,
        SnapBack = 0xB5,
        Wait = 0xB6,
        UnjustifiedPoints = 0xB7,
        PvpSituations = 0xB8,
        BestiaryTracker = 0xB9,
        PreyHuntingTaskBaseData = 0xBA,
        PreyHuntingTaskData = 0xBB,
        TopFloor = 0xBE,
        BottomFloor = 0xBF,
        UpdateLootContainers = 0xC0,
        PlayerDataTournament = 0xC1,
        CyclopediaHouseActionResult = 0xC3,
        TournamentInformation = 0xC4,
        TournamentLeaderboard = 0xC5,
        CyclopediaStaticHouseData = 0xC6,
        CyclopediaCurrentHouseData = 0xC7,
        Outfit = 0xC8,
        ExivaSuppressed = 0xC9,
        UpdateExivaOptions = 0xCA,
        TransactionDetails = 0xCB,
        ImpactTracking = 0xCC,
        MarketStatistics = 0xCD,
        ItemWasted = 0xCE,
        ItemLooted = 0xCF,
        TrackQuestflags = 0xD0,
        KillTracking = 0xD1,
        BuddyData = 0xD2,
        BuddyStatusChange = 0xD3,
        BuddyGroupData = 0xD4,
        MonsterCyclopedia = 0xD5,
        MonsterCyclopediaMonsters = 0xD6,
        MonsterCyclopediaRace = 0xD7,
        MonsterCyclopediaBonusEffects = 0xD8,
        MonsterCyclopediaNewDetails = 0xD9,
        CyclopediaCharacterInfo = 0xDA,
        HirelingNameChange = 0xDB,
        TutorialHint = 0xDC,
        //AutomapFlag = 0xDD,
        CyclopediaMapData = 0xDD,
        DailyRewardCollectionState = 0xDE,
        CreditBalance = 0xDF,
        IngameShopError = 0xE0,
        RequestPurchaseData = 0xE1,
        OpenRewardWall = 0xE2,
        CloseRewardWall = 0xE3,
        DailyRewardBasic = 0xE4,
        DailyRewardHistory = 0xE5,
        PreyFreeListRerollAvailability = 0xE6,
        PreyTimeLeft = 0xE7,
        PreyData = 0xE8,
        PreyPrices = 0xE9,
        OfferDescription = 0xEA,
        ImbuingDialogRefresh = 0xEB,
        CloseImbuingDialog = 0xEC,
        ShowMessageDialog = 0xED,
        RequestResourceBalance = 0xEE,
        TibiaTime = 0xEF,
        QuestLog = 0xF0,
        QuestLine = 0xF1,
        UpdatingShopBalance = 0xF2,
        ChannelEvent = 0xF3,
        ObjectInfo = 0xF4,
        PlayerInventory = 0xF5,
        MarketEnter = 0xF6,
        MarketLeave = 0xF7,
        MarketDetail = 0xF8,
        MarketBrowse = 0xF9,
        ShowModalDialog = 0xFA,
        StoreCategories = 0xFB,
        StoreOffers = 0xFC,
        TransactionHistory = 0xFD,
        StoreSuccess = 0xFE
    }
}
