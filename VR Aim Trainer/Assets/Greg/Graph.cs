using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.IO;

public class Graph : MonoBehaviour {
    [SerializeField] private Sprite circleSprite; // node 
    // parent objects
    private RectTransform graphContainerTransform; 
    // References to graph elements
    private Transform horizontalGridMarkerTemplate;
    private Transform verticalGridMarkerTemplate;
    private Transform dotConnection;

    private Transform yAxisHigh;
    private Transform yAxisMid;
    // private LineRenderer horizontalGridMarkerTemplate;

    // data for generating graph
    private float xLength;
    private float yLength;


    private void Awake() {
        // Retrieve references to necessary game objects
        graphContainerTransform = transform.Find("GraphContainer").GetComponent<RectTransform>();
        horizontalGridMarkerTemplate = graphContainerTransform.Find("HorizontalGridMarkerTemplate"); //.GetComponent<LineRenderer>();
        verticalGridMarkerTemplate = graphContainerTransform.Find("VerticalGridMarkerTemplate");
        dotConnection = graphContainerTransform.Find("DotConnection");
        yAxisHigh = graphContainerTransform.Find("YAxisHigh");
        yAxisMid = graphContainerTransform.Find("YAxisMid");
        // set values
        initializeGraphData();
        // Testing
        //Vector2[] testNodes = {new Vector2(0, 0), new Vector2(1, 0.2f), new Vector2(2.0f, 0.6f), new Vector2(2.6f, 0.8f)};
        //createGraph(testNodes);
        //String[] testLabels = {"string one", "string two"};
        //drawXLabels(testLabels);
        //drawYLabels(testLabels);
    }

    public void createGraph(List<Tuple<float, float>> data, bool percentageGraph) {
        // Debug.Log("Graph - enter create graphs");
        Vector2[] normalizedNodes = NormalizeData(data, percentageGraph);
        // Draw grid
        drawGrid(5, 5);

        // Start actual function
        // for each node in nodes invoke CreateCircle
        foreach (Vector2 node in normalizedNodes)
        {
            CreateCircle(node);
        }
        // sort Vector2 nodes based on their x value
        //https://stackoverflow.com/questions/12026344/how-to-use-array-sort-to-sort-an-array-of-structs-by-a-specific-element
        // with a linerenderer draw a connecting line using nodes
        LineRenderer dotConnectionRenderer = dotConnection.GetComponent<LineRenderer>();
        // make an array of 2 vector3 elements for endpoints of the line
        Vector3[] nodes3 = normalizedNodes.Select(node => new Vector3(node.x, node.y, 0)).Cast<Vector3>().ToArray();
        dotConnectionRenderer.positionCount = nodes3.Length;
        dotConnectionRenderer.SetPositions(nodes3);
        dotConnectionRenderer.gameObject.SetActive(true); // set as active
        // Debug.Log("Graph - exit create graphs");
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
            xGridTransform.gameObject.SetActive(true); // set as active
            currentYValue += xGridDistance;
        }
        // height = previous height + (i * yLength / xGrids)
        // set the parent and draw the divider line

        float currentXValue = 0;
        for (int i = 0; i < yGrids; i++) {
            // create new line from the template
            Transform yGridTransform = Instantiate(verticalGridMarkerTemplate, graphContainerTransform);
            LineRenderer yGridLineRenderer = yGridTransform.GetComponent<LineRenderer>();
            // make an array of 2 vector3 elements for endpoints of the line
            var points = new Vector3[2];
            points[0] = new Vector3(currentXValue, 0, 0);
            points[1] = new Vector3(currentXValue, yLength, 0);
            yGridLineRenderer.SetPositions(points);
            yGridTransform.gameObject.SetActive(true); // set as active
            currentXValue += yGridDistance;
        }
    }

    //private void drawXLabels(String[] labels) {
    //    if (labels.Length > 0) {
    //        float spaceInBetween = labels.Length  > 1 ? (xLength / (labels.Length - 1)) : 0;
    //        for (int i = 0; i < labels.Length; i++) {
    //            String label = labels[i];
    //            Transform xAxisLabel = Instantiate(xLabelTemplate, graphContainerTransform);
    //            // position label along axis with respect to position of template
    //            RectTransform xLabelRectTransform = xAxisLabel.GetComponent<RectTransform>();
    //            Vector3 currentPosition = xLabelRectTransform.anchoredPosition3D;
    //            float currentXPosition = currentPosition.x;
    //            float newXPosition = currentXPosition + (spaceInBetween * i);
    //            xLabelRectTransform.anchoredPosition3D = new Vector3(newXPosition, currentPosition.y, currentPosition.z);
    //            // set text
    //            xAxisLabel.GetComponent<TMPro.TextMeshPro>().text = label;
    //            // render label
    //            xAxisLabel.gameObject.SetActive(true);
    //        }
    //    }
        
    //}

    //private void drawYLabels(String[] labels) {
    //    if (labels.Length > 0) {
    //        float spaceInBetween = labels.Length  > 1 ? (yLength / (labels.Length - 1)) : 0;
    //        for (int i = 0; i < labels.Length; i++) {
    //            String label = labels[i];
    //            Transform yAxisLabel = Instantiate(yLabelTemplate, graphContainerTransform);
    //            // position label along axis with respect to position of template
    //            RectTransform yLabelRectTransform = yAxisLabel.GetComponent<RectTransform>();
    //            Vector3 currentPosition = yLabelRectTransform.anchoredPosition3D;
    //            float currentYPosition = currentPosition.y;
    //            float newYPosition = currentYPosition + (spaceInBetween * i);
    //            yLabelRectTransform.anchoredPosition3D = new Vector3(currentPosition.x, newYPosition, currentPosition.z);
    //            // set text
    //            yAxisLabel.GetComponent<TMPro.TextMeshPro>().text = label;
    //            // render label
    //            yAxisLabel.gameObject.SetActive(true);
    //        }
    //    }
        
    //}

    private GameObject CreateCircle(Vector2 anchoredPosition) { 
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
        return gameObject;
    }

    public Vector2[] NormalizeData(List<Tuple<float, float>> data, bool percentage) {
        // Debug.Log("Graph - Entering NormalizeData");
        float upperLimitX = data.Max(d => d.Item1); 
        float upperLimitY = percentage ? 1.0f : data.Max(d => d.Item2);
        if (!percentage) {
            // round up the Y limit 
            upperLimitY += 50;
            while (upperLimitY % 100 != 0) { upperLimitY++; }
        }
        yAxisHigh.GetComponent<TMPro.TextMeshPro>().text = upperLimitY.ToString();
        yAxisMid.GetComponent<TMPro.TextMeshPro>().text = (upperLimitY/2).ToString();
        Vector2[] normalizedData = data.OrderBy(d => d.Item1).Select( d => {
                float normalizedXPos = xLength * (d.Item1 / upperLimitX);
                float normalizedYPos = yLength * (d.Item2 / upperLimitY);
                return new Vector2(normalizedXPos, normalizedYPos);
            }).Cast<Vector2>().ToArray();
        // Debug.Log("Graph - Exiting NormalizeData");
        return normalizedData;
    }

}
