using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pickup))]
public class PickupCalendarPiece : MonoBehaviour
{
    public int pieceNumber = 1;

    void OnPickedUp()
    {
        FindObjectOfType<CalendarPieceHUDController>().PieceCollected(pieceNumber);
    }
}
