using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pedestal : MonoBehaviour
{
    private Camera camera;
    public TextMeshProUGUI quesiton;
    public Image icon;
    public Animator animator;

    private void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(camera.transform.forward);
    }

    public void ResetAnimation()
    {
        animator.SetTrigger("Reset");
    }

    public void ShowInfo()
    {
        animator.SetTrigger("Show");
    }
}
