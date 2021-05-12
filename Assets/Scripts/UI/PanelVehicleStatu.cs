using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PanelVehicleStatu : PanelBase<PanelVehicleStatu>, ISimuPanel
    {
        public Image image_steer;
        public Slider slider_speed;
        public Slider slider_throttle;
        public Slider slider_brake;
        public Text text_speed;
        public Text text_throttle;
        public Text text_brake;

        public Text text_name;
        public void InitVehicle(string name)
        {
            text_name.text = name;
            SetPanelActive(true);
        }

        public void UpdateVehicleStatu(float steer,float speed,float throttle,float brake)
        {
            image_steer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 540 * steer));
            slider_speed.value = speed;
            slider_throttle.value = throttle;
            slider_brake.value = brake;
            text_speed.text = speed.ToString();
            text_throttle.text = throttle.ToString();
            text_brake.text = brake.ToString();
        }
        void ChangeTL()
        {
            SetPanelActive(true);
        }
    }
}
