using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private RectTransform wallpaper;
    [SerializeField] private float wallpaperWidth; //used for debugging
    [SerializeField] private float midWidth;
    [SerializeField] private float topWidth;

    [SerializeField] private List<RectTransform> botFurniture = new();
    [SerializeField] private List<RectTransform> midFurniture = new();
    [SerializeField] private List<RectTransform> topFurniture = new();

    [SerializeField] private RectTransform botLayer;
    [SerializeField] private RectTransform midLayer;
    [SerializeField] private RectTransform topLayer;

    [SerializeField] private float yCameraPosition;

    private float botOffset, midOffset, topOffset;
    Vector2 newMidOffset, newTopOffset;

    // Start is called before the first frame update
    void Start()
    {
        adjustLayerWidths();
        updateFurniturePos();
    }   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void adjustLayerWidths(){
        wallpaperWidth = wallpaper.rect.width;  

        /* extend mid and top layer lengths based on
        current room size */
        midOffset = wallpaperWidth/3;
        midWidth = wallpaperWidth + midOffset;

        topOffset = 2*(wallpaperWidth/3);
        topWidth = wallpaperWidth + topOffset;

        newMidOffset = midLayer.offsetMax;
        newTopOffset = topLayer.offsetMax;

        newMidOffset.x = midWidth;
        newTopOffset.x = topWidth;

        midLayer.offsetMax = newMidOffset;
        topLayer.offsetMax = newTopOffset;
    }

    private void updateFurniturePos(){
        Vector2 position = new();
        float oldX, newX;
        foreach(RectTransform furniture in midFurniture){
            position = furniture.anchoredPosition;
            Debug.Log("furniture: " + furniture + ", furniture anchored position: " + position);
            
            // scale x position to new layer length
            oldX = position.x;
            newX = midWidth*(oldX / wallpaperWidth);    
            Debug.Log("new X position: " + newX);

            // update furniture position
            position.x = newX;
            furniture.anchoredPosition = position;

            furniture.SetParent(midLayer);
        }
    }
}
