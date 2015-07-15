using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadCreator : MonoBehaviour
{
	public static RoadCreator Instance {
		get {
			if (!_instance) {
				GameObject roadCreator = new GameObject ("RoadCreator");
				_instance = roadCreator.AddComponent<RoadCreator> ();
			}
			return _instance;
		}
	}

	private static RoadCreator _instance;
	[SerializeField]
	private GameObject
		Road, CrossIntersection, LIntersection, TriIntersection;
	private List<List<Edge>> HorList, VerList;
	void Awake ()
	{
		if (!_instance) {
			_instance = this;
		}
		HorList = new List<List<Edge>> ();
		VerList = new List<List<Edge>> ();
	}
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void CreateRoad (bool Vertical, Vector3 StartPoint)
	{
		if (Vertical) {
			StartCoroutine (VerticalRoad (StartPoint));
		}
	}

	IEnumerator VerticalRoad (Vector3 StartPoint)
	{
		bool tmp = true;
		List<GameObject> TmpPath = new List<GameObject> ();
		int LastDirection = 0;
		while (tmp) {
			Debug.Log ("------------");
			Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Vector3 EndPoint;
			float CellSize = ModuleCreator.Instance.GridCellSize;
			if (Physics.Raycast (r, out hit)) {
				EndPoint = hit.point;
				EndPoint.x = (EndPoint.x % CellSize < CellSize / 2f) ? CellSize * ((int)(EndPoint.x / CellSize)) : CellSize * ((int)(EndPoint.x / CellSize) + 1);
				EndPoint.z = (EndPoint.z % CellSize < CellSize / 2f) ? CellSize * ((int)(EndPoint.z / CellSize)) : CellSize * ((int)(EndPoint.z / CellSize) + 1);
			} else {
				EndPoint = StartPoint;
			}

			int size = Mathf.FloorToInt (Mathf.Abs ((StartPoint - EndPoint).z / CellSize)) + 1;
			int direction = (int)Mathf.Sign ((StartPoint - EndPoint).z);
			if (direction != LastDirection) {
				foreach (var ob in TmpPath) {
					Destroy (ob);
				}
				TmpPath.Clear ();
			}
			Debug.Log ("size : " + size.ToString ());
			Debug.Log ("direction : " + direction.ToString ());
			Debug.Log ("TmpPath.count : " + TmpPath.Count.ToString ());
			if (size > TmpPath.Count) {
				for (int i = TmpPath.Count; i < size; i++) {
					TmpPath.Add (Instantiate (Road, StartPoint + Vector3.back * i * direction, Quaternion.identity) as GameObject);
				}
			} else if (size < TmpPath.Count) {
				for (int i = TmpPath.Count; i > size; i--) {
					Debug.Log (i - 1);
					Destroy (TmpPath [i - 1]);
					TmpPath.RemoveAt (i - 1);
				}
			}

			LastDirection = direction;
			yield return null;
		}
	}

	/*IEnumerator HorizontalRoad(Vector3 StartPoint){
		
	}*/
}
