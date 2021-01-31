using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public string sceneIndex = "";
    public void ChangeScene()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>().ChangeScene(sceneIndex);
    }
}
