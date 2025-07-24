using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameUi : BaseUi
    {
        [SerializeField] private Text titleText;

        private void OnEnable()
        {
            titleText.text = GameManager.Instance.CurrentGameOptions.ToString();
        }

    }
}