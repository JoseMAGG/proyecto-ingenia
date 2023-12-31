using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeMovement : MonoBehaviour
{
    private Gyroscope gyro;
    public readonly bool gyroEnabled;
    private Quaternion rotation;

    private void Awake()
    {
        EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            rotation = new Quaternion(0, 0, 1, 0);
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rotation;
        }
    }
}
