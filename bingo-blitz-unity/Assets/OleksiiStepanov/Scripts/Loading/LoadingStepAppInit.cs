using BingoBlitzClone.Game;

namespace BingoBlitzClone.Loading
{
    public class LoadingStepAppInit : LoadingStepBase
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
