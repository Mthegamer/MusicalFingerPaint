using UnityEngine;
using System.Collections;

public class PointFollow : MonoBehaviour {

	void Update () {
	  transform.position = GameObject.Find("Canvas").GetComponent<Automation>().GetLastPoint();
	}
}
