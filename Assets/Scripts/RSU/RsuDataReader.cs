using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;

[RequireComponent(typeof(WebRequestServer))]
[RequireComponent(typeof(RsuPluginsStatus), typeof(RsuTrafficlightStatus), typeof(RsuVehicleStatus))]
public class RsuDataReader : MonoBehaviour
{
    WebRequestServer webRequestServer;
    RsuPluginsStatus rsuPluginsStatus;
    RsuTrafficlightStatus rsuTrafficlightStatus;
    RsuVehicleStatus rsuVehicleStatus;
    const string keyPluginsStatus = "plugins/status";
    const string keyVehicleStatus = "vehicle/status";
    const string keyTrafficlightStatus = "traffic_light/status";
    void Start()
    {
        webRequestServer = GetComponent<WebRequestServer>();
        rsuPluginsStatus = GetComponent<RsuPluginsStatus>();
        rsuTrafficlightStatus = GetComponent<RsuTrafficlightStatus>();
        rsuVehicleStatus = GetComponent<RsuVehicleStatus>();
        webRequestServer.OnGetRequest += OnGetRequest;
    }

    private void OnGetRequest(string json)
    {
        var data = JsonConvert.DeserializeObject<List<RsuData>>(json);
        foreach (var item in data)
        {
            if (item.key.Contains(keyVehicleStatus))
            {
                rsuVehicleStatus.OnDate(item.value.ToString());
            }
            else if (item.key.Contains(keyTrafficlightStatus))
            {
                rsuTrafficlightStatus.OnDate(item.value.ToString());
            }
            else if (item.key.Contains(keyPluginsStatus))
            {
                rsuPluginsStatus.OnDate(item.value.ToString());
            }
            else
            {
                Debug.LogWarningFormat("Unknown key {0} from web api", json);
            }
        }
    }
}

[Serializable]
public class RsuData
{
    [SerializeField]
    public string key;
    [SerializeField]
    public System.Object value;
}

