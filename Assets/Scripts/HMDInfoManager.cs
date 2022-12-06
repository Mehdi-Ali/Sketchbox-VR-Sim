using UnityEngine;
using UnityEngine.XR;

public class HMDInfoManager : MonoBehaviour
{
    private void Start()
    {
        var isDeviceActive = XRSettings.isDeviceActive;
        var loadedDeviceName = XRSettings.loadedDeviceName;

        if (!isDeviceActive)
        {
            Debug.Log("No VR Setup detected");
            return;
        }

        else if (loadedDeviceName == "Mock HMD" || loadedDeviceName == "MockHMD Display")
        {
            Debug.Log("No VR Setup detected, Mocking XR is active");
        }

        else 
        {
            Debug.Log(loadedDeviceName + "Connected");
        }
    }
}
