using _Project.Develop.Runtime.UI.Core;
using TMPro;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Features.Gameplay.Sequence
{
    public class SequenceTileView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _text;
    
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _completedColor;

        private void Awake()
        {
            _text.color = _defaultColor;
        }

        public void SetText(string text) => _text.text = text;
    
        public void SetComplete() => _text.color = _completedColor;
    }
}