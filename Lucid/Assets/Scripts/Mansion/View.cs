using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private RectTransform wallpaper;
    [SerializeField] private float wallpaperWidth; //used for debugging

    [SerializeField] private List<Furniture> backFurniture = new();
    [SerializeField] private List<Furniture> midFurniture = new();
    [SerializeField] private List<Furniture> frontFurniture = new();

    [SerializeField] private RectTransform backLayer;
    [SerializeField] private RectTransform middleLayer;
    [SerializeField] private RectTransform frontLayer;

    [SerializeField] private float yCameraPosition;

    private float backOffset, midOffset, frontOffset;
    Vector2 newMidOffset, newFrontOffset;

    // Start is called before the first frame update
    void Start()
    {
        wallpaperWidth = wallpaper.rect.width;

        midOffset = wallpaperWidth/3;
        frontOffset = 2*(wallpaperWidth/3);

        newMidOffset = middleLayer.offsetMax;
        newFrontOffset = frontLayer.offsetMax;

        newMidOffset.x = midOffset;
        newFrontOffset.x = frontOffset;

        middleLayer.offsetMax = newMidOffset;
        frontLayer.offsetMax = newFrontOffset;
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
