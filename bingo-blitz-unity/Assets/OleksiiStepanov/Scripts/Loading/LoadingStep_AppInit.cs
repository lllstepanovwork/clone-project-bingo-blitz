using OleksiiStepanov.Game;

namespace OleksiiStepanov.Loading
{
    public class LoadingStep_AppInit : LoadingStepBase
    {
        public override void Enter()
        {
            // Init framerate, resolution, etc.
            Exit();
        }

        public override LoadingStep GetStepType()
        {
            return LoadingStep.AppInit;
        }
    }
}
