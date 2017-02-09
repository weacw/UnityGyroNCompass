using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhotoCamera : MonoBehaviour
{
    private bool camAvaulable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvaulable = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
        }
        if (backCam == null)
        {
            Debug.Log("Unable to find back camera");
            return;
        }
        backCam.Play();
        background.texture = backCam;
        camAvaulable = true;
    }

    public void Update()
    {
        if (!camAvaulable) return;
        float ratio = (float) backCam.width/(float) backCam.height;
        fit.aspectRatio = ratio;
        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
