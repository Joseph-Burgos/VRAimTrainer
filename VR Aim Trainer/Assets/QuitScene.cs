using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitScene : MonoBehaviour
{
    public void QuitGame(){
        
        #if UNITY_EDITOR 
            UnityEditor.EditorApplication.isPlaying = false;//checks if game is being played or no
        #else 
            Application.Quit();//Quits game
        #endif
    }
}
