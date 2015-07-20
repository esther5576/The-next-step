using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public List<Edge>[,] _edgemap;
	private int _sizeX, _sizeY;
	// Use this for initialization
	void Start ()
	{
		_sizeX = (int)(GetComponent<Terrain> ().terrainData.size.x / ModuleCreator.Instance.GridCellSize);
		_sizeY = (int)(GetComponent<Terrain> ().terrainData.size.z / ModuleCreator.Instance.GridCellSize);
		_map = new Node[_sizeX, _sizeY];
		_edgemap = new List<Edge>[_sizeX, _sizeY];
		for (int i = 0; i < _sizeX; i++) {
			for (int j = 0; j < _sizeY; j++) {
				_edgemap [i, j] = new List<Edge> ();
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void RemoveEdge (Edge target)
	{
		foreach (var item in _edgemap) {
			item.Remove (target);
		}
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
	
	public void IntersectionConnection (Node intersection)
	{
		
	}
	
	public void CreateRoad (Vector3 Start, Vector3 End)
	{
		//We check if Start is not already a node on the grid
		if (_map [(int)Start.x, (int)Start.z] != null) {
			//we check if End is not already a node on the grid
			if (_map [(int)End.x, (int)End.z] != null) {
				//TODO : Start and End are already nodes on the grid
				List<Node> PathNodes = new List<Node> ();
				PathNodes.Add (_map [(int)Start.x, (int)Start.z]);
				Vector3 direction = End - Start;
				direction.Normalize ();
				Debug.Log ("OPTION 1");
				if (Mathf.Abs (direction.x) > 0) {
					Debug.Log ("/t X direction");
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).z) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if (direction.x > 0) {
						for (int i = (int)Start.x; i < End.x; i++) {
							Node ChNode = _map [i, (int)Start.z];
							if (ChNode == null)
								continue;
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.x; i > End.x; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				} else {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).x) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if ((End - Start).z > 0) {
						for (int i = (int)Start.z; i > End.z; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.z; i > End.z; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				}
				PathNodes.Add (_map [(int)End.x, (int)End.z]);
				for (int i = 0; i < PathNodes.Count-1; i++) {
					PathNodes [i].ConnectTo (PathNodes [i + 1]);
				}
				return;
			}
			//We check if End is on an edge
			if (_edgemap [(int)End.x, (int)End.z].Count > 0) {
				//TODO : Start is already a node on the grid and End is on a edge
				List<Node> PathNodes = new List<Node> ();
				PathNodes.Add (_map [(int)Start.x, (int)Start.z]);
				Vector3 direction = End - Start;
				direction.Normalize ();
				Node LastNode = new Node ((int)End.x, (int)End.z, this);
				Node EndNode = LastNode;
				
				if (Mathf.Abs (direction.x) > 0) {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).z) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if (direction.x > 0) {
						for (int i = (int)Start.x; i < End.x; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
								else if (i + 1 > End.x)
									LastNode = neihbour;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.x; i > End.x; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
								else if (i + 1 > End.x)
									LastNode = neihbour;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				} else {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).x) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if ((End - Start).z > 0) {
						for (int i = (int)Start.z; i > End.z; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
								else if (i + 1 > End.x)
									LastNode = neihbour;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.z; i > End.z; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
								else if (i + 1 > End.x)
									LastNode = neihbour;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				}
				if (LastNode == EndNode) {
					_edgemap [(int)End.x, (int)End.z] [0].A.ConnectTo (LastNode);
					_edgemap [(int)End.x, (int)End.z] [0].B.ConnectTo (LastNode);
				}
				PathNodes.Add (LastNode);
				for (int i = 0; i < PathNodes.Count-1; i++) {
					PathNodes [i].ConnectTo (PathNodes [i + 1]);
				}
				return;
			} 
			//
			else {
				List<Node> PathNodes = new List<Node> ();
				Node LastNode = new Node ((int)End.x, (int)End.z, this);
				AddNode (LastNode);
				PathNodes.Add (_map [(int)Start.x, (int)Start.z]);
				Vector3 direction = End - Start;
				direction.Normalize ();
				
				if (Mathf.Abs (direction.x) > 0) {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).z) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if (direction.x > 0) {
						for (int i = (int)Start.x; i < End.x; i++) {
							Node ChNode = _map [i, (int)Start.z];
							if (ChNode == null)
								continue;
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.x; i > End.x; i--) {
							Node ChNode = _map [i, (int)Start.z];
							if (ChNode == null)
								continue;
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				} else {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).x) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if ((End - Start).z > 0) {
						for (int i = (int)Start.z; i > End.z; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.z; i > End.z; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				}
				for (int i = 0; i < PathNodes.Count-1; i++) {
					PathNodes [i].ConnectTo (PathNodes [i + 1]);
				}
			}
		}
		//We check if Start is on an edge
		if (_edgemap [(int)Start.x, (int)Start.z].Count > 0) {
			//We check if End is not already a node on the grid
			if (_map [(int)End.x, (int)End.z] != null) {
				List<Node> PathNodes = new List<Node> ();
				PathNodes.Add (_map [(int)Start.x, (int)Start.z]);
				Vector3 direction = End - Start;
				direction.Normalize ();
				Node LastNode = new Node ((int)End.x, (int)End.z, this);
				if (_edgemap [(int)Start.x, (int)Start.z] [0].A.X != _edgemap [(int)Start.x, (int)Start.z] [0].B.X) {
					_edgemap [(int)Start.x, (int)Start.z] [0].A.DisconnectFrom (_edgemap [(int)Start.x, (int)Start.z] [0].B);
					PathNodes [0].ConnectTo (_edgemap [(int)Start.x, (int)Start.z] [0].A);
					PathNodes [0].ConnectTo (_edgemap [(int)Start.x, (int)Start.z] [0].B);
				} else {
					if (direction.x > 0) {
						if (_edgemap [(int)Start.x, (int)Start.z] [0].A.X > _edgemap [(int)Start.x, (int)Start.z] [0].B.X) {
							PathNodes [0] = _edgemap [(int)Start.x, (int)Start.z] [0].B;
							_edgemap [(int)Start.x, (int)Start.z] [0].A.DisconnectFromEveryNodes ();
							RemoveNode (_edgemap [(int)Start.x, (int)Start.z] [0].A);
						} else {
							PathNodes [0] = _edgemap [(int)Start.x, (int)Start.z] [0].A;
							_edgemap [(int)Start.x, (int)Start.z] [0].B.DisconnectFromEveryNodes ();
							RemoveNode (_edgemap [(int)Start.x, (int)Start.z] [0].B);
						}
					} else {
						if (_edgemap [(int)Start.x, (int)Start.z] [0].A.X > _edgemap [(int)Start.x, (int)Start.z] [0].B.X) {
							PathNodes [0] = _edgemap [(int)Start.x, (int)Start.z] [0].A;
							_edgemap [(int)Start.x, (int)Start.z] [0].B.DisconnectFromEveryNodes ();
							RemoveNode (_edgemap [(int)Start.x, (int)Start.z] [0].B);
						} else {
							PathNodes [0] = _edgemap [(int)Start.x, (int)Start.z] [0].B;
							_edgemap [(int)Start.x, (int)Start.z] [0].A.DisconnectFromEveryNodes ();
							RemoveNode (_edgemap [(int)Start.x, (int)Start.z] [0].A);
						}
					}
				}
				
				if (Mathf.Abs (direction.x) > 0) {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).z) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if (direction.x > 0) {
						for (int i = (int)Start.x; i < End.x; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.x; i > End.x; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				} else {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).x) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if ((End - Start).z > 0) {
						for (int i = (int)Start.z; i > End.z; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.z; i > End.z; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				}
				//PathNodes.Add (LastNode);
				for (int i = 0; i < PathNodes.Count-1; i++) {
					PathNodes [i].ConnectTo (PathNodes [i + 1]);
				}
				return;
			}
			//We check if End is on an edge
			if (_edgemap [(int)End.x, (int)End.z].Count > 0) {
				List<Node> PathNodes = new List<Node> ();
				PathNodes.Add (_map [(int)Start.x, (int)Start.z]);
				Vector3 direction = End - Start;
				direction.Normalize ();
				Node LastNode = new Node ((int)End.x, (int)End.z, this);
				Node EndNode = LastNode;
				
				if (_edgemap [(int)Start.x, (int)Start.z] [0].A.X != _edgemap [(int)Start.x, (int)Start.z] [0].B.X) {
					_edgemap [(int)Start.x, (int)Start.z] [0].A.DisconnectFrom (_edgemap [(int)Start.x, (int)Start.z] [0].B);
					PathNodes [0].ConnectTo (_edgemap [(int)Start.x, (int)Start.z] [0].A);
					PathNodes [0].ConnectTo (_edgemap [(int)Start.x, (int)Start.z] [0].B);
				} else {
					if (direction.x > 0) {
						if (_edgemap [(int)Start.x, (int)Start.z] [0].A.X > _edgemap [(int)Start.x, (int)Start.z] [0].B.X) {
							PathNodes [0] = _edgemap [(int)Start.x, (int)Start.z] [0].B;
							_edgemap [(int)Start.x, (int)Start.z] [0].A.DisconnectFromEveryNodes ();
							RemoveNode (_edgemap [(int)Start.x, (int)Start.z] [0].A);
						} else {
							PathNodes [0] = _edgemap [(int)Start.x, (int)Start.z] [0].A;
							_edgemap [(int)Start.x, (int)Start.z] [0].B.DisconnectFromEveryNodes ();
							RemoveNode (_edgemap [(int)Start.x, (int)Start.z] [0].B);
						}
					} else {
						if (_edgemap [(int)Start.x, (int)Start.z] [0].A.X > _edgemap [(int)Start.x, (int)Start.z] [0].B.X) {
							PathNodes [0] = _edgemap [(int)Start.x, (int)Start.z] [0].A;
							_edgemap [(int)Start.x, (int)Start.z] [0].B.DisconnectFromEveryNodes ();
							RemoveNode (_edgemap [(int)Start.x, (int)Start.z] [0].B);
						} else {
							PathNodes [0] = _edgemap [(int)Start.x, (int)Start.z] [0].B;
							_edgemap [(int)Start.x, (int)Start.z] [0].A.DisconnectFromEveryNodes ();
							RemoveNode (_edgemap [(int)Start.x, (int)Start.z] [0].A);
						}
					}
				}
				
				if (Mathf.Abs (direction.x) > 0) {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).z) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if (direction.x > 0) {
						for (int i = (int)Start.x; i < End.x; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
								else if (i + 1 > End.x)
									LastNode = neihbour;
									
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.x; i > End.x; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
								else if (i + 1 > End.x)
									LastNode = neihbour;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				} else {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).x) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if ((End - Start).z > 0) {
						for (int i = (int)Start.z; i > End.z; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
								else if (i + 1 > End.x)
									LastNode = neihbour;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.z; i > End.z; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
								else if (i + 1 > End.x)
									LastNode = neihbour;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				}
				if (LastNode == EndNode) {
					_edgemap [(int)End.x, (int)End.z] [0].A.ConnectTo (LastNode);
					_edgemap [(int)End.x, (int)End.z] [0].B.ConnectTo (LastNode);
				}
				PathNodes.Add (LastNode);
				for (int i = 0; i < PathNodes.Count-1; i++) {
					PathNodes [i].ConnectTo (PathNodes [i + 1]);
				}
				return;
			} 
			//
			else {
				List<Node> PathNodes = new List<Node> ();
				Node LastNode = new Node ((int)End.x, (int)End.z, this);
				AddNode (LastNode);
				PathNodes.Add (_map [(int)Start.x, (int)Start.z]);
				Vector3 direction = End - Start;
				direction.Normalize ();
				if (_edgemap [(int)Start.x, (int)Start.z] [0].A.X != _edgemap [(int)Start.x, (int)Start.z] [0].B.X) {
					_edgemap [(int)Start.x, (int)Start.z] [0].A.DisconnectFrom (_edgemap [(int)Start.x, (int)Start.z] [0].B);
					PathNodes [0].ConnectTo (_edgemap [(int)Start.x, (int)Start.z] [0].A);
					PathNodes [0].ConnectTo (_edgemap [(int)Start.x, (int)Start.z] [0].B);
				} else {
					if (direction.x > 0) {
						if (_edgemap [(int)Start.x, (int)Start.z] [0].A.X > _edgemap [(int)Start.x, (int)Start.z] [0].B.X) {
							PathNodes [0] = _edgemap [(int)Start.x, (int)Start.z] [0].B;
							_edgemap [(int)Start.x, (int)Start.z] [0].A.DisconnectFromEveryNodes ();
							RemoveNode (_edgemap [(int)Start.x, (int)Start.z] [0].A);
						} else {
							PathNodes [0] = _edgemap [(int)Start.x, (int)Start.z] [0].A;
							_edgemap [(int)Start.x, (int)Start.z] [0].B.DisconnectFromEveryNodes ();
							RemoveNode (_edgemap [(int)Start.x, (int)Start.z] [0].B);
						}
					} else {
						if (_edgemap [(int)Start.x, (int)Start.z] [0].A.X > _edgemap [(int)Start.x, (int)Start.z] [0].B.X) {
							PathNodes [0] = _edgemap [(int)Start.x, (int)Start.z] [0].A;
							_edgemap [(int)Start.x, (int)Start.z] [0].B.DisconnectFromEveryNodes ();
							RemoveNode (_edgemap [(int)Start.x, (int)Start.z] [0].B);
						} else {
							PathNodes [0] = _edgemap [(int)Start.x, (int)Start.z] [0].B;
							_edgemap [(int)Start.x, (int)Start.z] [0].A.DisconnectFromEveryNodes ();
							RemoveNode (_edgemap [(int)Start.x, (int)Start.z] [0].A);
						}
					}
				}
				
				if (Mathf.Abs (direction.x) > 0) {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).z) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if (direction.x > 0) {
						for (int i = (int)Start.x; i < End.x; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.x; i > End.x; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				} else {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).x) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if ((End - Start).z > 0) {
						for (int i = (int)Start.z; i > End.z; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.z; i > End.z; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				}
				//PathNodes.Add (LastNode);
				for (int i = 0; i < PathNodes.Count-1; i++) {
					PathNodes [i].ConnectTo (PathNodes [i + 1]);
				}
				return;
			}
		}
		//Start isn't already a node and isn't on an edge
		else {
			//We check if End is not already a node on the grid
			if (_map [(int)End.x, (int)End.z] != null) {
				//TODO : End is already a node on the grid	
				List<Node> PathNodes = new List<Node> ();
				Node StartNode = new Node ((int)Start.x, (int)Start.z, this);
				AddNode (StartNode);
				PathNodes.Add (StartNode);
				Vector3 direction = End - Start;
				direction.Normalize ();
				
				if (Mathf.Abs (direction.x) > 0) {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).z) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if (direction.x > 0) {
						for (int i = (int)Start.x; i < End.x; i++) {
							Node ChNode = _map [i, (int)Start.z];
							if (ChNode == null)
								continue;
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.x; i > End.x; i--) {
							Node ChNode = _map [i, (int)Start.z];
							if (ChNode == null)
								continue;
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				} else {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).x) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if ((End - Start).z > 0) {
						for (int i = (int)Start.z; i > End.z; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.z; i > End.z; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				}
				for (int i = 0; i < PathNodes.Count-1; i++) {
					PathNodes [i].ConnectTo (PathNodes [i + 1]);
				}
				return;
			}
			//We check if End is on an edge
			if (_edgemap [(int)End.x, (int)End.z].Count > 0) {
				//TODO : End is on a edge
				List<Node> PathNodes = new List<Node> ();
				Node StartNode = new Node ((int)Start.x, (int)Start.z, this);
				AddNode (StartNode);
				PathNodes.Add (StartNode);
				Vector3 direction = End - Start;
				direction.Normalize ();
				Node LastNode = new Node ((int)End.x, (int)End.z, this);
				Node EndNode = LastNode;
				
				if (Mathf.Abs (direction.x) > 0) {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).z) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if (direction.x > 0) {
						for (int i = (int)Start.x; i < End.x; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
								else if (i + 1 > End.x)
									LastNode = neihbour;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.x; i > End.x; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
								else if (i + 1 > End.x)
									LastNode = neihbour;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				} else {
					Node tmp = PathNodes [0];
					bool _CorrectStart = false;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).x) > 0)
							_CorrectStart = true;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if ((End - Start).z > 0) {
						for (int i = (int)Start.z; i > End.z; i++) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
								else if (i + 1 > End.x)
									LastNode = neihbour;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.z; i > End.z; i--) {
							Node ChNode = _map [i, (int)Start.z];
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
								else if (i + 1 > End.x)
									LastNode = neihbour;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				}
				if (LastNode == EndNode) {
					_edgemap [(int)End.x, (int)End.z] [0].A.ConnectTo (LastNode);
					_edgemap [(int)End.x, (int)End.z] [0].B.ConnectTo (LastNode);
				}
				PathNodes.Add (LastNode);
				for (int i = 0; i < PathNodes.Count-1; i++) {
					PathNodes [i].ConnectTo (PathNodes [i + 1]);
				}
				return;
			} 
			//
			else {
				List<Node> PathNodes = new List<Node> ();
				Node StartNode = new Node ((int)Start.x, (int)Start.z, this);
				Node LastNode = new Node ((int)End.x, (int)End.z, this);
				AddNode (StartNode);
				AddNode (LastNode);
				PathNodes.Add (StartNode);
				Vector3 direction = End - Start;
				direction.Normalize ();
				
				if (Mathf.Abs (direction.x) > 0) {
					Node tmp = PathNodes [0];
					bool _CorrectStart = true;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).z) > 0)
							_CorrectStart = false;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if (direction.x > 0) {
						for (int i = (int)Start.x+1; i < End.x; i++) {
							Node ChNode = _map [i, (int)Start.z];
							if (ChNode == null)
								continue;
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = (int)Start.x+1; i > End.x; i--) {
							Node ChNode = _map [i, (int)Start.z];
							if (ChNode == null)
								continue;
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.Y != PathNodes [0].Y)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				} else {
					Node tmp = PathNodes [0];
					bool _CorrectStart = true;
					foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
						Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
						if ((NeihbourPos - Start).normalized == direction * -1f)
							tmp = neihbour;
						if (Mathf.Abs ((NeihbourPos - Start).x) > 0)
							_CorrectStart = false;
					}
					if (!_CorrectStart) {
						PathNodes [0].DisconnectFromEveryNodes ();
						PathNodes [0] = tmp;
						_map [(int)Start.x, (int)Start.z] = null;
					}
					if ((End - Start).z > 0) {
						for (int i = (int)Start.z+1; i < End.z; i++) {
							Node ChNode = _map [(int)Start.x, i];
							if (ChNode == null) {
								//Debug.Log (_edgemap [(int)Start.x, i].Count.ToString () + " " + i.ToString () + " " + (int)Start.x);
								if (_edgemap [(int)Start.x, i].Count == 0)
									continue;
								ChNode = new Node ((int)Start.x, i, this);
								Edge CollapseEdge = _edgemap [(int)Start.x, i] [0];
								CollapseEdge.A.DisconnectFrom (CollapseEdge.B);
								ChNode.ConnectTo (CollapseEdge.A);
								ChNode.ConnectTo (CollapseEdge.B);
								AddNode (ChNode);
							}
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
							}
							if (!KeepNode) {
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					} else {
						for (int i = ((int)Start.z)-1; i > End.z; i--) {
							Node ChNode = _map [(int)Start.x, i];
							if (ChNode == null) {
								if (_edgemap [(int)Start.x, i].Count == 0)
									continue;
								ChNode = new Node ((int)Start.x, i, this);
								Edge CollapseEdge = _edgemap [(int)Start.x, i] [0];
								CollapseEdge.A.DisconnectFrom (CollapseEdge.B);
								ChNode.ConnectTo (CollapseEdge.A);
								ChNode.ConnectTo (CollapseEdge.B);
								AddNode (ChNode);
							}
							bool KeepNode = false;
							foreach (var neihbour in ChNode.ConnectedNodes) {
								if (neihbour.X != PathNodes [0].X)
									KeepNode = true;
							}
							if (!KeepNode) {
								Debug.Log (i);
								ChNode.DisconnectFromEveryNodes ();
								RemoveNode (ChNode);
							} else {
								PathNodes.Add (ChNode);
							}
						}
					}
				}
				
				PathNodes.Add (LastNode);
				for (int i = 0; i < PathNodes.Count-1; i++) {
					PathNodes [i].ConnectTo (PathNodes [i + 1]);
				}
				/*foreach (var item in _edgemap) {
					if (item.Count == 1)
						Debug.Log (item [0].A.X.ToString () + " | " + item [0].A.Y.ToString () + " ||| " + item [0].B.X.ToString () + " | " + item [0].B.Y.ToString ());
				}*/
				return;
			}
		}
		
	}
	
	void OnDrawGizmos ()
	{
		Gizmos.color = Color.green;
		Gizmos.matrix = transform.localToWorldMatrix;
		//Gizmos.DrawSphere (new Vector3 (77, 0, 8), 0.5f)
		if (_map != null) {
			foreach (Node item in _map) {
				if (item == null)
					continue;
				Vector3 pos = new Vector3 (item.X, 5f, item.Y);
				
				Gizmos.DrawSphere (pos, 0.5f);
				
			}
			
			Gizmos.color = Color.blue;
			float height = 0f;
			foreach (var item in _map) {
				if (item == null)
					continue;
				Vector3 posA = new Vector3 (item.X, 5f + height, item.Y);
				foreach (var CoN in item.ConnectedNodes) {
					Vector3 posB = new Vector3 (CoN.X, 5f + height, CoN.Y);
					Gizmos.DrawLine (posA, posB);
				}
				Debug.Log (item.X.ToString () + " | " + item.Y.ToString () + " | " + height.ToString () + " | " + item.ConnectedNodes.Count.ToString ());
				height += 0.5f;
			}
		}
	}
}
