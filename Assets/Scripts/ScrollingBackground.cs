using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    [SerializeField] float scrollSpeed = 0.5f;
    private Renderer quadRenderer;

    private void Start()
    {
        quadRenderer = gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        Vector2 textureOffset = new Vector2(Time.time * scrollSpeed, 0);
        quadRenderer.material.mainTextureOffset = textureOffset;
    }

}
