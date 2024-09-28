using UnityEngine;
using System.Collections.Generic;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private int matchId;
    bool canUnoccupiedFirstIn = true;
    public bool firstIn = true;
    Vector3 offset;
    private SpriteRenderer image;
    public string itemTag = "Item";
    public string destinationTag = "DropArea";
    public bool setPos = false;
    public bool goBack = false;
    public GameObject earlyPlace = null;
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

    private void Start()
    {
        setPos = true;
        if (earlyPlace != null && closestDropArea == null && earlyPlace.TryGetComponent(out objectMatchForm))
        {
            objectMatchForm.SetOccupied(true);
        }
    }

    void OnMouseDown()
    {
        Color currentColor = image.color;
        currentColor.a = 0.67f;
        image.color = currentColor;

        offset = transform.position - MouseWorldPosition();
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
        setPos = false;
        goBack = false;


        if (closestDropArea != null && closestDropArea.TryGetComponent(out objectMatchForm) && dropAreas.Count == 1)
        {
            objectMatchForm.SetOccupied(false);
        } 
        else if (earlyPlace != null && earlyPlace.TryGetComponent(out objectMatchForm) && canUnoccupiedFirstIn)
        {
            objectMatchForm.SetOccupied(false);
            canUnoccupiedFirstIn = false;
        }
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

                    gameObject.GetComponent<DragAndDrop>().enabled = false;
                }
                else
                {
                    setPos = true;
                    objectMatchForm.SetOccupied(true);
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
        if (setPos && closestDropArea != null && !goBack)
        {
            transform.position = Vector2.SmoothDamp(transform.position, closestDropArea.transform.position + new Vector3(0, 0, -3), ref velocity, 0.1f);
        } 
        else if (earlyPlace != null && firstIn)
        {
            transform.position = Vector2.SmoothDamp(transform.position,earlyPlace.transform.position + new Vector3(0, 0, -3), ref velocity, 0.1f);
            if ((Vector2)transform.position == (Vector2)earlyPlace.transform.position)
            {
                firstIn = false;
            }
        }
        else if (goBack)
        {
            Vector2 smoothPosition = Vector2.SmoothDamp(transform.position, (Vector3)startPos, ref velocity, 0.1f);
            transform.position = new Vector3(smoothPosition.x, smoothPosition.y, -3);
        }

        if (Vector3.Distance(transform.position, startPos) < 0.01f)
        {
            setPos = false;
            goBack = false;
        }
    }
}