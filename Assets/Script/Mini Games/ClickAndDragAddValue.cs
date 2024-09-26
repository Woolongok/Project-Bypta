using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDragAddValue : MonoBehaviour
{
    public GameObject objectToMove;
    private Vector2 velocity = Vector2.zero;
    public float movePerSwipe = -100;
    Vector2 nextTarget = Vector2.zero;
    bool onceAdd = true;

    float lastYVal;
    public float TotalDragging = 0;
    void Start()
    {
        lastYVal = Input.mousePosition.y;
    }
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.y < lastYVal)
            {
                if(onceAdd)
                {
                    nextTarget = objectToMove.transform.position + objectToMove.transform.position + new Vector3(0, movePerSwipe, 0);
                    onceAdd = false;
                }
                Debug.Log(TotalDragging);
                lastYVal = Input.mousePosition.y;
                TotalDragging++;
            }
        }
        else
        {
            nextTarget = objectToMove.transform.position;
            lastYVal = 1080;
            onceAdd = true;
        }

        if (TotalDragging >= 20)
        {
            objectToMove.transform.position = Vector2.SmoothDamp(objectToMove.transform.position,nextTarget, ref velocity, 1f);
            StartCoroutine(timeToZero());
        }
    }

    IEnumerator timeToZero()
    {
        yield return new WaitForSeconds(1);
        TotalDragging = 0;
    }
}
