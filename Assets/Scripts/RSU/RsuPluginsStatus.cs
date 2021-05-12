using System;
using Newtonsoft.Json;
using UnityEngine;

public class RsuPluginsStatus : MonoBehaviour
{
    internal void OnDate(string data)
    {
        var statuses = JsonConvert.DeserializeObject<PluginsStatus>(data.ToString());
    }
}

[Serializable]
public class PluginsStatus
{
    [SerializeField]
    public PluginStatus traffic_light;
    [SerializeField]
    public PluginStatus vehicle_status;
}
[Serializable]
public class PluginStatus
{
    [SerializeField]
    public bool active;
    [SerializeField]
    public string path;
}