using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Color32 color;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();  
    }
    void Start()
    {
        color.r = 255;
        color.g = 255;
        color.b = 100;
        sr.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
