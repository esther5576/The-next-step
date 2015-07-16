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
		Road;
	private bool _onCreation = false;

	void Awake ()
	{
		if (!_instance) {
			_instance = this;
		}

	}
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Z) && !_onCreation) {
			_onCreation = true;
			Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (r, out hit)) {
				Vector3 StPt = hit.point;
				float CellSize = ModuleCreator.Instance.GridCellSize;
				StPt.x = (StPt.x % CellSize < CellSize / 2f) ? CellSize * ((int)(StPt.x / CellSize)) : CellSize * ((int)(StPt.x / CellSize) + 1);
				StPt.z = (StPt.z % CellSize < CellSize / 2f) ? CellSize * ((int)(StPt.z / CellSize)) : CellSize * ((int)(StPt.z / CellSize) + 1);
				StartCoroutine (CreateRoad (StPt));
			}
		}
	}

	IEnumerator CreateRoad (Vector3 StartPoint)
	{
		bool tmp = true;
		List<GameObject> TmpPath = new List<GameObject> ();
		Vector2 LastDirection = Vector2.zero;
		while (tmp) {
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

			Vector3 road = StartPoint - EndPoint;
			Vector2 direction = Vector2.zero;

			if (Mathf.Abs (road.z) > Mathf.Abs (road.x)) {
				int size = Mathf.FloorToInt (Mathf.Abs (road.z / CellSize)) + 1;
				direction.y = (int)Mathf.Sign (road.z);
				if (direction != LastDirection) {
					foreach (var ob in TmpPath) {
						Destroy (ob);
					}
					TmpPath.Clear ();
					TmpPath.Add (Instantiate (Road, StartPoint, Quaternion.identity) as GameObject);
				}
				Debug.Log (size);
				if (size > TmpPath.Count) {
					for (int i = TmpPath.Count; i < size; i++) {
						Vector3 pos = StartPoint + Vector3.back * i * direction.y;
						RaycastHit HeightHit;
						if (Physics.Raycast (pos + Vector3.up * 10, Vector3.down, out HeightHit)) {
							pos.y = HeightHit.point.y;
						}
						if (HeightHit.collider.tag != "Pipeline") {
							Vector3 normal = HeightHit.normal;
							normal.x = 0;
							TmpPath.Add (Instantiate (Road, pos, Quaternion.identity) as GameObject);
							TmpPath [i].transform.LookAt (TmpPath [i].transform.position + normal);
						} else {
							TmpPath.Add (null);
						}
					}
				} else if (size < TmpPath.Count) {
					for (int i = TmpPath.Count; i > size; i--) {
						Destroy (TmpPath [i - 1]);
						TmpPath.RemoveAt (i - 1);
					}
				}

			} else {
				int size = Mathf.FloorToInt (Mathf.Abs (road.x / CellSize)) + 1;
				direction.x = (int)Mathf.Sign (road.x);
				if (direction != LastDirection) {
					foreach (var ob in TmpPath) {
						Destroy (ob);
					}	
					TmpPath.Clear ();
					TmpPath.Add (Instantiate (Road, StartPoint, Quaternion.identity) as GameObject);
				}
				Debug.Log (size);
				if (size > TmpPath.Count) {
					for (int i = TmpPath.Count; i < size; i++) {
						Vector3 pos = StartPoint + Vector3.left * i * direction.x;
						RaycastHit HeightHit;
						if (Physics.Raycast (pos + Vector3.up * 10, Vector3.down, out HeightHit)) {
							pos.y = HeightHit.point.y;
						}
						if (HeightHit.collider.tag != "Pipeline") {
							Vector3 normal = HeightHit.normal;
							normal.z = 0;
							TmpPath.Add (Instantiate (Road, pos, Quaternion.identity) as GameObject);
							TmpPath [i].transform.LookAt (TmpPath [i].transform.position + normal);
						} else {
							TmpPath.Add (null);
						}
					}
				} else if (size < TmpPath.Count) {
					for (int i = TmpPath.Count; i > size; i--) {
						Destroy (TmpPath [i - 1]);
						TmpPath.RemoveAt (i - 1);
					}
				}
			}
			LastDirection = direction;
			if (Input.GetMouseButtonDown (0) && Input.GetKey (KeyCode.LeftShift)) {
				StartPoint = TmpPath [TmpPath.Count - 1].transform.position;
				TmpPath.Clear ();

			} else if (Input.GetMouseButtonDown (0))
				tmp = false;
			yield return null;
		}
		_onCreation = false;
	}


}
