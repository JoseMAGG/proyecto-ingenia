using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMoveMode : MonoBehaviour
{
    public CameraTouchMovement TouchMode;
    public GyroscopeMovement GyroMode;

    void Start()
    {
        bool gyroEnabled = GyroMode.gyroEnabled;
        GyroMode.enabled = gyroEnabled;
        TouchMode.enabled = !gyroEnabled;
    }

    public void SwitchMode()
    {
        bool gyroEnabled = GyroMode.enabled;
        GyroMode.enabled = !gyroEnabled;
        TouchMode.enabled = gyroEnabled;
    }
}
