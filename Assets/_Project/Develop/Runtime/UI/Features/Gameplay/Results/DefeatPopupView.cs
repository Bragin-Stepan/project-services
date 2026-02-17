using System;
using _Project.Develop.Runtime.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.Features.Gameplay.Results
{
    public class DefeatPopupView : PopupViewBase
    {
        public event Action ExitClicked;
        public event Action RestartClicked;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _restartButton;

        public void SetTitle(string title) => _title.text = title;

        protected override void OnPreShow()
        {
            base.OnPreShow();

            _exitButton.onClick.AddListener(OnExitButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked() => RestartClicked?.Invoke();

        private void OnExitButtonClicked() => ExitClicked?.Invoke();
    }
}