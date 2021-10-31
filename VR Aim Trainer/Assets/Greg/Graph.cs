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
    // Transforms used to draw graph elements
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;
    // game objects comprising an active graph

    private List<GameObject> gameObjectList; 
    private void Awake() {
        Debug.Log("Graph is awake!");
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        Debug.Log("Graph container is found!");
        // labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        // labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        // dashTemplateX = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>();
        // dashTemplateY = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();

        // gameObjectList = new List<GameObject>();
        // List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17 };
        // ShowGraph(valueList, (int _i) => "Game " + (_i + 1), (float _f) => "" + Mathf.RoundToInt(_f));

        // Testing
        CreateCircle(new Vector2(0, 0)); 
        Debug.Log("We created a new circle!!!");
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
        Debug.Log("LEaving create circle");
        return gameObject;
    }

    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0); 
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f; // anchored position is halfway point in line
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
        return gameObject;
    }

    private void ShowGraph(List<int> valueList, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null) {
        // assign optional lambda parameters
        if (getAxisLabelX == null) {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null) {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }
        // ???? - new graph data possibly
        foreach (GameObject gameObject in gameObjectList) {
            Destroy(gameObject);
        }
        gameObjectList.Clear();

        // define height of graph
        float graphHeight = graphContainer.sizeDelta.y;
        // identify max and min y values
        float yMaximum = valueList[0];
        float yMinimum = valueList[0];
        foreach (int value in valueList) {
            if (value > yMaximum) {
                yMaximum = value;
            }
            if (value < yMinimum) {
                yMinimum = value;
            }
        }
        // set a buffer above the maximum and minimum points
        yMaximum = yMaximum + ((yMaximum - yMinimum) * 0.2f);
        yMinimum = yMinimum - ((yMaximum - yMinimum) * 0.2f);

        float xSize = 50f; // x-axis grid size

        GameObject lastCircleGameObject = null;
        // from left to right, begin drawing the graph out
        for (int i = 0; i < valueList.Count; i++) {
            // place node
            float xPosition = xSize + i * xSize;
            float yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            gameObjectList.Add(circleGameObject);
            if (lastCircleGameObject != null) { // if not the very first node on the graph then connect this node with the previous one
                GameObject dotConnectionGameObject = CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                gameObjectList.Add(dotConnectionGameObject);
            }
            lastCircleGameObject = circleGameObject;
            // write the x label
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -7f); // label location
            labelX.GetComponent<Text>().text = getAxisLabelX(i);
            gameObjectList.Add(labelX.gameObject);
            // mark a dash on the x axis
            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, -3f); // dashed-line location
            gameObjectList.Add(dashX.gameObject);
        }

        int separatorCount = 10; // number of divisions on the y-axis
        // for each division along the y-axis
        for (int i = 0; i <= separatorCount; i++) {
            // label division
            RectTransform labelY = Instantiate(labelTemplateY); 
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount; // determine height at which to place division
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight); // label location
            // call lambda function to generate the label value
            labelY.GetComponent<Text>().text = getAxisLabelY(yMinimum + (normalizedValue * (yMaximum - yMinimum)));
            gameObjectList.Add(labelY.gameObject);
            // draw dash to indicate division
            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);  // dashed-line location
            gameObjectList.Add(dashY.gameObject);
        }
    } // end showgraph
    int GetAngleFromVectorFloat(Vector2 dir) { return -1; }

}
