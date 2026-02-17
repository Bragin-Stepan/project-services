using _Project.Develop.Runtime.UI.Common;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Features.Gameplay.Sequence;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Screens.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] public IconTextView CoinsView { get; private set; }
        [field: SerializeField] public IconTextListView StatsView { get; private set; }
        [field: SerializeField] public SequenceTilesListView SequenceListView { get; private set; }
    }
}
