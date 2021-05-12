using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class RsuVehicleStatus : MonoBehaviour
{
    public Transform vehicle;
    internal void OnDate(string data)
    {
        var pose = JsonConvert.DeserializeObject<List<VehiclePose>>(data.ToString());
        foreach (var item in pose)
        {
            vehicle.position = item.position.Ros2Unity();
            vehicle.rotation = item.orientation.Ros2Unity();
        }
    }
}

static class Externs
{
    public static Vector3 Ros2Unity(this Vector3 vector3)
    {
        return new Vector3(vector3.x, vector3.z, vector3.y);
    }
    public static Quaternion Ros2Unity(this Quaternion quaternion)
    {
        return new Quaternion(quaternion.y, -quaternion.z, -quaternion.x, quaternion.w);
    }
}

[Serializable]
public class VehiclePose
{
    [SerializeField]
    public Quaternion orientation;
    [SerializeField]
    public Vector3 position;
}
