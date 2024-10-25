using OleksiiStepanov.Game;

namespace OleksiiStepanov.Loading
{
    public class LoadingStep_UserData : LoadingStepBase
    {
        public override void Enter()
        {
            Exit();
        }

        public override LoadingStep GetStepType()
        {
            return LoadingStep.UserData;
        }
    }
}
