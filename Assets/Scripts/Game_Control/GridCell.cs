using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private GameObject occupant;
    public bool IsEmpty => occupant == null;   // a destroyed ally frees the cell
    //Checks if a cell has an ally on it before placing a new unit
    public bool TryPlaceExisting(GameObject unit)
    {
        if (!IsEmpty) return false;

        occupant = unit;
        unit.transform.position = transform.position;
        unit.transform.SetParent(transform);
        return true;
    }
}
