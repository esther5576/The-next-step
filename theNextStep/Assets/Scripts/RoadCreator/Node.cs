﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
	public List<Node> ConnectedNodes {
		get {
			return _connectedNodes;
		}
	}
	public int X {
		get {
			return _x;
		}
	}
	public int Y {
		get {
			return _y;
		}
	}
	
	protected List<Node> _neighbours {
		get {
			int x0, x1, y0, y1;
			x0 = (_x - 1 < 0) ? _x : _x - 1;
			x1 = (_x + 1 > _grid.SixeX) ? _x : _x + 1;
			y0 = (_y - 1 < 0) ? _y : _y - 1;
			y1 = (_y + 1 > _grid.SixeY) ? _y : _y + 1;
			List<Node> Neighbour = new List<Node> ();
			
			if (_grid.Map [x0, _y] != null && _grid.Map [x0, _y] != this)
				Neighbour.Add (_grid.Map [x0, _y]);
			if (_grid.Map [x1, _y] != null && _grid.Map [x1, _y] != this)
				Neighbour.Add (_grid.Map [x1, _y]);
			if (_grid.Map [_x, y0] != null && _grid.Map [_x, y0] != this)
				Neighbour.Add (_grid.Map [_x, y0]);		
			if (_grid.Map [_x, y1] != null && _grid.Map [_x, y1] != this)
				Neighbour.Add (_grid.Map [_x, y1]);
			return Neighbour;
		}
	}
	
	protected int _x, _y;
	protected List<Node> _connectedNodes;
	protected Grid _grid;
	public Node (int x, int y, Grid ParentGrid)
	{
		_x = x;
		_y = y;
		_grid = ParentGrid;
		_connectedNodes = new List<Node> ();
		ConnectTo (_neighbours);
	}
	
	public void ConnectTo (Node Target)
	{
		if (Target == this)
			return;
		if (!_connectedNodes.Contains (Target))
			_connectedNodes.Add (Target);
		if (Target.ConnectedNodes.Contains (this))
			return;
		Target.ConnectTo (this);
		if (_x == Target.X) {
			Node A = (_y > Target.Y) ? Target : this;
			Node B = (A == this) ? Target : this;
			Edge AB = new Edge ();
			AB.A = A;
			AB.B = B;
			for (int i = A.Y; i < B.Y; i++) {
				_grid._edgemap [_x, i].Add (AB);
			}
		} else {
			Node A = (_x > Target.X) ? Target : this;
			Node B = (A == this) ? Target : this;
			Edge AB = new Edge ();
			AB.A = A;
			AB.B = B;
			for (int i = A.X; i < B.X; i++) {
				_grid._edgemap [i, _y].Add (AB);
			}
		}
		
	}
	
	public void ConnectTo (List<Node> Targets)
	{
		foreach (var item in Targets) {
			ConnectTo (item);
		}
	}
	
	public void DisconnectFrom (Node Target)
	{
		_connectedNodes.Remove (Target);
		if (Target.ConnectedNodes.Contains (this)) {
			Target.DisconnectFrom (this);
			if (_x == Target.X) {
				Node A = (_y > Target.Y) ? Target : this;
				Node B = (A == this) ? Target : this;
				Edge AB = new Edge ();
				_grid.RemoveEdge (AB);
				
			} else {
				Node A = (_x > Target.X) ? Target : this;
				Node B = (A == this) ? Target : this;
				Edge AB = new Edge ();
				_grid.RemoveEdge (AB);
			}
		}
		
	}
	
	public void DisconnectFromEveryNodes ()
	{
		foreach (var item in _connectedNodes) {
			DisconnectFrom (item);
		}
	}
	
	bool IsConnectedTo (Node Target)
	{
		List<Node> ToChek = new List<Node> ();
		List<Node> Checked = new List<Node> ();
		ToChek.Add (this);
		while (ToChek.Count > 0) {
			if (ToChek [0]._connectedNodes.Contains (Target))
				return true;
			Checked.Add (ToChek [0]);
			foreach (var item in ToChek[0]._connectedNodes) {
				if (!ToChek.Contains (item) && !Checked.Contains (item)) {
					ToChek.Add (item);
				}
			}
			ToChek.RemoveAt (0);
		}
		return false;
	}
	
}
