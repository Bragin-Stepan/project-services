using _Project.Develop.Runtime.UI.Common;
using _Project.Develop.Runtime.UI.Core;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Screens.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] public IconTextView CoinsView { get; private set; }
        [field: SerializeField] public IconTextListView StatsView { get; private set; }
    }
}
