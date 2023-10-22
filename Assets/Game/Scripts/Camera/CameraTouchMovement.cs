using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchMovement : MonoBehaviour
{
    [Range(1, 5)]
    public float angularSpeed;
    public bool invertHorizontal;
    public bool invertVertical;
    public Vector2 clampVision;
    private Vector2 invertSigns;
    private Vector3 pastRotation;
    void Start()
    {
        pastRotation = Vector3.zero;
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
            //ClampVision();
        }
    }

    private void ClampVision()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z = 0;
        Vector3 deltaRotation = pastRotation - rotation;
        rotation.y = Clamp(rotation.y, clampVision.y, deltaRotation.y > 0);
        rotation.x = Clamp(rotation.x, clampVision.x, deltaRotation.x > 0);
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void MoveWithTouch()
    {
        Touch touchZero = Input.GetTouch(0);
        if (touchZero.phase == TouchPhase.Moved)
        {
            Vector3 newRotation = pastRotation;
            
            newRotation.y = Mathf.Clamp(newRotation.y + touchZero.deltaPosition.x
                * angularSpeed * Time.deltaTime * invertSigns.y,
            clampVision.y * -1f, clampVision.y);

            newRotation.x = Mathf.Clamp(newRotation.x + touchZero.deltaPosition.y
                * angularSpeed * Time.deltaTime * invertSigns.x,
            clampVision.x * -1f, clampVision.x);

            transform.eulerAngles = newRotation;
            pastRotation = newRotation;
        }
    }

    private void MoveWithMouse()
    {
        float pointerY = Input.GetAxis("Mouse X");
        float pointerX = Input.GetAxis("Mouse Y");
        Vector2 rotateVector = new Vector2(pointerX * invertSigns.x, pointerY * invertSigns.y) * angularSpeed * 10 * Time.deltaTime;
        transform.Rotate(rotateVector);
    }

    private float Clamp(float value, float clamp, bool isIncreasing)
    {
        print("Increasing: " + isIncreasing);
        if (value > 360 - clamp)
            return value;
        else if (value < 360 - clamp && !isIncreasing)
            return 360 - clamp;
        else if (value > clamp && isIncreasing)
            return clamp;
        else return value;
    }
}
