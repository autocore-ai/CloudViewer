using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Assets.Scripts;

namespace Assets.Scripts.UI
{
    public class PanelTrafficLight : PanelBase<PanelTrafficLight>, ISimuPanel
    {
        public Text text_name;
        public Button button_set;
        public Toggle toggle_R;
        public Toggle toggle_Y;
        public Toggle toggle_g;
        public Text text_setscends;
        public InputField inputField_Scends;
        public TrafficLight trafficLight;

        public Image image_color;
        public Text text_second;

        private void Start()
        {
            button_set.onClick.AddListener(PostTraffic);
            CVManager.Instance.OnChangeTL += ChangeTL;
        }
        private void PostTraffic()
        {
            if (trafficLight == null)
            {
                PanelNotice.Instance.SetNotice("Traffic is not set", 2);
                return;
            }
            TrafficLightData data = trafficLight.LightData;
            data.remain = int.Parse(inputField_Scends.text);
            if (toggle_R.isOn) data.color = 1;
            else if (toggle_g.isOn) data.color = 2;
            else if (toggle_Y.isOn) data.color = 3;
            StartCoroutine(CVManager.Instance.webRequesetServer.PostWebRequest_Form(data));
        }
        void ChangeTL(TrafficLight trafficLight)
        {
            SetPanelActive(true);
            this.trafficLight = trafficLight;
        }
        void Update()
        {
            if (trafficLight == null) return;
            if (trafficLight.LightData == null) return;
            switch (trafficLight.LightData.color)
            {
                case 1:
                    image_color.color = Color.red;
                    break;
                case 2:
                    image_color.color = Color.green;
                    break;
                case 3:
                    image_color.color = Color.yellow;
                    break;
                default:
                    break;
            }
            text_second.text = trafficLight.LightData.remain.ToString();


        }

    }
}
