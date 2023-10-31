using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class ProductShowcase : MonoBehaviour
{
    public Answers answers;
    public float radious;
    public float timeToRotate;
    public float rotationX;
    private float angleFraction;

    void Start()
    {
        angleFraction = (360f / transform.childCount);
        SetSelectedProducts();
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

    public void RotateProducts()
    {
        StartCoroutine(RotateProductsCoroutine());
    }

    private IEnumerator RotateProductsCoroutine()
    {
        float nextAngle = (transform.rotation.eulerAngles.y + angleFraction) % 360f;
        float speed = angleFraction / timeToRotate * Time.deltaTime;
        float rotationY = transform.localRotation.eulerAngles.y;
        do
        {
            rotationY = (rotationY + speed) % 360f;
            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
            yield return new WaitForEndOfFrame();
        } while (MathF.Abs(rotationY - nextAngle) > speed);
        transform.localRotation = Quaternion.Euler(0, nextAngle, 0);
    }
}
