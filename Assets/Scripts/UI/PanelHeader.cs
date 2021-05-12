using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PanelHeader: PanelBase<PanelHeader>, ISimuPanel
    {
        public Text text_FPS;
        public Text text_version;
        public Button button_address;
        public Dropdown dropdown_Camera;


        public float updateInterval = 0.5f;
        private float lastInterval;
        private int frames = 0;
        private float fps;
        public Action OnSetCameraFree;
        public Action OnSetCameraOverLook;
        public void Start()
        {
            text_version.text ="v."+Application.version;
        }
        void Update()
        {
            ++frames;
            float timeNow = Time.realtimeSinceStartup;
            if (timeNow >= lastInterval + updateInterval)
            {
                fps = frames / (timeNow - lastInterval);
                frames = 0;
                lastInterval = timeNow;
            }
            text_FPS.text = fps.ToString("0.0");
        }

        public void SetCameraDropdown()
        {
            dropdown_Camera.onValueChanged.AddListener((int value)=>
            {
                if (value == 0)
                {
                    OnSetCameraFree.Invoke();
                }
                else
                {
                    OnSetCameraOverLook.Invoke();
                }
            });
        }

    }
}
