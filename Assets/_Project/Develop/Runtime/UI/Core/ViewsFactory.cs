using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Develop.Runtime.UI.Core
{
    public class ViewsFactory
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;

        private readonly Dictionary<Type, string> _uiPaths = new (PathToResources.UIPaths);

        public ViewsFactory(ResourcesAssetsLoader resourcesAssetsLoader)
        {
            _resourcesAssetsLoader = resourcesAssetsLoader;
        }
        
        public TView Create<TView>(Transform parent = null) where TView : MonoBehaviour, IView
        {
            if (_uiPaths.TryGetValue(typeof(TView), out string resourcePath) == false)
                throw new KeyNotFoundException($"[ViewsFactory] Path for {typeof(TView)} not found");

            GameObject prefab = _resourcesAssetsLoader.Load<GameObject>(resourcePath);
            GameObject instance = Object.Instantiate(prefab, parent);
            TView view = instance.GetComponent<TView>();

            if (view == null)
                throw new InvalidOperationException($"Not found {typeof(TView)} component on view instance");

            return view;
        }

        public void Release<TView>(TView view) where TView : MonoBehaviour, IView
        {
            Object.Destroy(view.gameObject);
        }
    }
}