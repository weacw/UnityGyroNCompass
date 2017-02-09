using UnityEngine;
using System.Collections;

public class GyroController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private GameObject cameraContainer;
    private Quaternion rot;
    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            cameraContainer.transform.rotation = Quaternion.Euler(90.0f,0,0);
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude*rot;
        }
    }
}
