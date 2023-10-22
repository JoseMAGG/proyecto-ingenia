using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchMovement : MonoBehaviour
{
    public Camera camera;
    [Range(1, 3)]
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
        if (Input.touchCount == 1)
        {
            MoveWithTouch();
        }
    }


    private void MoveWithTouch()
    {
        Touch touchZero = Input.GetTouch(0);
        if (touchZero.phase == TouchPhase.Moved)
        {
            RotateCamera(touchZero);
        }
    }

    private void RotateCamera(Touch touchZero)
    {
        Vector3 newRotation = pastRotation;

        newRotation.y = Mathf.Clamp(newRotation.y + touchZero.deltaPosition.x
            * angularSpeed * Time.deltaTime * invertSigns.y,
        clampVision.y * -1f, clampVision.y);

        newRotation.x = Mathf.Clamp(newRotation.x + touchZero.deltaPosition.y
            * angularSpeed * Time.deltaTime * invertSigns.x,
        clampVision.x * -1f, clampVision.x);

        camera.transform.localRotation = Quaternion.Euler(newRotation);
        pastRotation = newRotation;
    }
}
