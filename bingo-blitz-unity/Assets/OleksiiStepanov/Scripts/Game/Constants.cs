using UnityEngine;

namespace OleksiiStepanov.Game
{
    public class Constants : MonoBehaviour
    {
        #region Audio

        public const string AUDIO_NAME_CLICK = "sound_click";
        public const string AUDIO_NAME_CLICK_DENIAL = "sound_click_denial";
        public const string AUDIO_NAME_ADD_SOFT_CURRENCY = "sound_soft_currency";
        public const string AUDIO_NAME_KNOCK = "sound_knock";
        public const string AUDIO_NAME_WIN = "sound_win";
        public const string AUDIO_NAME_POP = "sound_pop";

        #endregion

        #region UserProfileData

        public const string SAVE_NAME = "express-delivery-apocalipsys-save-profile.json";

        #endregion

        #region Scenes

        public const string LOADER_SCENE_NAME = "Loader";
        public const string MAIN_SCENE_NAME = "Main";

        #endregion

        #region ComboTier

        public const string COMBO_TIER_1 = "Wow!";
        public const string COMBO_TIER_2 = "Great!";
        public const string COMBO_TIER_3 = "Awesome!";

        #endregion

        #region TextMeshProSpriteShortcuts

        public const string TMP_SOFT_CURRENCY = "<sprite name=\"coin\">";
        public const string TMP_STAR = "<sprite name=\"star\">";
        public const string TMP_MEDAL = "<sprite name=\"medal\">";
        public const string TMP_PIG = "<sprite name=\"pig\">";
        public const string TMP_BOMB = "<sprite name=\"pu-bomb\">";
        public const string TMP_SHUFFLE = "<sprite name=\"pu-shuffle\">";
        public const string TMP_HINT = "<sprite name=\"pu-hint\">";

        #endregion

        #region TextMeshProSpriteShortcuts

        public const string CHAPTER_TEXT = "Chapter";

        #endregion

        #region IAP

        public const string IAP_NO_ADS = "com.oleksiistepanov.mahjonghorizons.no_ads";
        public const string IAP_PRODUCT_1 = "com.oleksiistepanov.mahjonghorizons.soft_currency_01";
        public const string IAP_PRODUCT_2 = "com.oleksiistepanov.mahjonghorizons.soft_currency_02";
        public const string IAP_PRODUCT_3 = "com.oleksiistepanov.mahjonghorizons.soft_currency_03";
        public const string IAP_PRODUCT_4 = "com.oleksiistepanov.mahjonghorizons.soft_currency_04";

        #endregion

        #region ConsoleMessages

        public const string CONSOLE_MESSAGE_USER_PROFILE_SAVED = "Profile: Profile Saved";
        public const string CONSOLE_MESSAGE_USER_PROFILE_LOADED = "Profile: Profile Loaded";
        public const string CONSOLE_MESSAGE_NEW_USER_PROFILE_CREATED = "Profile: New Profile";
        public const string CONSOLE_MESSAGE_LOCALIZATION = "Localization";

        #endregion

        #region SortingLayerNames

        public const string SORTING_LAYER_KNOCK = "Knock";

        #endregion

        #region ConsoleMessages

        public const string CONSOLE_MESSAGE_COLOR_PINK = "<color=#EE77AE>";
        public const string CONSOLE_MESSAGE_COLOR_GREEN = "<color=#00B587>";
        public const string CONSOLE_MESSAGE_COLOR_RED = "<color=#EF413B>";
        public const string CONSOLE_MESSAGE_COLOR_BLUE = "<color=#00AAFF>";
        public const string CONSOLE_MESSAGE_COLOR_YELLOW = "<color=yellow>";
        public const string CONSOLE_MESSAGE_COLOR_END = "</color>";

        #endregion

        #region Gameplay

        public const float BINGO_SEQUENCE_FIRST_TIME = 1f;
        public const float BINGO_SEQUENCE_REPEAT_TIME = 3f;

        #endregion
    }
}
