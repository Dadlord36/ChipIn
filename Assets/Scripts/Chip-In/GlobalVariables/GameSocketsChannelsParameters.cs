namespace GlobalVariables
{
    public static class GameSocketsChannelsParameters
    {
        public const string ChipInHostUrl = "wss://chip-in-dev.herokuapp.com";
        public const string ChipInHostUrl2 = "wss://websocket-chip-in-dev.herokuapp.com";
        private const string ConstPortString = ":3334";
        public const string GameChannelName = "GameChannel";
    }

    public static class SlotsGameStatesNames
    {
        public const string RoundEnd = "Round End";
        public const string GameEnd = "Game End";
    }
}