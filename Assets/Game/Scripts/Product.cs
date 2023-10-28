using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Image image;
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Light light;

    public void SetSelected(bool selected)
    {
        if (!selected) return;
        meshRenderer.material = selectedMaterial;
        image.sprite = selectedSprite;
        light.enabled = true;
    }
}
