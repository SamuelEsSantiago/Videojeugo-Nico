using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimap{
    public class MinimapWindow : MonoBehaviour
    {
        private static MinimapWindow instance;
        private void Awake()
        {
            instance = this;
        }

        // Update is called once per frame
        public static void Show()
        {
            instance.gameObject.SetActive(true);
        }
        public static void Hide()
        {            
            instance.gameObject.SetActive(false);
        }
    }
}
