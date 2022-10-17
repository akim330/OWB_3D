using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayFPS : MonoBehaviour
{
     public TextMeshProUGUI fpsText;
     public float deltaTime;

     void Update () {
        //deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        deltaTime = Time.deltaTime;
         float fps = 1.0f / Time.deltaTime;
         fpsText.text = Mathf.Ceil (fps).ToString ();
     }
 
}
