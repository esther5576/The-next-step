using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
	public Node[,] Map {
		get {
			return _map;
		}
	}
	public int SixeX {
		get {
			return _sizeX;
		}
	}
	public int SixeY {
		get {
			return _sizeY;
		}
	}

	private Node[,] _map;
	private int _sizeX, _sizeY;
	// Use this for initialization
	void Start ()
	{
		_sizeX = (int)(GetComponent<Terrain> ().terrainData.size.x / ModuleCreator.Instance.GridCellSize);
		_sizeY = (int)(GetComponent<Terrain> ().terrainData.size.z / ModuleCreator.Instance.GridCellSize);
		_map = new Node[_sizeX, _sizeY];
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public bool AddNode (Node node)
	{
		if (node.X < 0 || node.Y < 0 || node.X >= _sizeX || node.Y >= _sizeY)
			return false;
		if (_map [node.X, node.Y] == null) {
			_map [node.X, node.Y] = node;
			return true;
		} else {
			Debug.LogWarning ("Already a node there!");
			return false;
		}
	}
	
	public bool RemoveNode (Node node)
	{
		if (node.X < 0 || node.Y < 0 || node.X >= _sizeX || node.Y >= _sizeY)
			return false;
		if (_map [node.X, node.Y] == null) {
			Debug.LogError ("The node isn't in the grid");
			return false;
		} else {
			_map [node.X, node.Y] = null;
			return true;
		}
	}
	
	void OnDrawGizmos ()
	{
		Gizmos.color = Color.green;
		Gizmos.matrix = transform.localToWorldMatrix;
		//Gizmos.DrawSphere (new Vector3 (77, 0, 8), 0.5f);
		if (_map != null) {
			foreach (Node item in _map) {
				if (item == null)
					continue;
				Vector3 pos = new Vector3 (item.X, 5f, item.Y);
				Gizmos.DrawSphere (pos, 0.5f);
			}
			Gizmos.color = Color.blue;
			foreach (var item in _map) {
				if (item == null)
					continue;
				Vector3 posA = new Vector3 (item.X, 5f, item.Y);
				foreach (var CoN in item.ConnectedNodes) {
					Vector3 posB = new Vector3 (CoN.X, 5f, CoN.Y);
					Gizmos.DrawLine (posA, posB);
				}	
			}
		}
	}
}
