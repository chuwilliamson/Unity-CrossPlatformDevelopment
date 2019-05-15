using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables;

public class Vector2VariableMonitor : MonoBehaviour
{

    public Vector2Variable V2Variable;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		V2Variable.Value = new Vector2(Screen.width, Screen.height);
	}
}
