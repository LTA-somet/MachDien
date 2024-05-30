using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class HighlightController : MonoBehaviour
{
   
    [SerializeField]
    private Material originalMaterial;
    [SerializeField]
    private Material highlightMaterial;

    private void Start()
    {
        // Lưu vật liệu ban đầu của vật thể
        originalMaterial = GetComponent<MeshRenderer>().material;
        highlightMaterial = Instantiate(originalMaterial);
    }

    private void OnMouseEnter()
    {
        // Thay đổi vật liệu thành vật liệu nổi bật

        GetComponent<MeshRenderer>().material = highlightMaterial;
        GetComponent<MeshRenderer>().material.SetFloat("_OutlineWidth", 20f);
        GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.red);

    }

    private void OnMouseExit()
    {
        // Khôi phục vật liệu ban đầu khi chuột ra khỏi vật thể
        GetComponent<MeshRenderer>().material = originalMaterial;
    }
   
}
