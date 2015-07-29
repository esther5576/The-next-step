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
	public Node MainBuildingNodes {
		get {
			return MainBuilding.GetComponent<ProductionAndNeeds> ()._associateNode;
		}
	}

	private Node[,] _map;
	public List<Edge>[,] _edgemap;
	private int _sizeX, _sizeY;

	public GameObject MainBuilding;
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
		MainBuilding.GetComponent<ProductionAndNeeds> ()._associateNode = AddBuilding (MainBuilding.transform.position, MainBuilding.GetComponent<Collider> ().bounds.size);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public Node AddBuilding (Vector3 position, Vector3 size)
	{
		Vector3 A, B, C, D;
		A = new Vector3 ((position.x - (size.x / 2f)) + 0.5f, 0, (position.z + (size.z / 2f)) - 0.5f);
		B = new Vector3 ((position.x + (size.x / 2f)) - 0.5f, 0, (position.z + (size.z / 2f)) - 0.5f);
		C = new Vector3 ((position.x + (size.x / 2f)) - 0.5f, 0, (position.z - (size.z / 2f)) + 0.5f);
		D = new Vector3 ((position.x - (size.x / 2f)) + 0.5f, 0, (position.z - (size.z / 2f)) + 0.5f);

		/*Debug.Log (A);
		Debug.Log (B);
		Debug.Log (C);
		Debug.Log (D);*/
		

		CreateRoad (A, B);
		CreateRoad (B, C);
		CreateRoad (C, D);
		CreateRoad (D, A);


		return _map [(int)A.x, (int)A.z];
	}
	
	public void RemoveEdge (Edge target)
	{
		foreach (var item in _edgemap) {
			item.Remove (target);
			//if (item.Remove (target))
			//Debug.Log ("RemoveSucces");
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

	public void CreateRoadFrom2Nodes (Vector3 Start, Vector3 End)
	{
		Vector3 direction = (End - Start).normalized;
		if (direction.x < 0 || direction.z < 0) {
			Vector3 tmpV = Start;
			Start = End;
			End = tmpV;
			direction = (End - Start).normalized;
		}
		List<Node> PathNodes = new List<Node> ();
		PathNodes.Add (_map [(int)Start.x, (int)Start.z]);
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
			if (!_CorrectStart && PathNodes [0] != tmp) {
				PathNodes [0].DisconnectFromEveryNodes ();
				PathNodes [0] = tmp;
				_map [(int)Start.x, (int)Start.z] = null;
			}
			for (int i = (int)Start.x+1; i < End.x+1; i++) {
				Node ChNode = _map [i, (int)Start.z];
				if (ChNode == null) {
					//Debug.Log (_edgemap [i, (int)Start.z].Count.ToString () + " " + i.ToString () + " " + (int)Start.z);
					if (_edgemap [i, (int)Start.z].Count == 0 && _map [i, (int)Start.z + 1] == null && _map [i, (int)Start.z - 1] == null)
						continue;
					if (_edgemap [i, (int)Start.z].Count > 0) {
						ChNode = new Node (i, (int)Start.z, this);
						Edge CollapseEdge = _edgemap [i, (int)Start.z] [0];
						CollapseEdge.A.DisconnectFrom (CollapseEdge.B);
						ChNode.ConnectTo (CollapseEdge.A);
						ChNode.ConnectTo (CollapseEdge.B);
						AddNode (ChNode);
					} else if (_map [i, (int)Start.z + 1] != null || _map [i, (int)Start.z - 1] != null) {
						ChNode = new Node (i, (int)Start.z, this);
						AddNode (ChNode);
						if (_map [i, (int)Start.z + 1] != null) {
							Node neihbourg = _map [i, (int)Start.z + 1];
							bool keepNode = false;
							foreach (Node n in neihbourg.ConnectedNodes) {
								if (Vector3.Angle (neihbourg.DirectionTo (n), ChNode.DirectionTo (neihbourg)) != 0)
									keepNode = true;
							}
							if (!keepNode) {
								ChNode.ConnectTo (neihbourg.ConnectedNodes);
								neihbourg.DisconnectFromEveryNodes ();
								RemoveNode (neihbourg);
							} else {
								ChNode.ConnectTo (neihbourg);
							}
						}
						if (_map [i, (int)Start.z - 1] != null) {
							Node neihbourg = _map [i, (int)Start.z - 1];
							bool keepNode = false;
							foreach (Node n in neihbourg.ConnectedNodes) {
								if (Vector3.Angle (neihbourg.DirectionTo (n), ChNode.DirectionTo (neihbourg)) != 0)
									keepNode = true;
							}
							if (!keepNode) {
								ChNode.ConnectTo (neihbourg.ConnectedNodes);
								neihbourg.DisconnectFromEveryNodes ();
								RemoveNode (neihbourg);
							} else {
								ChNode.ConnectTo (neihbourg);
							}
						}
					}
				}
				bool KeepNode = false;
				foreach (var neihbour in ChNode.ConnectedNodes) {
					if (neihbour.Y != PathNodes [0].Y)
						KeepNode = true;
					else if (neihbour.X > End.x + 1)
						PathNodes.Add (neihbour);
				}
				if (ChNode.ConnectedNodes.Count == 0 && i + 1 >= End.x + 1) {
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
			Node tmp = PathNodes [0];
			bool _CorrectStart = false;
			foreach (Node neihbour in PathNodes[0].ConnectedNodes) {
				Vector3 NeihbourPos = new Vector3 (neihbour.X, 0, neihbour.Y);
				if ((NeihbourPos - Start).normalized == direction * -1f)
					tmp = neihbour;
				if (Mathf.Abs ((NeihbourPos - Start).x) > 0)
					_CorrectStart = true;
			}
			if (!_CorrectStart && PathNodes [0] != tmp) {
				PathNodes [0].DisconnectFromEveryNodes ();
				PathNodes [0] = tmp;
				_map [(int)Start.x, (int)Start.z] = null;
			}
			for (int i = (int)Start.z+1; i < End.z+1; i++) {
				Node ChNode = _map [(int)Start.x, i];
				if (ChNode == null) {
					//Debug.Log (_edgemap [(int)Start.x, i].Count.ToString () + " " + i.ToString () + " " + (int)Start.x);
					if (_edgemap [(int)Start.x, i].Count == 0 && _map [(int)Start.x + 1, i] == null && _map [(int)Start.x - 1, i] == null)
						continue;
					if (_edgemap [(int)Start.x, i].Count > 0) {
						ChNode = new Node ((int)Start.x, i, this);
						Edge CollapseEdge = _edgemap [(int)Start.x, i] [0];
						CollapseEdge.A.DisconnectFrom (CollapseEdge.B);
						ChNode.ConnectTo (CollapseEdge.A);
						ChNode.ConnectTo (CollapseEdge.B);
						AddNode (ChNode);
					} else if (_map [(int)Start.x + 1, i] != null || _map [(int)Start.x - 1, i] != null) {
						ChNode = new Node ((int)Start.x, i, this);
						AddNode (ChNode);
						if (_map [(int)Start.x + 1, i] != null) {
							Node neihbourg = _map [(int)Start.x + 1, i];
							bool keepNode = false;
							foreach (Node n in neihbourg.ConnectedNodes) {
								if (Vector3.Angle (neihbourg.DirectionTo (n), ChNode.DirectionTo (neihbourg)) != 0)
									keepNode = true;
							}
							if (!keepNode) {
								ChNode.ConnectTo (neihbourg.ConnectedNodes);
								neihbourg.DisconnectFromEveryNodes ();
								RemoveNode (neihbourg);
							} else {
								ChNode.ConnectTo (neihbourg);
							}
						}
						if (_map [(int)Start.x - 1, i] != null) {
							Node neihbourg = _map [(int)Start.x - 1, i];
							bool keepNode = false;
							foreach (Node n in neihbourg.ConnectedNodes) {
								if (Vector3.Angle (neihbourg.DirectionTo (n), ChNode.DirectionTo (neihbourg)) != 0)
									keepNode = true;
							}
							if (!keepNode) {
								ChNode.ConnectTo (neihbourg.ConnectedNodes);
								neihbourg.DisconnectFromEveryNodes ();
								RemoveNode (neihbourg);
							} else {
								ChNode.ConnectTo (neihbourg);
							}
						}
					}
				}
				bool KeepNode = false;
				foreach (var neihbour in ChNode.ConnectedNodes) {
					if (neihbour.X != PathNodes [0].X)
						KeepNode = true;
					else if (neihbour.Y > End.z + 1)
						PathNodes.Add (neihbour);
				}
				if (ChNode.ConnectedNodes.Count == 0 && i + 1 >= End.z + 1) {
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
		
		for (int i = 0; i < PathNodes.Count-1; i++) {
			PathNodes [i].ConnectTo (PathNodes [i + 1]);
		}
		return;
	}
	
	public void CreateRoad (Vector3 Start, Vector3 End)
	{
		Start.y = 0;
		Start.x -= 0.5f;
		Start.z -= 0.5f;
		End.y = 0;
		End.x -= 0.5f;
		End.z -= 0.5f;

		//We check if Start is not already a node on the grid
		if (_map [(int)Start.x, (int)Start.z] != null) {
			//we check if End is not already a node on the grid
			if (_map [(int)End.x, (int)End.z] != null) {
				//TODO : Start and End are already nodes on the grid
				CreateRoadFrom2Nodes (Start, End);
				return;
			}
			//We check if End is on an edge
			if (_edgemap [(int)End.x, (int)End.z].Count > 0) {
				//TODO : Start is already a node on the grid and End is on a edge
				Vector3 direction = End - Start;
				direction.Normalize ();
				if (_edgemap [(int)End.x, (int)End.z] [0].getDirection () == (direction.z == 0)) {
					Debug.Log ("Orthogonal");
					Node EndNode = new Node ((int)End.x, (int)End.z, this);
					EndNode.ConnectTo (_edgemap [(int)End.x, (int)End.z] [0].A);
					EndNode.ConnectTo (_edgemap [(int)End.x, (int)End.z] [0].B);
					_edgemap [(int)End.x, (int)End.z] [0].A.DisconnectFrom (_edgemap [(int)End.x, (int)End.z] [0].B);
					AddNode (EndNode);
					End.x = EndNode.X;
					End.z = EndNode.Y;
				} else {
					if (_edgemap [(int)End.x, (int)End.z] [0].A.DistanceTo (Start) > _edgemap [(int)End.x, (int)End.z] [0].B.DistanceTo (Start)) {
						End.x = _edgemap [(int)End.x, (int)End.z] [0].A.X;
						End.z = _edgemap [(int)End.x, (int)End.z] [0].A.Y;
					} else {
						End.x = _edgemap [(int)End.x, (int)End.z] [0].B.X;
						End.z = _edgemap [(int)End.x, (int)End.z] [0].B.Y;
					}
				}
				CreateRoadFrom2Nodes (Start, End);
				return;
			} 
			//
			else {
				Node LastNode = new Node ((int)End.x, (int)End.z, this);
				AddNode (LastNode);
				Vector3 direction = (End - Start).normalized;

				bool keepOriginal = false;
				bool CheckEdge = true;
				if (_map [(int)End.x + (int)direction.z, (int)End.z + (int)direction.x] != null) {
					Debug.LogWarning ("Direction side 1");
					Node neihbourg = _map [(int)End.x + (int)direction.z, (int)End.z + (int)direction.x];
					bool keepNode = false;
					foreach (Node n in neihbourg.ConnectedNodes) {
						if (Vector3.Angle (neihbourg.DirectionTo (n), LastNode.DirectionTo (neihbourg)) != 0)
							keepNode = true;
					}
					if (!keepNode) {
						LastNode.ConnectTo (neihbourg.ConnectedNodes [0]);
						neihbourg.DisconnectFromEveryNodes ();
						RemoveNode (neihbourg);
					} else {
						LastNode.ConnectTo (neihbourg);
					}
					keepOriginal = true;
				}	
				if (_map [(int)End.x - (int)direction.z, (int)End.z - (int)direction.x] != null) {
					Node neihbourg = _map [(int)End.x - (int)direction.z, (int)End.z - (int)direction.x];
					Debug.LogWarning ("Direction side 2");
					bool keepNode = false;
					foreach (Node n in neihbourg.ConnectedNodes) {
						if (Vector3.Angle (neihbourg.DirectionTo (n), LastNode.DirectionTo (neihbourg)) != 0)
							keepNode = true;
					}
					if (!keepNode) {
						LastNode.ConnectTo (neihbourg.ConnectedNodes [0]);
						neihbourg.DisconnectFromEveryNodes ();
						RemoveNode (neihbourg);
					} else {
						LastNode.ConnectTo (neihbourg);
					}
					keepOriginal = true;
				}
				if (_map [(int)End.x + (int)direction.x, (int)End.z + (int)direction.z] != null) {
					Node neihbourg = _map [(int)End.x + (int)direction.x, (int)End.z + (int)direction.z];
					Debug.LogWarning ("Direction - 1");
					if (!keepOriginal) {
						RemoveNode (LastNode);
						End.x = neihbourg.X;
						End.z = neihbourg.Y;
						CheckEdge = false;
					} else {
						LastNode.ConnectTo (neihbourg);
					}
				}
				//We Check for neighbourg edges to connect to
				if (CheckEdge) {
					if (_edgemap [(int)End.x + (int)direction.z, (int)End.z + (int)direction.x].Count > 0) {
						foreach (var item in _edgemap[(int)End.x + (int)direction.z, (int)End.z + (int)direction.x].ToArray()) {
							if (!item.A.IsConnectedTo (LastNode) && !item.B.IsConnectedTo (LastNode)) {
								Node IntersectionNode = new Node ((int)End.x + (int)direction.z, (int)End.z + (int)direction.x, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								LastNode.ConnectTo (IntersectionNode);
							}
						}
					}	
					if (_edgemap [(int)End.x - (int)direction.z, (int)End.z - (int)direction.x].Count > 0) {
						foreach (var item in _edgemap[(int)End.x - (int)direction.z, (int)End.z - (int)direction.x].ToArray()) {
							Debug.Log ((!item.A.IsConnectedTo (LastNode)).ToString () + " , " + (!item.B.IsConnectedTo (LastNode)).ToString ());
							if (!item.A.IsConnectedTo (LastNode) && !item.B.IsConnectedTo (LastNode)) {
								Node IntersectionNode = new Node ((int)End.x - (int)direction.z, (int)End.z - (int)direction.x, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								LastNode.ConnectTo (IntersectionNode);
							}
						}
					}
					if (_edgemap [(int)End.x + (int)direction.x, (int)End.z + (int)direction.z].Count > 0) {
						foreach (var item in _edgemap[(int)End.x + (int)direction.x, (int)End.z + (int)direction.z].ToArray()) {
							if (!item.A.IsConnectedTo (LastNode) && !item.B.IsConnectedTo (LastNode)) {
								Node IntersectionNode = new Node ((int)End.x + (int)direction.x, (int)End.z + (int)direction.z, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								if (LastNode.ConnectedNodes.Count == 0) {
									RemoveNode (LastNode);
									End.x = (int)End.x + (int)direction.x;
									End.z = (int)End.z + (int)direction.z;
								} else {
									LastNode.ConnectTo (IntersectionNode);
								}
							}	
						}
					}
				}

				CreateRoadFrom2Nodes (Start, End);
				return;
			}
		}
		//We check if Start is on an edge
		if (_edgemap [(int)Start.x, (int)Start.z].Count > 0) {
			//We check if End is not already a node on the grid
			if (_map [(int)End.x, (int)End.z] != null) {
				//Start on an edge and End is alreay a node on the grid
				Vector3 direction = End - Start;
				direction.Normalize ();
				if (_edgemap [(int)Start.x, (int)Start.z] [0].getDirection () == (direction.z == 0)) {
					Debug.Log ("Orthogonal");
					Node StartNode = new Node ((int)Start.x, (int)Start.z, this);
					StartNode.ConnectTo (_edgemap [(int)Start.x, (int)Start.z] [0].A);
					StartNode.ConnectTo (_edgemap [(int)Start.x, (int)Start.z] [0].B);
					_edgemap [(int)Start.x, (int)Start.z] [0].A.DisconnectFrom (_edgemap [(int)Start.x, (int)Start.z] [0].B);
					AddNode (StartNode);
					Start.x = StartNode.X;
					Start.z = StartNode.Y;
				} else {
					if (_edgemap [(int)Start.x, (int)Start.z] [0].A.DistanceTo (End) > _edgemap [(int)Start.x, (int)Start.z] [0].B.DistanceTo (End)) {
						Start.x = _edgemap [(int)Start.x, (int)Start.z] [0].A.X;
						Start.z = _edgemap [(int)Start.x, (int)Start.z] [0].A.Y;
					} else {
						Start.x = _edgemap [(int)Start.x, (int)Start.z] [0].B.X;
						Start.z = _edgemap [(int)Start.x, (int)Start.z] [0].B.Y;
					}
				}
				CreateRoadFrom2Nodes (Start, End);
				return;
			}
			//We check if End is on an edge
			if (_edgemap [(int)End.x, (int)End.z].Count > 0) {
				Vector3 direction = End - Start;
				direction.Normalize ();
				if (_edgemap [(int)Start.x, (int)Start.z] [0].getDirection () == (direction.z == 0)) {
					Debug.Log ("Orthogonal");
					Node StartNode = new Node ((int)Start.x, (int)Start.z, this);
					StartNode.ConnectTo (_edgemap [(int)Start.x, (int)Start.z] [0].A);
					StartNode.ConnectTo (_edgemap [(int)Start.x, (int)Start.z] [0].B);
					_edgemap [(int)Start.x, (int)Start.z] [0].A.DisconnectFrom (_edgemap [(int)Start.x, (int)Start.z] [0].B);
					AddNode (StartNode);
					Start.x = StartNode.X;
					Start.z = StartNode.Y;
				} else {
					if (_edgemap [(int)Start.x, (int)Start.z] [0].A.DistanceTo (End) > _edgemap [(int)Start.x, (int)Start.z] [0].B.DistanceTo (End)) {
						Start.x = _edgemap [(int)Start.x, (int)Start.z] [0].A.X;
						Start.z = _edgemap [(int)Start.x, (int)Start.z] [0].A.Y;
					} else {
						Start.x = _edgemap [(int)Start.x, (int)Start.z] [0].B.X;
						Start.z = _edgemap [(int)Start.x, (int)Start.z] [0].B.Y;
					}
				}
				if (_edgemap [(int)End.x, (int)End.z] [0].getDirection () == (direction.z == 0)) {
					Debug.Log ("Orthogonal");
					Node EndNode = new Node ((int)End.x, (int)End.z, this);
					EndNode.ConnectTo (_edgemap [(int)End.x, (int)End.z] [0].A);
					EndNode.ConnectTo (_edgemap [(int)End.x, (int)End.z] [0].B);
					_edgemap [(int)End.x, (int)End.z] [0].A.DisconnectFrom (_edgemap [(int)End.x, (int)End.z] [0].B);
					AddNode (EndNode);
					End.x = EndNode.X;
					End.z = EndNode.Y;
				} else {
					if (_edgemap [(int)End.x, (int)End.z] [0].A.DistanceTo (Start) > _edgemap [(int)End.x, (int)End.z] [0].B.DistanceTo (Start)) {
						End.x = _edgemap [(int)End.x, (int)End.z] [0].A.X;
						End.z = _edgemap [(int)End.x, (int)End.z] [0].A.Y;
					} else {
						End.x = _edgemap [(int)End.x, (int)End.z] [0].B.X;
						End.z = _edgemap [(int)End.x, (int)End.z] [0].B.Y;
					}
				}
				CreateRoadFrom2Nodes (Start, End);
				return;
			} 
			//
			else {
				Vector3 direction = End - Start;
				direction.Normalize ();
				if (_edgemap [(int)Start.x, (int)Start.z] [0].getDirection () == (direction.z == 0)) {
					//Debug.Log ("Orthogonal");
					Node StartNode = new Node ((int)Start.x, (int)Start.z, this);
					StartNode.ConnectTo (_edgemap [(int)Start.x, (int)Start.z] [0].A);
					StartNode.ConnectTo (_edgemap [(int)Start.x, (int)Start.z] [0].B);
					_edgemap [(int)Start.x, (int)Start.z] [0].A.DisconnectFrom (_edgemap [(int)Start.x, (int)Start.z] [0].B);
					AddNode (StartNode);
					Start.x = StartNode.X;
					Start.z = StartNode.Y;
				} else {
					if (_edgemap [(int)Start.x, (int)Start.z] [0].A.DistanceTo (End) > _edgemap [(int)Start.x, (int)Start.z] [0].B.DistanceTo (End)) {
						Start.x = _edgemap [(int)Start.x, (int)Start.z] [0].A.X;
						Start.z = _edgemap [(int)Start.x, (int)Start.z] [0].A.Y;
					} else {
						Start.x = _edgemap [(int)Start.x, (int)Start.z] [0].A.X;
						Start.z = _edgemap [(int)Start.x, (int)Start.z] [0].A.Y;
					}
				}
				Node LastNode = new Node ((int)End.x, (int)End.z, this);
				AddNode (LastNode);

				bool keepOriginal = false;
				bool CheckEdge = true;
				if (_map [(int)End.x + (int)direction.z, (int)End.z + (int)direction.x] != null) {
					Debug.LogWarning ("Direction side 1");
					Node neihbourg = _map [(int)End.x + (int)direction.z, (int)End.z + (int)direction.x];
					bool keepNode = false;
					foreach (Node n in neihbourg.ConnectedNodes) {
						if (Vector3.Angle (neihbourg.DirectionTo (n), LastNode.DirectionTo (neihbourg)) != 0)
							keepNode = true;
					}
					if (!keepNode) {
						LastNode.ConnectTo (neihbourg.ConnectedNodes [0]);
						neihbourg.DisconnectFromEveryNodes ();
						RemoveNode (neihbourg);
					} else {
						LastNode.ConnectTo (neihbourg);
					}
					keepOriginal = true;
				}	
				if (_map [(int)End.x - (int)direction.z, (int)End.z - (int)direction.x] != null) {
					Node neihbourg = _map [(int)End.x - (int)direction.z, (int)End.z - (int)direction.x];
					Debug.LogWarning ("Direction side 2");
					bool keepNode = false;
					foreach (Node n in neihbourg.ConnectedNodes) {
						if (Vector3.Angle (neihbourg.DirectionTo (n), LastNode.DirectionTo (neihbourg)) != 0)
							keepNode = true;
					}
					if (!keepNode) {
						LastNode.ConnectTo (neihbourg.ConnectedNodes [0]);
						neihbourg.DisconnectFromEveryNodes ();
						RemoveNode (neihbourg);
					} else {
						LastNode.ConnectTo (neihbourg);
					}
					keepOriginal = true;
				}
				if (_map [(int)End.x + (int)direction.x, (int)End.z + (int)direction.z] != null) {
					Node neihbourg = _map [(int)End.x + (int)direction.x, (int)End.z + (int)direction.z];
					Debug.LogWarning ("Direction - 1");
					if (!keepOriginal) {
						RemoveNode (LastNode);
						End.x = neihbourg.X;
						End.z = neihbourg.Y;
						CheckEdge = false;
					} else {
						LastNode.ConnectTo (neihbourg);
					}
				}
				//We Check for neighbourg edges to connect to
				if (CheckEdge) {
					if (_edgemap [(int)End.x + (int)direction.z, (int)End.z + (int)direction.x].Count > 0) {
						foreach (var item in _edgemap[(int)End.x + (int)direction.z, (int)End.z + (int)direction.x].ToArray()) {
							if (!item.A.IsConnectedTo (LastNode) && !item.B.IsConnectedTo (LastNode)) {
								Node IntersectionNode = new Node ((int)End.x + (int)direction.z, (int)End.z + (int)direction.x, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								LastNode.ConnectTo (IntersectionNode);
							}
						}
					}	
					if (_edgemap [(int)End.x - (int)direction.z, (int)End.z - (int)direction.x].Count > 0) {
						foreach (var item in _edgemap[(int)End.x - (int)direction.z, (int)End.z - (int)direction.x].ToArray()) {
							Debug.Log ((!item.A.IsConnectedTo (LastNode)).ToString () + " , " + (!item.B.IsConnectedTo (LastNode)).ToString ());
							if (!item.A.IsConnectedTo (LastNode) && !item.B.IsConnectedTo (LastNode)) {
								Node IntersectionNode = new Node ((int)End.x - (int)direction.z, (int)End.z - (int)direction.x, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								LastNode.ConnectTo (IntersectionNode);
							}
						}
					}
					if (_edgemap [(int)End.x + (int)direction.x, (int)End.z + (int)direction.z].Count > 0) {
						foreach (var item in _edgemap[(int)End.x + (int)direction.x, (int)End.z + (int)direction.z].ToArray()) {
							if (!item.A.IsConnectedTo (LastNode) && !item.B.IsConnectedTo (LastNode)) {
								Node IntersectionNode = new Node ((int)End.x + (int)direction.x, (int)End.z + (int)direction.z, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								if (LastNode.ConnectedNodes.Count == 0) {
									RemoveNode (LastNode);
									End.x = (int)End.x + (int)direction.x;
									End.z = (int)End.z + (int)direction.z;
								} else {
									LastNode.ConnectTo (IntersectionNode);
								}
							}	
						}
					}
				}

				CreateRoadFrom2Nodes (Start, End);
				return;
			}
		}
		//Start isn't already a node and isn't on an edge
		else {
			//We check if End is not already a node on the grid
			if (_map [(int)End.x, (int)End.z] != null) {
				//TODO : End is already a node on the grid
				Node StartNode = new Node ((int)Start.x, (int)Start.z, this);
				AddNode (StartNode);
				Vector3 direction = End - Start;
				direction.Normalize ();
				//We Check for Neihbourg Nodes to connect to
				bool keepOriginal = false; 	
				bool CheckEdge = true;
				if (_map [(int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x] != null) {
					Debug.LogWarning ("Direction side 1");
					Node neihbourg = _map [(int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x];
					bool keepNode = false;
					foreach (Node n in neihbourg.ConnectedNodes) {
						if (Vector3.Angle (neihbourg.DirectionTo (n), StartNode.DirectionTo (neihbourg)) != 0)
							keepNode = true;
					}
					if (!keepNode) {
						StartNode.ConnectTo (neihbourg.ConnectedNodes [0]);
						neihbourg.DisconnectFromEveryNodes ();
						RemoveNode (neihbourg);
					} else {
						StartNode.ConnectTo (neihbourg);
					}
					keepOriginal = true;
				}	
				if (_map [(int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x] != null) {
					Node neihbourg = _map [(int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x];
					Debug.LogWarning ("Direction side 2");
					bool keepNode = false;
					foreach (Node n in neihbourg.ConnectedNodes) {
						if (Vector3.Angle (neihbourg.DirectionTo (n), StartNode.DirectionTo (neihbourg)) != 0)
							keepNode = true;
					}
					if (!keepNode) {
						StartNode.ConnectTo (neihbourg.ConnectedNodes [0]);
						neihbourg.DisconnectFromEveryNodes ();
						RemoveNode (neihbourg);
					} else {
						StartNode.ConnectTo (neihbourg);
					}
					keepOriginal = true;
				}
				if (_map [(int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z] != null) {
					Node neihbourg = _map [(int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z];
					Debug.LogWarning ("Direction - 1");
					if (!keepOriginal) {
						RemoveNode (StartNode);
						Start.x = neihbourg.X;
						Start.z = neihbourg.Y;
						CheckEdge = false;
					} else {
						StartNode.ConnectTo (neihbourg);
					}
				}
				//We Check for neighbourg edges to connect to
				if (CheckEdge) {
					if (_edgemap [(int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x].Count > 0) {
						foreach (var item in _edgemap[(int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x].ToArray()) {
							if (!item.A.IsConnectedTo (StartNode) && !item.B.IsConnectedTo (StartNode)) {
								Node IntersectionNode = new Node ((int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								StartNode.ConnectTo (IntersectionNode);
							}
						}
					}	
					if (_edgemap [(int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x].Count > 0) {
						foreach (var item in _edgemap[(int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x].ToArray()) {
							Debug.Log ((!item.A.IsConnectedTo (StartNode)).ToString () + " , " + (!item.B.IsConnectedTo (StartNode)).ToString ());
							if (!item.A.IsConnectedTo (StartNode) && !item.B.IsConnectedTo (StartNode)) {
								Node IntersectionNode = new Node ((int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								StartNode.ConnectTo (IntersectionNode);
							}
						}
					}
					if (_edgemap [(int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z].Count > 0) {
						foreach (var item in _edgemap[(int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z].ToArray()) {
							if (!item.A.IsConnectedTo (StartNode) && !item.B.IsConnectedTo (StartNode)) {
								Node IntersectionNode = new Node ((int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								if (StartNode.ConnectedNodes.Count == 0) {
									RemoveNode (StartNode);
									Start.x = (int)Start.x - (int)direction.x;
									Start.z = (int)Start.z - (int)direction.z;
								} else {
									StartNode.ConnectTo (IntersectionNode);
								}
							}	
						}
					}
				}

				CreateRoadFrom2Nodes (Start, End);
				return;
			}
			//We check if End is on an edge
			if (_edgemap [(int)End.x, (int)End.z].Count > 0) {
				//TODO : End is on a edge
				Node StartNode = new Node ((int)Start.x, (int)Start.z, this);
				AddNode (StartNode);
				Vector3 direction = End - Start;
				direction.Normalize ();
				//We Check for Neihbourg Nodes to connect to
				bool keepOriginal = false;
				bool CheckEdge = true;
				if (_map [(int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x] != null) {
					Debug.LogWarning ("Direction side 1");
					Node neihbourg = _map [(int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x];
					bool keepNode = false;
					foreach (Node n in neihbourg.ConnectedNodes) {
						if (Vector3.Angle (neihbourg.DirectionTo (n), StartNode.DirectionTo (neihbourg)) != 0)
							keepNode = true;
					}
					if (!keepNode) {
						StartNode.ConnectTo (neihbourg.ConnectedNodes [0]);
						neihbourg.DisconnectFromEveryNodes ();
						RemoveNode (neihbourg);
					} else {
						StartNode.ConnectTo (neihbourg);
					}
					keepOriginal = true;
				}	
				if (_map [(int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x] != null) {
					Node neihbourg = _map [(int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x];
					Debug.LogWarning ("Direction side 2");
					bool keepNode = false;
					foreach (Node n in neihbourg.ConnectedNodes) {
						if (Vector3.Angle (neihbourg.DirectionTo (n), StartNode.DirectionTo (neihbourg)) != 0)
							keepNode = true;
					}
					if (!keepNode) {
						StartNode.ConnectTo (neihbourg.ConnectedNodes [0]);
						neihbourg.DisconnectFromEveryNodes ();
						RemoveNode (neihbourg);
					} else {
						StartNode.ConnectTo (neihbourg);
					}
					keepOriginal = true;
				}
				if (_map [(int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z] != null) {
					Node neihbourg = _map [(int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z];
					Debug.LogWarning ("Direction - 1");
					if (!keepOriginal) {
						RemoveNode (StartNode);
						Start.x = neihbourg.X;
						Start.z = neihbourg.Y;
						CheckEdge = false;
					} else {
						StartNode.ConnectTo (neihbourg);
					}
				}
				//We Check for neighbourg edges to connect to
				if (CheckEdge) {
					if (_edgemap [(int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x].Count > 0) {
						foreach (var item in _edgemap[(int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x].ToArray()) {
							if (!item.A.IsConnectedTo (StartNode) && !item.B.IsConnectedTo (StartNode)) {
								Node IntersectionNode = new Node ((int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								StartNode.ConnectTo (IntersectionNode);
							}
						}
					}	
					if (_edgemap [(int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x].Count > 0) {
						foreach (var item in _edgemap[(int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x].ToArray()) {
							Debug.Log ((!item.A.IsConnectedTo (StartNode)).ToString () + " , " + (!item.B.IsConnectedTo (StartNode)).ToString ());
							if (!item.A.IsConnectedTo (StartNode) && !item.B.IsConnectedTo (StartNode)) {
								Node IntersectionNode = new Node ((int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								StartNode.ConnectTo (IntersectionNode);
							}
						}
					}
					if (_edgemap [(int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z].Count > 0) {
						foreach (var item in _edgemap[(int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z].ToArray()) {
							if (!item.A.IsConnectedTo (StartNode) && !item.B.IsConnectedTo (StartNode)) {
								Node IntersectionNode = new Node ((int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								if (StartNode.ConnectedNodes.Count == 0) {
									RemoveNode (StartNode);
									Start.x = (int)Start.x - (int)direction.x;
									Start.z = (int)Start.z - (int)direction.z;
								} else {
									StartNode.ConnectTo (IntersectionNode);
								}
							}	
						}
					}
				}

				if (_edgemap [(int)End.x, (int)End.z] [0].getDirection () == (direction.z == 0)) {
					Node EndNode = new Node ((int)End.x, (int)End.z, this);
					EndNode.ConnectTo (_edgemap [(int)End.x, (int)End.z] [0].A);
					EndNode.ConnectTo (_edgemap [(int)End.x, (int)End.z] [0].B);
					_edgemap [(int)End.x, (int)End.z] [0].A.DisconnectFrom (_edgemap [(int)End.x, (int)End.z] [0].B);
					AddNode (EndNode);
					End.x = EndNode.X;
					End.z = EndNode.Y;
				} else {
					Debug.Log (_edgemap [(int)End.x, (int)End.z] [0].A.DistanceTo (Start));
					Debug.Log (_edgemap [(int)End.x, (int)End.z] [0].B.DistanceTo (Start));
					if (_edgemap [(int)End.x, (int)End.z] [0].A.DistanceTo (Start) > _edgemap [(int)End.x, (int)End.z] [0].B.DistanceTo (Start)) {
						End.x = _edgemap [(int)End.x, (int)End.z] [0].A.X;
						End.z = _edgemap [(int)End.x, (int)End.z] [0].A.Y;
					} else {
						End.x = _edgemap [(int)End.x, (int)End.z] [0].B.X;
						End.z = _edgemap [(int)End.x, (int)End.z] [0].B.Y;
					}
				}
				CreateRoadFrom2Nodes (Start, End);
				return;
			} 
			//
			else {
				Vector3 direction = (End - Start).normalized;
				Node StartNode = new Node ((int)Start.x, (int)Start.z, this);
				AddNode (StartNode);

				//We Check for Neihbourg Nodes to connect to
				bool keepOriginal = false;
				bool CheckEdge = true;
				if (_map [(int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x] != null) {
					Debug.LogWarning ("Direction side 1");
					Node neihbourg = _map [(int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x];
					bool keepNode = false;
					foreach (Node n in neihbourg.ConnectedNodes) {
						if (Vector3.Angle (neihbourg.DirectionTo (n), StartNode.DirectionTo (neihbourg)) != 0)
							keepNode = true;
					}
					if (!keepNode) {
						StartNode.ConnectTo (neihbourg.ConnectedNodes [0]);
						neihbourg.DisconnectFromEveryNodes ();
						RemoveNode (neihbourg);
					} else {
						StartNode.ConnectTo (neihbourg);
					}
					keepOriginal = true;
				}	
				if (_map [(int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x] != null) {
					Node neihbourg = _map [(int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x];
					Debug.LogWarning ("Direction side 2");
					bool keepNode = false;
					foreach (Node n in neihbourg.ConnectedNodes) {
						if (Vector3.Angle (neihbourg.DirectionTo (n), StartNode.DirectionTo (neihbourg)) != 0)
							keepNode = true;
					}
					if (!keepNode) {
						StartNode.ConnectTo (neihbourg.ConnectedNodes [0]);
						neihbourg.DisconnectFromEveryNodes ();
						RemoveNode (neihbourg);
					} else {
						StartNode.ConnectTo (neihbourg);
					}
					keepOriginal = true;
				}
				if (_map [(int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z] != null) {
					Node neihbourg = _map [(int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z];
					Debug.LogWarning ("Direction - 1");
					if (!keepOriginal) {
						RemoveNode (StartNode);
						Start.x = neihbourg.X;
						Start.z = neihbourg.Y;
						CheckEdge = false;
					} else {
						StartNode.ConnectTo (neihbourg);
					}
				}
				//We Check for neighbourg edges to connect to
				if (CheckEdge) {
					if (_edgemap [(int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x].Count > 0) {
						foreach (var item in _edgemap[(int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x].ToArray()) {
							if (!item.A.IsConnectedTo (StartNode) && !item.B.IsConnectedTo (StartNode)) {
								Node IntersectionNode = new Node ((int)Start.x + (int)direction.z, (int)Start.z + (int)direction.x, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								StartNode.ConnectTo (IntersectionNode);
							}
						}
					}	
					if (_edgemap [(int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x].Count > 0) {
						foreach (var item in _edgemap[(int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x].ToArray()) {
							Debug.Log ((!item.A.IsConnectedTo (StartNode)).ToString () + " , " + (!item.B.IsConnectedTo (StartNode)).ToString ());
							if (!item.A.IsConnectedTo (StartNode) && !item.B.IsConnectedTo (StartNode)) {
								Node IntersectionNode = new Node ((int)Start.x - (int)direction.z, (int)Start.z - (int)direction.x, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								StartNode.ConnectTo (IntersectionNode);
							}
						}
					}
					if (_edgemap [(int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z].Count > 0) {
						foreach (var item in _edgemap[(int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z].ToArray()) {
							if (!item.A.IsConnectedTo (StartNode) && !item.B.IsConnectedTo (StartNode)) {
								Node IntersectionNode = new Node ((int)Start.x - (int)direction.x, (int)Start.z - (int)direction.z, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								if (StartNode.ConnectedNodes.Count == 0) {
									RemoveNode (StartNode);
									Start.x = (int)Start.x - (int)direction.x;
									Start.z = (int)Start.z - (int)direction.z;
								} else {
									StartNode.ConnectTo (IntersectionNode);
								}
							}	
						}
					}
				}


				Node LastNode = new Node ((int)End.x, (int)End.z, this);
				AddNode (LastNode);

				//We Check for Neihbourg Nodes to connect to
				keepOriginal = false;
				CheckEdge = true;
				if (_map [(int)End.x + (int)direction.z, (int)End.z + (int)direction.x] != null) {
					Debug.LogWarning ("Direction side 1");
					Node neihbourg = _map [(int)End.x + (int)direction.z, (int)End.z + (int)direction.x];
					bool keepNode = false;
					foreach (Node n in neihbourg.ConnectedNodes) {
						if (Vector3.Angle (neihbourg.DirectionTo (n), LastNode.DirectionTo (neihbourg)) != 0)
							keepNode = true;
					}
					if (!keepNode) {
						LastNode.ConnectTo (neihbourg.ConnectedNodes [0]);
						neihbourg.DisconnectFromEveryNodes ();
						RemoveNode (neihbourg);
					} else {
						LastNode.ConnectTo (neihbourg);
					}
					keepOriginal = true;
				}	
				if (_map [(int)End.x - (int)direction.z, (int)End.z - (int)direction.x] != null) {
					Node neihbourg = _map [(int)End.x - (int)direction.z, (int)End.z - (int)direction.x];
					Debug.LogWarning ("Direction side 2");
					bool keepNode = false;
					foreach (Node n in neihbourg.ConnectedNodes) {
						if (Vector3.Angle (neihbourg.DirectionTo (n), LastNode.DirectionTo (neihbourg)) != 0)
							keepNode = true;
					}
					if (!keepNode) {
						LastNode.ConnectTo (neihbourg.ConnectedNodes [0]);
						neihbourg.DisconnectFromEveryNodes ();
						RemoveNode (neihbourg);
					} else {
						LastNode.ConnectTo (neihbourg);
					}
					keepOriginal = true;
				}
				if (_map [(int)End.x + (int)direction.x, (int)End.z + (int)direction.z] != null) {
					Node neihbourg = _map [(int)End.x + (int)direction.x, (int)End.z + (int)direction.z];
					Debug.LogWarning ("Direction - 1");
					if (!keepOriginal) {
						RemoveNode (LastNode);
						End.x = neihbourg.X;
						End.z = neihbourg.Y;
						CheckEdge = false;
					} else {
						LastNode.ConnectTo (neihbourg);
					}
				}
				//We Check for neighbourg edges to connect to
				if (CheckEdge) {
					if (_edgemap [(int)End.x + (int)direction.z, (int)End.z + (int)direction.x].Count > 0) {
						foreach (var item in _edgemap[(int)End.x + (int)direction.z, (int)End.z + (int)direction.x].ToArray()) {
							if (!item.A.IsConnectedTo (LastNode) && !item.B.IsConnectedTo (LastNode)) {
								Node IntersectionNode = new Node ((int)End.x + (int)direction.z, (int)End.z + (int)direction.x, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								LastNode.ConnectTo (IntersectionNode);
							}
						}
					}	
					if (_edgemap [(int)End.x - (int)direction.z, (int)End.z - (int)direction.x].Count > 0) {
						foreach (var item in _edgemap[(int)End.x - (int)direction.z, (int)End.z - (int)direction.x].ToArray()) {
							Debug.Log ((!item.A.IsConnectedTo (LastNode)).ToString () + " , " + (!item.B.IsConnectedTo (LastNode)).ToString ());
							if (!item.A.IsConnectedTo (LastNode) && !item.B.IsConnectedTo (LastNode)) {
								Node IntersectionNode = new Node ((int)End.x - (int)direction.z, (int)End.z - (int)direction.x, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								LastNode.ConnectTo (IntersectionNode);
							}
						}
					}
					if (_edgemap [(int)End.x + (int)direction.x, (int)End.z + (int)direction.z].Count > 0) {
						foreach (var item in _edgemap[(int)End.x + (int)direction.x, (int)End.z + (int)direction.z].ToArray()) {
							if (!item.A.IsConnectedTo (LastNode) && !item.B.IsConnectedTo (LastNode)) {
								Node IntersectionNode = new Node ((int)End.x + (int)direction.x, (int)End.z + (int)direction.z, this);
								AddNode (IntersectionNode);
								item.A.ConnectTo (IntersectionNode);
								item.B.ConnectTo (IntersectionNode);
								item.A.DisconnectFrom (item.B);	
								if (LastNode.ConnectedNodes.Count == 0) {
									RemoveNode (LastNode);
									End.x = (int)End.x + (int)direction.x;
									End.z = (int)End.z + (int)direction.z;
								} else {
									LastNode.ConnectTo (IntersectionNode);
								}
							}	
						}
					}
				}
				CreateRoadFrom2Nodes (Start, End);
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
				Vector3 pos = new Vector3 (item.X + 0.5f, 5f, item.Y + 0.5f);
				
				Gizmos.DrawSphere (pos, 0.5f);
				
			}
			
			Gizmos.color = Color.blue;
			float height = 0f;
			foreach (var item in _map) {
				if (item == null)
					continue;
				Vector3 posA = new Vector3 (item.X + 0.5f, 5f + height, item.Y + 0.5f);
				foreach (var CoN in item.ConnectedNodes) {
					Vector3 posB = new Vector3 (CoN.X + 0.5f, 5f + height, CoN.Y + 0.5f);
					Gizmos.DrawLine (posA, posB);
				}
				//Debug.Log (item.X.ToString () + " | " + item.Y.ToString () + " | " + height.ToString () + " | " + item.ConnectedNodes.Count.ToString ());
				height += 0.5f;
			}
		}
	}
}
