using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public int numLayers = 4;
    
    public bool ready = false;

    public float roomWidth, botWidth, midWidth, topWidth;
    public RectTransform wallLayer, botLayer, midLayer, topLayer;

    [SerializeField] private List<RectTransform> wallFurniture = new(); // for organization purposes
    [SerializeField] private List<RectTransform> botFurniture = new();  
    [SerializeField] private List<RectTransform> midFurniture = new();
    [SerializeField] private List<RectTransform> topFurniture = new();

    [SerializeField] private float yCameraPosition;

    private float botOffset, midOffset, topOffset;
    private Vector2 newBotOffset, newMidOffset, newTopOffset;

    // Start is called before the first frame update
    void Start()
    {
        adjustLayerWidths();
        updateFurniturePos(botFurniture, botWidth, botLayer);
        updateFurniturePos(midFurniture, midWidth, midLayer);
        updateFurniturePos(topFurniture, topWidth, topLayer);

        ready = true; // signal to scene controller that all layers and furniture are ready for movement
    }   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void adjustLayerWidths(){
        roomWidth = wallLayer.rect.width;  

        /* extend bot, mid, and top layer lengths 
        based on current room size */
        botOffset = roomWidth/10;
        midOffset = 3*botOffset;
        topOffset = 3*midOffset;

        botWidth = roomWidth + botOffset;
        midWidth = roomWidth + midOffset;
        topWidth = roomWidth + topOffset;

        /* update layer lengths in game */
        newBotOffset = botLayer.offsetMax;
        newMidOffset = midLayer.offsetMax;
        newTopOffset = topLayer.offsetMax;

        newBotOffset.x = botWidth;
        newMidOffset.x = midWidth;
        newTopOffset.x = topWidth;

        botLayer.offsetMax = newBotOffset;
        midLayer.offsetMax = newMidOffset;
        topLayer.offsetMax = newTopOffset;
    }

    private void updateFurniturePos(List<RectTransform> furnitureList, float newWidth, RectTransform newLayer){
        Vector2 position;
        float oldX, newX;
        foreach(RectTransform furniture in furnitureList){
            position = furniture.anchoredPosition;
            Debug.Log("furniture: " + furniture + ", furniture anchored position: " + position);
            
            // scale x position to new layer length
            oldX = position.x;
            newX = newWidth*(oldX / roomWidth);    
            Debug.Log("new X position: " + newX);

            // update furniture position
            position.x = newX;
            furniture.anchoredPosition = position;

            furniture.SetParent(newLayer);
        }
    }
}
