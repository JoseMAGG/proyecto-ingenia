using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class ProductShowcase : MonoBehaviour
{
    public Answers answers;
    public float radious;
    public float timeToRotate;
    private float angleFraction;
    private bool isTouching;
    private bool canMove;
    private bool isMoving;

    void Start()
    {
        //Usar para posicionar los elementos en forma de círculo
        //PlaceChildrenArround();
        angleFraction = (360f / transform.childCount);
        SetSelectedProducts();
    }

    void Update()
    {
        if (Input.touchCount > 0 && !isTouching)
        {
            isTouching = true;
        }
        else if (Input.touchCount == 0 && isTouching)
        {
            isTouching = false;
            canMove = true;
        }
        if (canMove && !isMoving)
        {
            StartCoroutine(RotateProducts());
        }
    }

    private void SetSelectedProducts()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i)
                .GetComponent<Product>()
                .SetSelected(answers.AnswersList[i]);
        }
    }

    private IEnumerator RotateProducts()
    {
        isTouching = true;
        isMoving = true;
        float nextAngle = (transform.rotation.eulerAngles.y + angleFraction) % 360f;
        float speed = angleFraction / timeToRotate * Time.deltaTime;
        float rotationY = transform.localRotation.eulerAngles.y;
        do
        {
            rotationY = (rotationY + speed) % 360f;
            transform.localRotation = Quaternion.Euler(0, rotationY, 0);
            yield return new WaitForEndOfFrame();
        } while (MathF.Abs(rotationY - nextAngle) > speed);
        transform.localRotation = Quaternion.Euler(0, nextAngle, 0);
        isMoving = false;
        canMove = false;
    }

    private void PlaceChildrenArround()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            float angulo = i * angleFraction;
            Vector3 posicion = transform.position - Quaternion.Euler(0, angulo, 0) * Vector3.forward * radious;
            transform.GetChild(i).position = posicion;
        }
    }
}
