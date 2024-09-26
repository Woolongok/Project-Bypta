using UnityEngine;
using System.Collections.Generic;

public class DragAndDropLimited : MonoBehaviour
{
    [SerializeField] private int matchId;

    Vector3 offset;
    private SpriteRenderer image;
    public string itemTag = "Item";
    public string destinationTag = "DropArea";
    public bool setPos = false;
    public bool goBack = false;
    public GameObject connectedCable;
    private Vector2 velocity = Vector2.zero;
    private Vector2 startPos;
    Collider2D closestDropArea;

    List<Collider2D> dropAreas = new List<Collider2D>();

    ObjectMatchForm objectMatchForm;

    void Awake()
    {
        image = GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    void OnMouseDown()
    {
        Color currentColor = image.color;
        currentColor.a = 0.67f;
        image.color = currentColor;

        offset = transform.position - MouseWorldPosition();

        if (closestDropArea != null && closestDropArea.TryGetComponent(out objectMatchForm) && dropAreas.Count == 1)
        {
            objectMatchForm.SetOccupied(false);
        }
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
        setPos = false;
        goBack = false;
    }

    void OnMouseUp()
    {
        Color currentColor = image.color;
        currentColor.a = 1f;
        image.color = currentColor;

        if (dropAreas.Count > 0)
        {
            closestDropArea = GetClosestDropArea();
            if (closestDropArea != null && closestDropArea.TryGetComponent(out objectMatchForm) && !objectMatchForm.IsOccupied())
            {
                if (matchId == objectMatchForm.Get_ID())
                {
                    setPos = true;
                    objectMatchForm.SetOccupied(true);
                    Debug.Log("Correct!");
                }
                else
                {
                    goBack = true;
                    objectMatchForm.SetOccupied(false);
                    Debug.Log("Incorrect");
                }
            }
            else
            {
                goBack = true;
            }
        }
        else
        {
            Debug.Log("No drop area found! Dropping item outside.");
        }
    }

    private Collider2D GetClosestDropArea()
    {
        float closestDistance = Mathf.Infinity;
        Collider2D closestArea = null;

        foreach (var area in dropAreas)
        {
            float distance = Vector2.Distance(transform.position, area.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestArea = area;
            }
        }

        return closestArea;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(destinationTag))
        {
            dropAreas.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(destinationTag))
        {
            dropAreas.Remove(collision);
        }
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void Update()
    {
        if (closestDropArea is not null && transform.position == closestDropArea.transform.position)
        {
            connectedCable.SetActive(true);
            gameObject.SetActive(false);
        }
        if (setPos && closestDropArea != null && !goBack)
        {
            transform.position = Vector2.SmoothDamp(transform.position, closestDropArea.transform.position + new Vector3(0, 0, -0.01f), ref velocity, 0.1f);
        }
        else if (goBack)
        {
            transform.position = Vector2.SmoothDamp(transform.position, (Vector3)startPos + new Vector3(0, 0, -0.01f), ref velocity, 0.1f);
        }

        if (Vector3.Distance(transform.position, startPos) < 0.01f)
        {
            setPos = false;
            goBack = false;
        }
    }
}