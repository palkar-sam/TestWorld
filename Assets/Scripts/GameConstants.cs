namespace Assets.Scripts
{
    public class GameConstants
    {

        public enum GameState
        {
            MainMenu,
            Options,
            Settings,
            Game
        }
    }

    public enum GameEvents
    {
        NONE = 0,
        ON_ROUND_STARTED,
        ON_ROUND_COMPLETE,
        ON_SHOW_VIEW,
        ON_HIDE_VIEW,
        ON_OPTION_SELECTED
    }

    public enum GameOptions
    {
        NONE = 0,
        OPTION_1x1,
        OPTION_2x2,
        OPTION_3x3,
        OPTION_4x4,
        OPTION_5x5
    }
}