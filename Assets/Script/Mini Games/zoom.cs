using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Zoom : MonoBehaviour
{
    public float minScale = 0.5f;
    public float maxScale = 3f;
    private Vector3 initialScale;
    public GameObject zoomObject;
    [Range(0.0f, 1.0f)] 
    public float theRightValue = 0.5f;
    public bool checkCameraZoom = false;


    private DepthOfField depthOfField; // Reference to the DepthOfField effect

    void Start()
    {
        initialScale = zoomObject.transform.localScale;

        
    }

    public void OnSliderValueChangeZoom(float value)
    {
        float newScaleValue = Mathf.Lerp(minScale, maxScale, value);
        zoomObject.transform.localScale = initialScale * newScaleValue;

        if (value >= theRightValue + 0.01)
        {
        }
        else if (value <= theRightValue - 0.01)
        {
        }
        else
        {
            checkCameraZoom = true;
            Debug.Log("Yayyy");
        }
    }

    
}
