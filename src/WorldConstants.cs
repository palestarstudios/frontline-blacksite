// This contains world and general utility constants for the game
// Stuff like movement, gravity, screen dimensions, so on...
//
// In the future we will separate the util ones into their own 
// config file the user can edit at their whim without rebuilding
//


namespace Blacksite.WorldConstants
{
    public static class Constants
    {
        public const float TickDt = 1f / 60f;
        public const float Gravity = 20.0f;
        public const float GroundAccel = 50.0f;
        public const float AirAccel = 20.0f;
        public const float MaxGroundSpeed = 7.0f;
        public const float MaxAirSpeed = 7.0f;
        public const float GroundFriction = 8.0f;
        public const int ScreenWidth = 1280;
        public const int ScreenHeight = 720;
        public const float JumpForce = 6.5f;
    }
}