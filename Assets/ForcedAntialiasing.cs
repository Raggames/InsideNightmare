using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ForcedAntialiasing : MonoBehaviour
{
    public Camera cam;
    public Volume vol;
    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.GetUniversalAdditionalCameraData().antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
        cam.GetUniversalAdditionalCameraData().antialiasingQuality = AntialiasingQuality.High;

        vol = FindObjectOfType<Volume>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            cam.GetUniversalAdditionalCameraData().antialiasing = AntialiasingMode.None;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            cam.GetUniversalAdditionalCameraData().antialiasing = AntialiasingMode.FastApproximateAntialiasing;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            cam.GetUniversalAdditionalCameraData().antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
            return;
        }

        if (!Input.GetKeyDown(KeyCode.Keypad4)) return;
        vol.weight = vol.weight <= 0.01 ? 0 : 1;

    }
}
