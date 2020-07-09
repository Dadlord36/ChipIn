namespace GlobalVariables
{
    public static class GameRequestParameters
    {
        public const string Join = "join";
        public const string Match = "match";
        public const string Move = "move";

        public const string SpinFrame = "spin_frame";
        public const string SpinIcons = "spin_icons";

        public const string True = "true";
        public const string False = "false";
        
        public static string ConvertBoolToStringText(bool boolean)
        {
            return boolean ? True : False;
        }
    }
}