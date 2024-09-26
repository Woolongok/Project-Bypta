using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ObjectMatchForm : MonoBehaviour
{
    [SerializeField] private int match_id;
    public bool isOccupied = false;

    public int Get_ID()
    {
        return match_id;
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public void SetOccupied(bool status)
    {
        isOccupied = status;
    }
}
