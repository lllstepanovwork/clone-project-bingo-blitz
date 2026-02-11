using TMPro;
using UnityEngine;

namespace BingoBlitzClone.Gameplay
{
    public class BingoFieldElementActiveState : BingoFieldElementState
    {
        [Header("Content")]
        [SerializeField] private TextMeshProUGUI numberText;
        
        public override void Enter()
        {
            gameObject.SetActive(true);
        }

        public override void SetNumber(int number)
        {
            numberText.text = number.ToString();
        }

        public override void Exit()
        {
            gameObject.SetActive(false);
        }
        
        public override void ResetState()
        {
            gameObject.SetActive(false);
        }
    }
}