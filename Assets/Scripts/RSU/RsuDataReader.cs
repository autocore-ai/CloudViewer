using Assets.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WebRequestServer))]
[RequireComponent(typeof(RsuPluginsStatus), typeof(RsuTrafficlightStatus), typeof(RsuVehicleStatus))]
[RequireComponent(typeof(PodState), typeof(AutotestResult))]
public class RsuDataReader : MonoBehaviour
{
    WebRequestServer webRequestServer;
    RsuPluginsStatus rsuPluginsStatus;
    RsuTrafficlightStatus rsuTrafficlightStatus;
    RsuVehicleStatus rsuVehicleStatus;
    PodState podState;
    AutotestResult autotestResult;
    const string keyPluginsStatus = "plugins/status";
    const string keyVehicleStatus = "vehicle/status";
    const string keyTrafficlightStatus = "traffic_light/status";
    const string keyPodState = "pod_state";
    const string keyAutotestResult = "autotest/result";
    void Start()
    {
        webRequestServer = GetComponent<WebRequestServer>();
        rsuPluginsStatus = GetComponent<RsuPluginsStatus>();
        rsuTrafficlightStatus = GetComponent<RsuTrafficlightStatus>();
        rsuVehicleStatus = GetComponent<RsuVehicleStatus>();
        podState = GetComponent<PodState>();
        autotestResult = GetComponent<AutotestResult>();
        webRequestServer.OnGetRequest += OnGetRequest;
    }

    private void OnGetRequest(string json)
    {
        var list = JsonConvert.DeserializeObject<List<RsuData>>(json);
        foreach (var item in list)
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
            else if (item.key.Contains(keyPodState))
            {
                podState.OnDate(item.value.ToString());
            }
            else if (item.key.Contains(keyAutotestResult))
            {
                autotestResult.OnDate(item.value.ToString());
            }
            else
            {
                Debug.LogWarningFormat("Unknown key {0} from web api", item.value);
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

