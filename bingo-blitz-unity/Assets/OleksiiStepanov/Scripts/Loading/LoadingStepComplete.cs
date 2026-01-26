using BingoBlitzClone.Game;
using Zenject;

namespace BingoBlitzClone.Loading
{
    public class LoadingStepComplete : LoadingStepBase
    {
        public override void Enter()
        {
            Exit();
        }

        public override LoadingStep GetStepType()
        {
            return LoadingStep.Complete;
        }
    }
}
