using System;
using _Project.Develop.Runtime.Configs.Meta;
using _Project.Develop.Runtime.Logic.Meta.Features;
using _Project.Develop.Runtime.UI.Common;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utils.ReactiveManagement;

namespace _Project.Develop.Runtime.UI.Features.StatsProgression
{
    public class StatProgressPresenter : IPresenter
    {   
        private readonly IReadOnlyVariable<int> _stat;
        private readonly ProgressStatTypes _type;
        private readonly ProgressStatIconsConfigSO _iconsConfig;

        private readonly IconTextView _view;
        
        private IDisposable _disposable;
        
        public StatProgressPresenter(
            IReadOnlyVariable<int> stat,
            ProgressStatTypes type,
            ProgressStatIconsConfigSO iconsConfig,
            IconTextView view)
        {
            _iconsConfig = iconsConfig;
            _type = type;
            _stat = stat;
            _view = view;
        }
        
        public IconTextView View => _view;

        public void Initialize()
        {
            UpdateValue(_stat.Value);
            _view.SetIcon(_iconsConfig.GetSpriteFor(_type));
            
            _disposable = _stat.Subscribe(OnChanged);
        }

        private void OnChanged(int arg1, int newValue) => UpdateValue(newValue);

        public void Dispose()
        {
            _disposable.Dispose();
        }
        
        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}