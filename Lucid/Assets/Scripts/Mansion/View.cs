using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private float wallpaperWidth;

    [SerializeField] private List<Furniture> backFurniture = new();
    [SerializeField] private List<Furniture> midFurniture = new();
    [SerializeField] private List<Furniture> frontFurniture = new();

    [SerializeField] private RectTransform backLayer;
    [SerializeField] private RectTransform middleLayer;
    [SerializeField] private RectTransform frontLayer;

    [SerializeField] private float yCameraPosition;

    private float backOffset, midOffset, frontOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
