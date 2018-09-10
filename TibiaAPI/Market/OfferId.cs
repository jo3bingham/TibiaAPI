namespace OXGaming.TibiaAPI.Market
{
    public class OfferId
    {
        public uint Counter { get; }
        public uint Timestamp { get; }

        public OfferId(uint timestamp, uint counter)
        {
            Timestamp = timestamp;
            Counter = counter;
        }
    }
}
