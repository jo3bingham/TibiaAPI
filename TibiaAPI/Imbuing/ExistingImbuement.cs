namespace OXGaming.TibiaAPI.Imbuing
{
    public class ExistingImbuement
    {
        public ImbuementData ImbuementData { get; set; }
        
        public uint ClearingGoldCost { get; set; }
        public uint RemainingDurationInSeconds { get; set; }

        public ExistingImbuement(ImbuementData imbuementData = null,
                                 uint remainingDurationInSeconds = 0,
                                 uint clearingGoldCost = 0)
        {
            ImbuementData = imbuementData;
            RemainingDurationInSeconds = remainingDurationInSeconds;
            ClearingGoldCost = clearingGoldCost;
        }
    }
}
