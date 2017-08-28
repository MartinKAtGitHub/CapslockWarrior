using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour {

	public Vector3 Offset = Vector3.zero;
	public Transform PositionUpdateTo;

	// Update is called once per frame
	void Update () {
		transform.position = PositionUpdateTo.position + Offset;
	}
}
