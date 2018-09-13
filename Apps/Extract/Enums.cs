namespace Extract
{
    class Enums
    {
        public enum FluidColors
        {
            FLUID_EMPTY = 0x00,
            FLUID_BLUE = 0x01,
            FLUID_RED = 0x02,
            FLUID_BROWN = 0x03,
            FLUID_GREEN = 0x04,
            FLUID_YELLOW = 0x05,
            FLUID_WHITE = 0x06,
            FLUID_PURPLE = 0x07
        }

        public enum FluidTypes
        {
            FLUID_NONE = FluidColors.FLUID_EMPTY,
            FLUID_WATER = FluidColors.FLUID_BLUE,
            FLUID_BLOOD = FluidColors.FLUID_RED,
            FLUID_BEER = FluidColors.FLUID_BROWN,
            FLUID_SLIME = FluidColors.FLUID_GREEN,
            FLUID_LEMONADE = FluidColors.FLUID_YELLOW,
            FLUID_MILK = FluidColors.FLUID_WHITE,
            FLUID_MANA = FluidColors.FLUID_PURPLE,

            FLUID_LIFE = FluidColors.FLUID_RED + 8,
            FLUID_OIL = FluidColors.FLUID_BROWN + 8,
            FLUID_URINE = FluidColors.FLUID_YELLOW + 8,
            FLUID_COCONUTMILK = FluidColors.FLUID_WHITE + 8,
            FLUID_WINE = FluidColors.FLUID_PURPLE + 8,

            FLUID_MUD = FluidColors.FLUID_BROWN + 16,
            FLUID_FRUITJUICE = FluidColors.FLUID_YELLOW + 16,

            FLUID_LAVA = FluidColors.FLUID_RED + 24,
            FLUID_RUM = FluidColors.FLUID_BROWN + 24,
            FLUID_SWAMP = FluidColors.FLUID_GREEN + 24,

            FLUID_TEA = FluidColors.FLUID_BROWN + 32,
            FLUID_MEAD = FluidColors.FLUID_BROWN + 40
        }
    }
}
