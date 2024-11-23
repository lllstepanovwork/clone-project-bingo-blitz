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

    #region UI

    public enum UIBasePanelAnimationType
    {
        Fade,
        FromBottom,
        Scale
    }

    #endregion
    
    #region Gameplay


    public enum BingoSequenceTransparencyType
    {
        Full,
        Quarter,
        Half,
        Clear
    }

    public enum ComboCounterState
    {
        CounterState,
        RewardState,
        CooldownState
    }

    #endregion
}