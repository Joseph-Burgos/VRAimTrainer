// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;

// public class UpdatedTracking : Target_Parent
// {
//     //might need this object but unsure yet
//     public GameObject target = GameObject.Find("TrackingTarget");
//     public Transform starting = target.position;
//     public Vector3 startPos, endPos;
//     public float speed = 1.0f;
//     public bool repeatable = false;

//     private float startTime;
//     private float journeyLength;
//     public float duration = 3.0f;
//     public Transform[] allPos = new Vector3[4];

//     // Start is called before the first frame update
//     IEnumerator Start()
//     {
//         //predefining Vector3s where the object will go around
//         allPos[0] = new Vector3(Random.Range(-3.0f,12.0f), 6, Random.Range(-4.0f,10.0f));
//         allPos[1] = new Vector3(Random.Range(-3.0f,12.0f), 6, Random.Range(-4.0f,10.0f));
//         allPos[2] = new Vector3(Random.Range(-3.0f,12.0f), 6, Random.Range(-4.0f,10.0f));
//         allPos[3] = new Vector3(Random.Range(-3.0f,12.0f), 6, Random.Range(-4.0f,10.0f));
//         startTime = Time.time;
//         startPos = starting;
//         endPos = allPos[0];
//         journeyLength = Vector3.Distance(startPos.position,endPos.position);
//         while(repeatable){
//             if(startPos == starting){//this will supposedly only happen once since target startPos will be in a random location
//                 endPos = allPos[0];
//                 yield return RepeatLerp(startPos.position, endPos.position,duration);
//                 yield return RepeatLerp(endPos.position, startPos.position,duration);
//                 //changes pos of start and end to not go back in this if statement
//                 startPos = allPos[0];
//                 endPos = allPos[1];
//             }
//             else{
//                 bool check = true;
//                 int counter = 0;
//                 //startPos and endPos should somehow switch throughout the array Vector3s
//                 while(check){
//                     if(startPos == allPos[counter]){//checks to see what position startPos is in atm
//                         if(startPos == allPos[3]){//checks to see if this position is at last index so it doesnt go out of bounds
//                             startPos = allPos[4];
//                             endPos = allPos[0];
//                             check = false;
//                         }
//                         else if(startPos == allPos[4]){
//                             startPos = allPos[0];
//                             endPos = allPos[1];
//                             check = false;
//                         }
//                         else{
//                             counter++;
//                             startPos = allPos[counter];
//                             endPos = allPos[counter+1];
//                             check = false;
//                         }
//                     }
//                     counter++;
//                 }

//                 yield return RepeatLerp(startPos.position, endPos.position,duration);
//                 yield return RepeatLerp(endPos.position, startPos.position,duration);
//             }
            
//       }
//     }


//     public override void hit()
//     {
//         hitCount += Time.deltaTime;
//         //Debug.Log("Hit timer: " + hitCount);

//         //change color on hit
//         if (!isHighlighted)
//         {
//             switchColor(Highlight_Color);
//             isHighlighted = true;
//             //Debug.Log("IS HIGHLIGHTED");
//         }

//     }
//     // Update is called once per frame
//     void Update()
//     {
//         //runs path repeatedly until something happens or game decides to end
//         if(!repeatable){
//             float currentDuration = (startTime.time - startTime) * speed;
//             float journeyFraction = currentDuration / journeyLength;
//             this.transform.position = Vector3.Lerp(startPos.position, endPos.position, journeyFraction);
//         }
//     }

//     IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time){
//         //this creates the repeating lerp, goes up back to line 31
//         float i = 0.0f;
//         float rate = (1.0f / time) * speed;
//         while( i < 1.0f){
//             i += Time.deltaTime * rate;
//             this.transform.position = Vector3.Lerp(a,b,i);
//             yield return null;
//         }
//     }
// }
