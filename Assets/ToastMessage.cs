using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AirbridgeUnityExample
{
    class Toast
    {
        public static int LENGTH_SHORT = 1;
        public static int LENGTH_LONG = 6;
    }

    public class ToastMessage : MonoBehaviour
    {
        private static GameObject _currentGameObject = null;

        public static void Show(string message, int seconds = 3)
        {
            Clear();
            GameObject prefab = Resources.Load("Prefabs/Toast Message") as GameObject;
            _currentGameObject = Instantiate(prefab);
            Text text = _currentGameObject.GetComponentInChildren<Text>();
            text.text = message;
            Destroy(_currentGameObject, seconds);
        }

        public static void Clear()
        {
            if (_currentGameObject != null)
            {
                Destroy(_currentGameObject);
                _currentGameObject = null;
            }
        }
    }
}