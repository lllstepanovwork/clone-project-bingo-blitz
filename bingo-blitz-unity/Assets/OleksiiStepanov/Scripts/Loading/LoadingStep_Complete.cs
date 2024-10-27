using OleksiiStepanov.App;
using OleksiiStepanov.Game;

namespace OleksiiStepanov.Loading
{
    public class LoadingStep_Complete : LoadingStepBase
    {
        public override void Enter()
        {
            AppLoader.Instance.LoaderCanvas.SetActive(false);
            Exit();
        }

        public override LoadingStep GetStepType()
        {
            return LoadingStep.Complete;
        }
    }
}
