using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.IO;

/* 
* This class is based on a feature developed by Code Monkey.
* unitycodemonkey.com
*/
public class Graph : MonoBehaviour {
    [SerializeField] private Sprite circleSprite; // node 
    // parent object
    private RectTransform graphContainer; 
    // References to graph elements

    // data for generating graph
    private float xLength;
    private float yLength;


    private void Awake() {
        // Retrieve references to necessary game objects
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        // set values
        initializeGraphData();
        // Testing
        // List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17 };
        createGraph();
    }

    public void createGraph() {
        CreateCircle(new Vector2(0, 0));
    }

    private void initializeGraphData() {
        xLength = graphContainer.sizeDelta.x;
        yLength = graphContainer.sizeDelta.y;
    }

    private void setDividers(int xDividers, int yDividers) {

    }

    private GameObject CreateCircle(Vector2 anchoredPosition) {
        Debug.Log("Entering create circle");
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        double horizontalSize = 0.1;
        double verticalSize = 0.1;
        rectTransform.sizeDelta = new Vector2(0.1f, 0.1f);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        // rectTransform.pivot = new Vector2(0, 0);
        Debug.Log("LEaving create circle");
        return gameObject;
    }

}
