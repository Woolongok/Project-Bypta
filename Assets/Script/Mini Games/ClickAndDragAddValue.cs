using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickAndDragAddValue : MonoBehaviour
{
    public GameObject button;
    public GameObject[] objectToMove; // Array of objects to move
    private Vector2 velocity = Vector2.zero; // SmoothDamp velocity
    public float movePerSwipe = -100; // Amount to move per swipe
    private Vector2[] nextTargets; // Targets for each object in the array
    private bool onceAdd = true;

    private float lastYVal; // Last Y position of the mouse
    public float addDragging = 0; // Counter for the total amount of dragging
    public float totalDragging = 0;


    void Start()
    {
        lastYVal = Input.mousePosition.y;
        nextTargets = new Vector2[objectToMove.Length]; // Initialize nextTargets array
        // Set initial target positions to current positions of objects
        for (int i = 0; i < objectToMove.Length; i++)
        {
            nextTargets[i] = objectToMove[i].transform.position;
        }
    }

    void Update()
    {
        if(totalDragging >= 600)
        {
            button.SetActive(true);
        }

        // Check if the left mouse button is being held down
        if (Input.GetMouseButton(0))
        {
            // If the mouse moved downwards since the last frame
            if (Input.mousePosition.y < lastYVal)
            {
                if (onceAdd)
                {
                    // Update the target position for each object in the array
                    for (int i = 0; i < objectToMove.Length; i++)
                    {
                        nextTargets[i] = objectToMove[i].transform.position + new Vector3(0, movePerSwipe, 0);
                    }
                    onceAdd = false;
                }

                Debug.Log(addDragging);
                lastYVal = Input.mousePosition.y;
                addDragging++;
                totalDragging++;
            }
        }
        else
        {
            // Reset target positions to the current positions of objects when mouse is not clicked
            for (int i = 0; i < objectToMove.Length; i++)
            {
                nextTargets[i] = objectToMove[i].transform.position;
            }
            lastYVal = 1080; // Arbitrary value to reset lastYVal
            onceAdd = true; // Allow new targets to be set on the next swipe
        }

        // If dragging exceeds a threshold value
        if (addDragging >= 20)
        {
            // Smoothly move each object towards its respective target position
            for (int i = 0; i < objectToMove.Length; i++)
            {
                objectToMove[i].transform.position = Vector2.SmoothDamp(
                    objectToMove[i].transform.position,
                    nextTargets[i],
                    ref velocity,
                    1f
                );
            }
            StartCoroutine(timeToZero());
        }
    }

    // Coroutine to reset the dragging counter after 1 second
    IEnumerator timeToZero()
    {
        yield return new WaitForSeconds(1);
        addDragging = 0;
    }
}

