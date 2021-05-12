﻿#region License
/*
 * Copyright 2021 AutoCore
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * Authors: AutoCore Members
 *
 */
#endregion


using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public enum LightMode
    {
        None = 0,
        Red = 1,
        Green = 2,
        Yellow = 3
    }
    public class TrafficLight : MonoBehaviour
    {
        private TrafficLightData lightData;
        public TrafficLightData LightData
        {
            get
            {
                if (lightData == null)
                {
                    lightData = new TrafficLightData(gameObject.name, 1, 30);
                }
                return lightData;
            }
            set
            {
                lightData = value;
                SetLight((LightMode)lightData.color);
            }
        }
        [SerializeField]
        private GameObject RedLight;
        [SerializeField]
        private GameObject YellowLight;
        [SerializeField]
        private GameObject GreenLight;

        //Transform StopLine { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        private GameObject[] Lights;
        private void Awake()
        {
            if (RedLight == null) RedLight = transform.GetChild(0).gameObject;
            if (GreenLight == null) GreenLight = transform.GetChild(1).gameObject;
            if (YellowLight == null) YellowLight = transform.GetChild(2).gameObject;
            Lights = new GameObject[3] { GreenLight, YellowLight, RedLight };
        }
        void Start()
        {
            if (!CVManager.Instance.trafficLights.ContainsKey(name))
            {
                CVManager.Instance.trafficLights.Add(name, this);
            }
        }
        public void SetLight(LightMode mode)
        {
            foreach (GameObject light in Lights)
            {
                light.SetActive(false);
            }
            if (mode != LightMode.None) Lights[(int)mode - 1].SetActive(true);
        }
    }

}