using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepScore : MonoBehaviour {

  public static int BlueScore = 0;
  public static int GreenScore = 0;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnGUI(){

        GUI.Box (new Rect (100, 100, 100, 100), BlueScore.ToString());
        GUI.Box (new Rect (975, 100, 100, 100), GreenScore.ToString());
    }
}
