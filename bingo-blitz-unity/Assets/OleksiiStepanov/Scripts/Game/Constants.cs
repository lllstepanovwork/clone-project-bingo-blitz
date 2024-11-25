using UnityEngine;

namespace OleksiiStepanov.Game
{
    public class Constants : MonoBehaviour
    {
        #region Scenes

        public const string LOADER_SCENE_NAME = "Loader";
        public const string MAIN_SCENE_NAME = "Main";

        #endregion

        #region TextMeshProSpriteShortcuts

        public const string TMP_SOFT_TEMP = "<sprite name=\"temp\">";

        #endregion

        #region ConsoleColors

        public const string CONSOLE_MESSAGE_COLOR_PINK = "<color=#EE77AE>";
        public const string CONSOLE_MESSAGE_COLOR_GREEN = "<color=#00B587>";
        public const string CONSOLE_MESSAGE_COLOR_RED = "<color=#EF413B>";
        public const string CONSOLE_MESSAGE_COLOR_BLUE = "<color=#00AAFF>";
        public const string CONSOLE_MESSAGE_COLOR_YELLOW = "<color=yellow>";
        public const string CONSOLE_MESSAGE_COLOR_END = "</color>";

        #endregion
        
        #region ConsoleColors

        public const string GAMEPLAY_MESSAGE_GO = "GO!";
        public const string GAMEPLAY_MESSAGE_YOU_WON = "YOU WON";
        public const string GAMEPLAY_MESSAGE_ROUND_OVER = "ROUND OVER";
        
        #endregion

        #region Gameplay

        public const float BINGO_SEQUENCE_FIRST_TIME = 0f;
        public const float BINGO_SEQUENCE_REPEAT_TIME = 4f;

        #endregion
        
        #region Links

        public const string BINGO_BLITZ_LINK = "https://www.bingoblitz.com/";
        public const string PLAYTIKA_LINK = "https://www.playtika.com/";
        public const string OLEKSII_STEPANOV_LINK = "https://www.linkedin.com/in/lllstepanov";

        #endregion
    }
}
