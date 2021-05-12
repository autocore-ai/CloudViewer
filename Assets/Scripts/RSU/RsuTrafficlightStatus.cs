using System.Collections.Generic;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;

public class RsuTrafficlightStatus : MonoBehaviour
{
    internal void OnDate(string data)
    {
        // Debug.Log(data);
        var list = JsonConvert.DeserializeObject<List<TrafficLightData>>(data);
        foreach (var item in list)
        {
            if (CVManager.Instance.trafficLights.ContainsKey(item.id))
            {
                CVManager.Instance.trafficLights[item.id].LightData = item;
            }
        }

    }
}
