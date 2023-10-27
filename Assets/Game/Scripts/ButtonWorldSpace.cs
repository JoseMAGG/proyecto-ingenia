using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWorldSpace : MonoBehaviour
{
    [SerializeField] private bool isYes;
    private void OnMouseUp()
    {
        PedestalsMovement.GetInstance().CatchAnswer(isYes);
    }
}
