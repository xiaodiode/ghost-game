using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] public bool ready = false;
    [SerializeField] public bool isLeft;
    [SerializeField] public RectTransform view;
    [SerializeField] public float yCameraPosition;
    [SerializeField] public float roomWidth, botWidth, midWidth, topWidth;
    [SerializeField] public RectTransform wallLayer, botLayer, midLayer, topLayer;

    [SerializeField] private List<RectTransform> wallFurniture = new(); // for organization purposes
    [SerializeField] private List<RectTransform> botFurniture = new();  
    [SerializeField] private List<RectTransform> midFurniture = new();
    [SerializeField] private List<RectTransform> topFurniture = new();

    

    private float botOffset, midOffset, topOffset;
    private Vector2 newBotOffset, newMidOffset, newTopOffset;

    // Start is called before the first frame update
    void Start()
    {
        yCameraPosition = view.anchoredPosition.y;
        // Debug.Log("yCameraPosition: " + yCameraPosition);

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
        float wallX;

        wallX = wallLayer.anchoredPosition.x;
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
        // Debug.Log("botwidth, midwidth, topwidth: " + botWidth + midWidth + topWidth);

        botLayer.offsetMax = newBotOffset;
        midLayer.offsetMax = newMidOffset;
        topLayer.offsetMax = newTopOffset;

        if(!isLeft){
            float newBotX, newMidX, newTopX; 
            Vector2 newBot, newMid, newTop;

            newBot = botLayer.anchoredPosition;
            newMid = midLayer.anchoredPosition;
            newTop = topLayer.anchoredPosition;

            newBotX = botLayer.anchoredPosition.x;
            newMidX = midLayer.anchoredPosition.x;
            newTopX = topLayer.anchoredPosition.x;

            //update X anchored positions to the left of wall for right view
            newBotX = newBotX - 2*(newBotX - wallX);
            newMidX = newMidX - 2*(newMidX - wallX);
            newTopX = newTopX - 2*(newTopX - wallX);

            newBot.x = newBotX;
            newMid.x = newMidX;
            newTop.x = newTopX;

            botLayer.anchoredPosition = newBot;
            midLayer.anchoredPosition = newMid;
            topLayer.anchoredPosition = newTop;
        }
    }

    private void updateFurniturePos(List<RectTransform> furnitureList, float newWidth, RectTransform newLayer){
        Vector2 position;
        float oldX, newX;
        foreach(RectTransform furniture in furnitureList){
            position = furniture.anchoredPosition;
            // Debug.Log("furniture: " + furniture + ", furniture anchored position: " + position);
            
            // scale x position to new layer length
            oldX = position.x;
            newX = newWidth*(oldX / roomWidth);
            
            // Debug.Log("new X position: " + newX);

            // update furniture position
            position.x = newX;
            furniture.anchoredPosition = position;

            furniture.SetParent(newLayer);
        }
    }
}
