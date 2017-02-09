using UnityEngine;
using System.Collections;

public class CompassController : MonoBehaviour
{
    private bool compassEnabled;
    void Start()
    {
        compassEnabled = EnableCompass();
        //StartCoroutine(UpdateRot());
        transform.rotation = Quaternion.AngleAxis(220,Vector3.up);
    }

    private bool EnableCompass()
    {
        Input.compass.enabled = true;
        Input.location.Start();
        return true;
    }

    private IEnumerator UpdateRot()
    {
        yield return new WaitForSeconds(1.0f);
        if (compassEnabled)
        {
            float magnetic = Input.compass.magneticHeading;

            transform.rotation = Quaternion.AngleAxis(magnetic, Vector3.up);
        }
    }
    private void OnGUI()
    {
        if (compassEnabled)
        {
            GUILayout.Label(transform.rotation.ToString());
            GUILayout.Label(Input.compass.magneticHeading.ToString());
        }
    }
}
