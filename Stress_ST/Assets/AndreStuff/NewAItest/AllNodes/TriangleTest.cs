using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleTest : MonoBehaviour {

	[Tooltip("This Is The Maximum Points At Which The Map Can Have, Maximum Depth, To The Maximum Left And Maximum Right.  The Same With The Top.\n\nReasoning For This Is So That It's Easier To Add Stuff Later")]
	public Vector2[] MapCornerPoints = new Vector2[4];

	[Tooltip("Changes From A Perfect Rectangle To The Current Where New Shapes In From Of Rectangles Are Added One By One")]
	public MapAlterations[] CoreMapAlterations;//Holding All Objects, So That If Stuff Goes Wrong I Can Reload?

	public Vector2[] MapObject = new Vector2[4];





	int ArrayLength = 0;

	public Vector3 LastUsedPoints;

	public Vector3[] OpenPoints;

	//public Vector3[] OpenPointsAbove;
	//public Vector3[] OpenPointsBelow;
	//public Vector3[] OpenPointsLeft;
	//public Vector3[] OpenPointsRight;


	Triangles testing = new Triangles();

	List<Triangles> TrianglesGroup = new List<Triangles>();


	// Use this for initialization
	void Start() {

		//	CreateStartTriangle();
		FindNewPoints();
		
	}




	void CreateStartTriangle() {//The Room Is A Square/Rectangle.   Aditional Objects Are Added Later.
		testing.test = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

		testing.A = MapCornerPoints[0];
		testing.B = MapCornerPoints[1];
		testing.C = MapCornerPoints[2];


		TrianglesGroup.Add(testing);
		testing = new Triangles();

		testing.test = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		testing.A = MapCornerPoints[3];
		testing.B = MapCornerPoints[0];
		testing.C = MapCornerPoints[2];

		TrianglesGroup.Add(testing);
	}

	void MapModifyers() {//The Room Is A Square/Rectangle.   Aditional Objects Are Added Later.

		//TODO DO SOMETHING

	}

	void FindNewPoints() {

		float pointX = 4;
		float pointY = 0;

		float triangleAX = 1;
		float triangleAY = 1;
		float triangleBX = 2;
		float triangleBY = 3;
		float triangleCX = 3;
		float triangleCY = 1;
	
		Debug.Log(intpoint_inside_trigon(pointX, pointY, triangleAX, triangleAY, triangleBX, triangleBY, triangleCX, triangleCY));



	}




	float as_x = 0;//This Is Called Quite Often. Dont Want To Create And Destroy Variables That Often
	float as_y = 0;
	bool s_ab = false;

	//Found This On Zer Internett. Made Tiny Changes, stackoverflow.com/questions/2049582/how-to-determine-if-a-point-is-in-a-2d-triangle
	bool intpoint_inside_trigon(float sX, float sY, float aX, float aY, float bX, float bY, float cX, float cY) {//Checking If sX || sY (Which Is The Point Im Checking) Is Inside The 3 Points Of The Triangle
		as_x = sX - aX;
		as_y = sY - aY;

		s_ab = (bX - aX) * as_y - (bY - aY) * as_x > 0;

		if ((cX - aX) * as_y - (cY - aY) * as_x > 0 == s_ab) return false;

		if ((cX - bX) * (sY - bY) - (cY - bY) * (sX - bX) > 0 != s_ab) return false;

		return true;
	}




	public bool DrawOnce = false;

	void OnDrawGizmosSelected() {
		// Display the explosion radius when selected

			if (TrianglesGroup.Count > 0) {
				for (int i = 0; i < TrianglesGroup.Count; i++) {
					
					Gizmos.color = TrianglesGroup[i].test;
					Gizmos.DrawLine(TrianglesGroup[i].A, TrianglesGroup[i].B);
					Gizmos.DrawLine(TrianglesGroup[i].B, TrianglesGroup[i].C);
					Gizmos.DrawLine(TrianglesGroup[i].C, TrianglesGroup[i].A);
				}
			}
	}
}

public class Triangles {
	public Color test;

	public Vector2 A;
	public Vector2 B;
	public Vector2 C;

	public Triangles AB;
	public Triangles AC;
	public Triangles BC;
}

[System.Serializable]
public class MapAlterations {

	public Vector2 A;
	public Vector2 B;
	public Vector2 C;
	public Vector2 D;

}