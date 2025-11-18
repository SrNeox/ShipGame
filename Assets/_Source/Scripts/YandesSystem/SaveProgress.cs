using _Source.Scripts.GameLogic.StaticClass.BankResources;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _Source.Scripts
{
    [RequireComponent(typeof(Button))]
    public class SaveProgress : MonoBehaviour
    {
        private Button _button;

        private void Awake() => _button = GetComponent<Button>();

        private void Start() => _button.onClick.AddListener(CompleteRound);

        private void CompleteRound()
        {
            if (YG2.reviewCanShow)
            {
                YG2.ReviewShow();
            }

            LocalData.TryChangeData();
        }
    }
}