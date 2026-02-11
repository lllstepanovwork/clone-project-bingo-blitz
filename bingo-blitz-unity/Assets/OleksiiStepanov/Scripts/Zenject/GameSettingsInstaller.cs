using System;
using UnityEngine;
using Zenject;

namespace BingoBlitzClone.Gameplay
{
    [CreateAssetMenu(menuName = "BingoBlitzClone/GameSettings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameRules GameRules;
        public CombinationInfo CombinationInfo;

        public override void InstallBindings()
        {
            Container.BindInstance(GameRules).IfNotBound();
            Container.BindInstance(CombinationInfo).IfNotBound();
        }
    }

    [Serializable]
    public class GameRules 
    {
        public float BingoBallAppearTime;
        public int MaxCombo;
    }
}