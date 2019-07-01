using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testsingle : SingletonMono<testsingle> {

	public void test()
    {
        Debug.Log("111");
        
    }
}
