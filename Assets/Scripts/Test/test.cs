using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {



    // Use this for initialization
    void Start () {
        testsingle.Instance.test();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Quit()
    {
    #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }

}
