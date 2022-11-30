using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelActivenessController uiPanelController;
        [SerializeField] private GameOverPanelController gameOverPanelController;
        [SerializeField] private LevelPanelController levelPanelController;
        [SerializeField] private HighScorePanelController highScorePanelController;
        [SerializeField] private CollectablePanelController collectablePanelController;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetChangedText += levelPanelController.OnScoreUpdateText;
            UISignals.Instance.onSetChangedText += collectablePanelController.OnMoneyIncreased;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onRestartLevel += levelPanelController.OnRestartLevel;
            ScoreSignals.Instance.onScoreIncrease += levelPanelController.OnScoreUpdateText;
            ScoreSignals.Instance.onHighScore += highScorePanelController.UpdateText;
            ScoreSignals.Instance.onHitTarget += levelPanelController.OnHitTarget;
            ScoreSignals.Instance.onMissTarget += levelPanelController.OnResetComboCounter;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetChangedText -= levelPanelController.OnScoreUpdateText;
            UISignals.Instance.onSetChangedText -= collectablePanelController.OnMoneyIncreased;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onRestartLevel -= levelPanelController.OnRestartLevel;
            ScoreSignals.Instance.onScoreIncrease -= levelPanelController.OnScoreUpdateText;
            ScoreSignals.Instance.onHighScore -= highScorePanelController.UpdateText;
            ScoreSignals.Instance.onHitTarget -= levelPanelController.OnHitTarget;
            ScoreSignals.Instance.onMissTarget -= levelPanelController.OnResetComboCounter;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenMenu(panelParam);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.CloseMenu(panelParam);
        }

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnLevelFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.GameOverPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.CollectablePanel);

            gameOverPanelController.ShowThePanel();
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.WinPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void RestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.CollectablePanel);

        }

        public void PauseButton()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.PausePanel);
            Time.timeScale = 0f;
        }
        public void HighScoreButton()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.HighScorePanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
        }
        public void OptionsButton()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.OptionsPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
        }
        public void StoreButton()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StorePanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.CollectablePanel);
        }
    }
}