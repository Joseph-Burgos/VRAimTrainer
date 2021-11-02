using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.IO;

/* 
* This class is based on a feature developed by Code Monkey (unitycodemonkey.com).
*/
public class Graph : MonoBehaviour {
    [SerializeField] private Sprite circleSprite; // node 
    // parent objects
    private RectTransform graphContainerTransform; 
    // References to graph elements
    private Transform horizontalGridMarkerTemplate;
    // private LineRenderer horizontalGridMarkerTemplate;

    // data for generating graph
    private float xLength;
    private float yLength;


    private void Awake() {
        // Retrieve references to necessary game objects
        graphContainerTransform = transform.Find("GraphContainer").GetComponent<RectTransform>();
        horizontalGridMarkerTemplate = graphContainerTransform.Find("HorizontalGridMarkerTemplate"); //.GetComponent<LineRenderer>();
        // horizontalGridMarkerTemplate = 
        // set values
        initializeGraphData();
        // Testing
        // List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17 };
        createGraph();
    }

    public void createGraph() {
        CreateCircle(new Vector2(0, 0));
        drawGrid(5, 0);
    }

    private void initializeGraphData() {
        xLength = graphContainerTransform.sizeDelta.x;
        yLength = graphContainerTransform.sizeDelta.y;
    }

    private void drawGrid(int xGrids, int yGrids) {
        // define distances between grids
        float xGridDistance = (xGrids > 0) ? yLength / xGrids: 0;
        float yGridDistance = (yGrids > 0) ? xLength / yGrids : 0;
        // x
        // for num of x grids
        float currentYValue = 0;
        for (int i = 0; i < xGrids; i++) {
            // create new line from the template
            Transform xGridTransform = Instantiate(horizontalGridMarkerTemplate, graphContainerTransform);
            LineRenderer xGridLineRenderer = xGridTransform.GetComponent<LineRenderer>();
            // make an array of 2 vector3 elements for endpoints of the line
            var points = new Vector3[2];
            points[0] = new Vector3(0, currentYValue, 0);
            points[1] = new Vector3(xLength, currentYValue, 0);
            xGridLineRenderer.SetPositions(points);
            xGridTransform.gameObject.SetActive(false); // set as active
            currentYValue += xGridDistance;
        }
        // height = previous height + (i * yLength / xGrids)
        // set the parent and draw the divider line
    }

    private GameObject CreateCircle(Vector2 anchoredPosition) {
        Debug.Log("Entering create circle");
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainerTransform, false);
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
