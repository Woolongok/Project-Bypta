using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class blur : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float theRightValue = 0.5f;
    // Define focus distance settings for Depth of Field
    public float minFocusDistance = 0.1f;  // Minimum distance (blurry)
    public float maxFocusDistance = 10f;   // Maximum distance (blurry)
    public float sharpFocusDistance = 2f;  // Distance for the sharpest focus
    public bool checkCameraBlur = false;
    private DepthOfField depthOfField; // Reference to the DepthOfField effect

    private void Start()
    {
        GameObject globalVolume = GameObject.Find("Global Volume");

        if (globalVolume != null)
        {
            Volume volume = globalVolume.GetComponent<Volume>();

            if (volume != null && volume.profile != null)
            {
                if (volume.profile.TryGet(out depthOfField))
                {
                    Debug.Log("Depth of Field found in the volume profile.");
                }
                else
                {
                    Debug.LogError("Depth of Field is not added to the Volume profile.");
                }
            }
            else
            {
                Debug.LogError("Volume component or profile is missing from Global Volume.");
            }
        }
        else
        {
            Debug.LogError("Global Volume object not found in the scene.");
        }
    }

    public void OnSliderValueChangeBlur(float value)
    {
        if (depthOfField != null)
        {
            depthOfField.active = true;

            float newFocusDistance;

            if (value >= theRightValue + 0.01)
            {
                newFocusDistance = 900 * (value - theRightValue) * 2;
            }
            else if (value <= theRightValue - 0.01)
            {
                newFocusDistance = 300 * (1 - value);
            }
            else
            {
                newFocusDistance = 10;
                checkCameraBlur = true;
            }
            Debug.Log(newFocusDistance);
            /*if (value <= 0.5f)
            {
                newFocusDistance = Mathf.Lerp(maxFocusDistance, sharpFocusDistance, value * 2);
            }
            else
            {
                newFocusDistance = Mathf.Lerp(sharpFocusDistance, minFocusDistance, (value - 0.5f) * 2);
            }*/

            depthOfField.focalLength.overrideState = true;
            depthOfField.focalLength.value = newFocusDistance;
        }

    }
}
