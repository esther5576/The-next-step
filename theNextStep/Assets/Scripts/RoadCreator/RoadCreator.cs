using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
		if (Input.GetKeyDown (KeyCode.F2) && !_onCreation) {
			_onCreation = true;
			Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (r, out hit)) {
				Vector3 StPt = hit.point;
				float CellSize = ModuleCreator.Instance.GridCellSize;
				StPt.x = ((StPt.x % CellSize < CellSize / 2f) ? CellSize * ((int)(StPt.x / CellSize)) : CellSize * ((int)(StPt.x / CellSize) + 1)) + 0.5f;
				StPt.z = ((StPt.z % CellSize < CellSize / 2f) ? CellSize * ((int)(StPt.z / CellSize)) : CellSize * ((int)(StPt.z / CellSize) + 1)) + 0.5f;
				StPt.y = 0f;
				StartCoroutine (CreateRoad (StPt));
			}
		}
	}

	IEnumerator CreateRoad (Vector3 StartPoint)
	{
		List<GameObject> Path = new List<GameObject> ();
		while (true) {
			Vector3 EndPoint = Vector3.zero;
			List<GameObject> ActivePath = new List<GameObject> ();
			Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			float CellSize = ModuleCreator.Instance.GridCellSize;
			if (Physics.Raycast (r, out hit)) {
				EndPoint = hit.point;
				EndPoint.x = ((EndPoint.x % CellSize < CellSize / 2f) ? CellSize * ((int)(EndPoint.x / CellSize)) : CellSize * ((int)(EndPoint.x / CellSize) + 1)) + 0.5f;
				EndPoint.z = ((EndPoint.z % CellSize < CellSize / 2f) ? CellSize * ((int)(EndPoint.z / CellSize)) : CellSize * ((int)(EndPoint.z / CellSize) + 1)) + 0.5f;
				EndPoint.y = 0;
			} else {
				EndPoint = StartPoint;
			}

			Vector3 RoadVec = EndPoint - StartPoint;
			RoadVec.x = (Mathf.Abs (RoadVec.x) > Mathf.Abs (RoadVec.z)) ? RoadVec.x : 0;
			RoadVec.z = (Mathf.Abs (RoadVec.z) > Mathf.Abs (RoadVec.x)) ? RoadVec.z : 0;
			RoadVec.y = 0f;


			while (Path.Count < RoadVec.magnitude + 1) {//When we need to add more cube to increase the path
				Path.Add (Instantiate (Road, Vector3.zero, Quaternion.identity) as GameObject);
			}

			//we set the cube that we don't need inactive and we set the other active and we add them to the active path
			for (int i = 0; i < Path.Count; i++) {
				if (i > RoadVec.magnitude)
					Path [i].SetActive (false);
				else {
					if (!Path [i].activeSelf)
						Path [i].SetActive (true);
					ActivePath.Add (Path [i]);
				}
			}

			//we place the cubes along the path
			for (int i = 0; i < ActivePath.Count; i++) {
				if (Physics.Raycast (StartPoint + RoadVec.normalized * i + Vector3.up * 5, Vector3.down, out hit)) {
					if (hit.collider.tag == "Pipeline") {
						ActivePath.Remove (ActivePath [i]);
						ActivePath.Insert (i, null);
						continue;
					}
					ActivePath [i].transform.position = StartPoint + RoadVec.normalized * i;
					ActivePath [i].transform.position += Vector3.up * hit.point.y;
					Vector3 normal = hit.normal;
					if (Mathf.Abs (RoadVec.x) > Mathf.Abs (RoadVec.z)) {
						normal.z = 0;
					} else
						normal.x = 0;
					ActivePath [i].transform.LookAt (ActivePath [i].transform.position + normal);
				}
			}

			if (Input.GetMouseButtonDown (0) && Input.GetKey (KeyCode.LeftShift)) {
				foreach (var item in ActivePath) {
					if (item == null)
						continue;
					item.transform.parent = transform;
					item.layer = 0;
					Path.Remove (item);
				}
				_grid.CreateRoad (StartPoint, ActivePath [ActivePath.Count - 1].transform.position);
				StartPoint = ActivePath [ActivePath.Count - 1].transform.position;
				StartPoint.y = 0;
			} else if (Input.GetMouseButtonDown (0)) {

				foreach (var item in ActivePath.ToArray()) {
					if (item == null) {
						ActivePath.Remove (item);
						continue;
					}
					item.transform.parent = transform;
					item.layer = 0;
				}
				_grid.CreateRoad (StartPoint, ActivePath.Last ().transform.position);
				break;
			}
			yield return null;
		}
		_onCreation = false;
	}

}
