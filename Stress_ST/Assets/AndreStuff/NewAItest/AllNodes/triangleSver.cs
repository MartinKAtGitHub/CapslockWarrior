using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triangleSver : MonoBehaviour {

	public Triangles1 group1;



	public Triangles1 getgroup1() {
		return group1;
	}

	public void setgroup(Triangles1 val) {
		group1 = val;
	}


}

public class Triangles1 {
	public Color TriangleColor;

	public Vector2 A;
	public Vector2 B;
	public Vector2 C;

	public Triangles AB;
	public Triangles AC;
	public Triangles BC;
}

[System.Serializable]
public class MapAlterations1 {

	public Vector2 A;
	public Vector2 B;
	public Vector2 C;
	public Vector2 D;

}

