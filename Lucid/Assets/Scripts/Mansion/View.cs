using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    const float cameraWidth = 640;
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

    
    
    private Vector2 baseWall, baseBot, baseMid, baseTop;

    private float botOffset, midOffset, topOffset;
    private Vector2 newBotOffset, newMidOffset, newTopOffset;

    // Start is called before the first frame update
    void Start()
    {
        yCameraPosition = view.anchoredPosition.y;

        // Debug.Log("yCameraPosition: " + yCameraPosition);

        adjustLayers();
        updateFurniturePos(botFurniture, botWidth, botLayer);
        updateFurniturePos(midFurniture, midWidth, midLayer);
        updateFurniturePos(topFurniture, topWidth, topLayer);
        if(!isLeft){
            shiftLayersLeft();
        }
        

        ready = true; // signal to scene controller that all layers and furniture are ready for movement
    }   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void adjustLayers(){
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

        baseWall = wallLayer.anchoredPosition;
        baseBot = botLayer.anchoredPosition;
        baseMid = midLayer.anchoredPosition;
        baseTop = topLayer.anchoredPosition;
    }

    public void shiftLayersLeft(){
        float newWallX, newBotX, newMidX, newTopX; 
        Vector2 newWall, newBot, newMid, newTop;

        newWall = baseWall;
        newBot = baseBot;
        newMid = baseMid;
        newTop = baseTop;
        
        //update X anchored positions to the left of wall for right view
        newWallX = newWall.x - roomWidth + cameraWidth;
        newBotX = newBot.x - 2*(newBot.x - baseWall.x) - roomWidth + cameraWidth;;
        newMidX = newMid.x - 2*(newMid.x - baseWall.x) - roomWidth + cameraWidth;;
        newTopX = newTop.x - 2*(newTop.x - baseWall.x) - roomWidth + cameraWidth;;

        newWall.x = newWallX;
        newBot.x = newBotX;
        newMid.x = newMidX;
        newTop.x = newTopX;

        wallLayer.anchoredPosition = newWall;
        botLayer.anchoredPosition = newBot;
        midLayer.anchoredPosition = newMid;
        topLayer.anchoredPosition = newTop;
    }

    public void shiftLayersRight(){
        wallLayer.anchoredPosition = baseWall;
        botLayer.anchoredPosition = baseBot;
        midLayer.anchoredPosition = baseMid;
        topLayer.anchoredPosition = baseTop;
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
