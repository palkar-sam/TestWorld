using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameUi : BaseUi
    {
        [SerializeField] private Text titleText;
        [SerializeField] private Text scoreText;
        [SerializeField] private Text userClickCountText;
        [SerializeField] private BoardUi boardUi;

        private void Start()
        {
            boardUi.OnScoreUpdate += OnScoreUpdate;
        }

        private void OnEnable()
        {
            titleText.text = GameManager.Instance.CurrentGameOptions.ToString();
            scoreText.text = $"Collected : {0}";
            userClickCountText.text = $"Clicks : {0}";
        }

        private void OnScoreUpdate(int collectCount, int userClickCount)
        {
            scoreText.text = $"Collected : {collectCount}";
            userClickCountText.text = $"Clicks : {userClickCount}";
        }

        public override void init<T>(T model)
        {
            throw new System.NotImplementedException();
        }
    }
}