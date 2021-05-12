#region License
using System.Collections.Generic;
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
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class CVManager : MonoBehaviour
    {
        public static CVManager Instance;
        private GameObject selectedGo;
        public GameObject SelectedGO
        {
            get { return selectedGo; }
            set
            {
                if (value == selectedGo) return;
                selectedGo = value;
                var traffic = selectedGo.GetComponentInParent<TrafficLight>();
                if (traffic != null)
                {
                    OnChangeTL.Invoke(traffic);
                }
                var vehicle = selectedGo.GetComponentInParent<Vehicle>();
                if (vehicle != null)
                {
                    OnChangeVehicle.Invoke(vehicle);
                }
            }
        }
        public Action<TrafficLight> OnChangeTL;
        public Action<Vehicle> OnChangeVehicle;
        public WebRequestServer webRequesetServer;
        GameObject goWebRequesetServer;
        public AddressNode addressNode;
        AddressNode goAddressNode;

        public Dictionary<string, TrafficLight> trafficLights = new Dictionary<string, TrafficLight>();
        public List<Vehicle> vehicles = new List<Vehicle>();
        private void Awake()
        {
            Instance = this;
            if (webRequesetServer == null)
            {
                webRequesetServer = Instantiate(goWebRequesetServer).GetComponent<WebRequestServer>();
            }
            if (addressNode == null)
            {
                addressNode = Instantiate(goAddressNode).GetComponent<AddressNode>();
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            addressNode.OnAddressConfigChange += SetAddress;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawLine(ray.origin, hit.point);
                    Debug.Log(hit.collider.name);
                    SelectedGO = hit.collider.gameObject;
                }
            }

        }

        private void SetAddress(AddressConfig config)
        {
            webRequesetServer.postAddress = config.postTrafficAddress;
            webRequesetServer.getAddress = config.getTrafficAddress;
        }
    }
}
