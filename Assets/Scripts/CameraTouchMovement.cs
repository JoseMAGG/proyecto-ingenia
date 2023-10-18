using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchMovement : MonoBehaviour
{
    [Range(1, 5)]
    public float angularSpeed;
    public bool invertHorizontal;
    public bool invertVertical;

    private Vector2 invertSigns;

    void Start()
    {
        invertSigns.x = invertHorizontal ? -1 : 1;
        invertSigns.y = invertVertical ? -1 : 1;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveWithMouse();
        }

        if (Input.touchCount == 1)
        {
            MoveWithTouch();
        }
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z = 0;
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void MoveWithTouch()
    {
        Touch touchZero = Input.GetTouch(0);
        if (touchZero.phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition * angularSpeed * Time.deltaTime;
            transform.Rotate(touchDeltaPosition.y * invertSigns.x, touchDeltaPosition.x * invertSigns.y, 0);
        }
    }

    private void MoveWithMouse()
    {
        float pointerY = Input.GetAxis("Mouse X");
        float pointerX = Input.GetAxis("Mouse Y");
        Vector2 rotateVector = new Vector2(pointerX * invertSigns.x, pointerY * invertSigns.y) * angularSpeed * 10 * Time.deltaTime;
        transform.Rotate(rotateVector);
    }
}
