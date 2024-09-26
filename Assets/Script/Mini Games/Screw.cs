using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour
{
    HingeJoint2D hingeJoint2D;

    void Awake()
    {
        hingeJoint2D = GetComponent<HingeJoint2D>();
    }

    private void OnMouseDown()
    {
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
