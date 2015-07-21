using UnityEngine;
using System.Collections;

public class Edge
{
	public Node A;
	public Node B;

	public bool getDirection ()
	{
		if (A.X == B.X)
			return true;
		else {
			return false;
		}
	}
}
