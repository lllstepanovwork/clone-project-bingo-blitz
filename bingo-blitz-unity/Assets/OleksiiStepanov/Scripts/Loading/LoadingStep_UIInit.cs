using OleksiiStepanov.Game;

namespace OleksiiStepanov.Loading
{
    public class LoadingStep_UIInit : LoadingStepBase
    {
        public override void Enter()
        {
            Exit();
        }

        public override LoadingStep GetStepType()
        {
            return LoadingStep.UIInit;
        }
    }
}
