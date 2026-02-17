using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.UI.Core;
using TMPro;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Features.Gameplay.Results
{
    public class WinPopupView : PopupViewBase
    {
        public event Action ContinueClicked;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private List<Transform> _stars;

        public void SetTitle(string title) => _title.text = title;

        public void OnContinueClick() => ContinueClicked?.Invoke();
    }
}