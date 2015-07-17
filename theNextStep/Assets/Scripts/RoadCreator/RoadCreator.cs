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
	private Grid _grid;

	void Awake ()
	{
		if (!_instance) {
			_instance = this;
		}

	}
	// Use this for initialization
	void Start ()
	{
		_grid = Terrain.activeTerrain.GetComponent<Grid> ();
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
		GameObject TmpPath = new GameObject ("TemporaryPath");
		Vector2 LastDirection = Vector2.zero;
		_grid.AddNode (new Node ((int)StartPoint.x, (int)StartPoint.z, _grid));
		Node StartNode = _grid.Map [(int)StartPoint.x, (int)StartPoint.z];
		List<Node> IntersectNodes = new List<Node> ();
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
					foreach (Transform ob in TmpPath.transform) {
						Destroy (ob.gameObject);
					}
					TmpPath.transform.DetachChildren ();
					(Instantiate (Road, StartPoint, Quaternion.identity) as GameObject).transform.parent = TmpPath.transform;
				}

				if (size > TmpPath.transform.childCount) {
					for (int i = TmpPath.transform.childCount; i < size; i++) {
						Vector3 pos = StartPoint + Vector3.back * i * direction.y;
						RaycastHit HeightHit;
						if (Physics.Raycast (pos + Vector3.up * 10, Vector3.down, out HeightHit)) {
							pos.y = HeightHit.point.y;
						}
						if (HeightHit.collider.tag != "Pipeline") {
							Vector3 normal = HeightHit.normal;
							normal.x = 0;
							(Instantiate (Road, pos, Quaternion.identity) as GameObject).transform.parent = TmpPath.transform;
							TmpPath.transform.GetChild (i).LookAt (TmpPath.transform.GetChild (i).position + normal);
						} else {
							GameObject nul = new GameObject ("null");
							nul.transform.parent = TmpPath.transform;
							IntersectNodes.Add (new Node ((int)HeightHit.point.x, (int)HeightHit.point.z, _grid));
						}
					}
				} else if (size < TmpPath.transform.childCount) {
					for (int i = TmpPath.transform.childCount; i > size; i--) {
						Destroy (TmpPath.transform.GetChild (i - 1).gameObject);
						TmpPath.transform.GetChild (i - 1).parent = null;
					}
				}

			} else {
				int size = Mathf.FloorToInt (Mathf.Abs (road.x / CellSize)) + 1;
				direction.x = (int)Mathf.Sign (road.x);
				if (direction != LastDirection) {
					foreach (Transform ob in TmpPath.transform) {
						Destroy (ob.gameObject);
					}
					TmpPath.transform.DetachChildren ();
					(Instantiate (Road, StartPoint, Quaternion.identity) as GameObject).transform.parent = TmpPath.transform;
				}

				if (size > TmpPath.transform.childCount) {
					for (int i = TmpPath.transform.childCount; i < size; i++) {
						Vector3 pos = StartPoint + Vector3.left * i * direction.x;
						RaycastHit HeightHit;
						if (Physics.Raycast (pos + Vector3.up * 10, Vector3.down, out HeightHit)) {
							pos.y = HeightHit.point.y;
						}
						if (HeightHit.collider.tag != "Pipeline") {
							Vector3 normal = HeightHit.normal;
							normal.z = 0;
							(Instantiate (Road, pos, Quaternion.identity) as GameObject).transform.parent = TmpPath.transform;
							TmpPath.transform.GetChild (i).LookAt (TmpPath.transform.GetChild (i).position + normal);
						} else {
							GameObject nul = new GameObject ("null");
							nul.transform.parent = TmpPath.transform;
							IntersectNodes.Add (new Node ((int)HeightHit.point.x, (int)HeightHit.point.z, _grid));
						}
					}
				} else if (size < TmpPath.transform.childCount) {
					for (int i = TmpPath.transform.childCount; i > size; i--) {
						Destroy (TmpPath.transform.GetChild (i - 1).gameObject);
						TmpPath.transform.GetChild (i - 1).parent = null;
					}
				}
			}
			LastDirection = direction;
			if (Input.GetMouseButtonDown (0) && Input.GetKey (KeyCode.LeftShift)) {
				_grid.AddNode (new Node ((int)TmpPath.transform.GetChild (TmpPath.transform.childCount - 1).position.x, (int)TmpPath.transform.GetChild (TmpPath.transform.childCount - 1).position.z, _grid));
				Node Endnode = _grid.Map [(int)TmpPath.transform.GetChild (TmpPath.transform.childCount - 1).position.x, (int)TmpPath.transform.GetChild (TmpPath.transform.childCount - 1).position.z];
				
				if (IntersectNodes.Count > 0) {
					StartNode.ConnectTo (IntersectNodes [0]);
					for (int i = 0; i < IntersectNodes.Count; i++) {
						if (i + 1 == IntersectNodes.Count) {
							IntersectNodes [i].ConnectTo (Endnode);
						} else {
							IntersectNodes [i].ConnectTo (IntersectNodes [i + 1]);
						}
						_grid.AddNode (IntersectNodes [i]);
					}
				} else {
					StartNode.ConnectTo (Endnode);
				}
			
				StartPoint = TmpPath.transform.GetChild (TmpPath.transform.childCount - 1).position;
				List<Transform> tmpl = new List<Transform> ();
				foreach (Transform item in TmpPath.transform) {
					tmpl.Add (item);
				}
				TmpPath.transform.DetachChildren ();
				foreach (Transform item in tmpl) {
					item.parent = transform;
				}
				
				
				_grid.AddNode (new Node ((int)StartPoint.x, (int)StartPoint.z, _grid));
				StartNode = _grid.Map [(int)StartPoint.x, (int)StartPoint.z];
				IntersectNodes.Clear ();
				

			} else if (Input.GetMouseButtonDown (0)) {
			
				_grid.AddNode (new Node ((int)TmpPath.transform.GetChild (TmpPath.transform.childCount - 1).position.x, (int)TmpPath.transform.GetChild (TmpPath.transform.childCount - 1).position.z, _grid));
				Node Endnode = _grid.Map [(int)TmpPath.transform.GetChild (TmpPath.transform.childCount - 1).position.x, (int)TmpPath.transform.GetChild (TmpPath.transform.childCount - 1).position.z];
				
				Debug.Log (IntersectNodes.Count);
				if (IntersectNodes.Count > 0) {
					StartNode.ConnectTo (IntersectNodes [0]);
					for (int i = 0; i < IntersectNodes.Count; i++) {
						if (i + 1 == IntersectNodes.Count) {
							IntersectNodes [i].ConnectTo (Endnode);
						} else {
							IntersectNodes [i].ConnectTo (IntersectNodes [i + 1]);
						}
						_grid.AddNode (IntersectNodes [i]);
					}
				} else {
					StartNode.ConnectTo (Endnode);
				}
			
				List<Transform> tmpl = new List<Transform> ();
				foreach (Transform item in TmpPath.transform) {
					tmpl.Add (item);
				}
				TmpPath.transform.DetachChildren ();
				foreach (Transform item in tmpl) {
					item.parent = transform;
				}
				
				
				
				tmp = false;
			}
			yield return null;
		}
		Destroy (TmpPath);
		_onCreation = false;
	}


}
