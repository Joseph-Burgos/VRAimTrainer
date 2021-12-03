using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevel(string level){
        SceneManager.LoadScene(level);
    }

    public string GetSceneName(){
        return SceneManager.GetActiveScene().name;
    }
}
