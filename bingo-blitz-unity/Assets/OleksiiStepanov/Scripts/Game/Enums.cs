
namespace OleksiiStepanov.Game
{
    #region AppLoader

    public enum LoadingStep
    {
        None,
        AppInit,
        AddressablesInit,
        FacebookInit,
        UIInit,
        ServerInit,
        UserLogin,
        UserData,
        GameData,
        Complete
    }

    #endregion

    #region UIBasePanelAnimationType

    public enum UIBasePanelAnimationType
    {
        Fade,
        FromBottom,
        Scale
    }

    #endregion

    #region Shop

    public enum ItemType
    {
        SoftCurrency,
        Bomb,
        Hint,
        Shuffle
    }

    #endregion

    #region Bank

    public enum BankPurchaseType
    {
        NoAds,
        Product1,
        Product2,
        Product3,
        Product4
    }

    #endregion

    #region Weather

    public enum WeatherType
    {
        NoWeather,
        LightRain,
        HeavyRain,
        LightSnow,
        HeavySnow,
        GoldenDust,
        Fog
    }

    #endregion

    #region LocalizationType

    public enum LocalizationType
    {
        English = 0,
        German = 1,
        Spanish = 2,
        French = 3,
        Default = 99
    }

    #endregion

    #region Gameplay

    #endregion
}