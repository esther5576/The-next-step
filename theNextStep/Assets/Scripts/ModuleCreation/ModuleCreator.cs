using UnityEngine;
using System.Collections;

public class ModuleCreator : MonoBehaviour
{
	public float GridCellSize {
		get{ return _gridCellSize;}
	}
	public static ModuleCreator Instance {
		get {
			if (!_instance) {
				GameObject Creator = new GameObject ("ModuleCreator");
				_instance = Creator.AddComponent<ModuleCreator> ();
			}
			return _instance;
		}
	}
	protected Terrain _terrain;

	protected bool _onDeployement = false;
	protected static ModuleCreator _instance;
	protected float _gridCellSize;

	public float _price;

	public bool _OnSound;
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void Initiate (float GridCellSize)
	{
		_gridCellSize = GridCellSize;
	}
	
	/// <summary>
	/// Deploy the specified Object.
	/// </summary>
	/// <param name="ObjectToDeploy">Object to deploy</param>
	public void Deploy (GameObject ObjectToDeploy)
	{
		if (!_onDeployement) {
			StartCoroutine (Deployement (ObjectToDeploy));
		}
	}

	/// <summary>
	/// Deployement of the specified Object.
	/// </summary>
	/// <param name="ObjectToDeploy">Object to deploy.</param>
	private IEnumerator Deployement (GameObject ObjectToDeploy)
	{
		_onDeployement = true;
		//we create our visualtion gameobject
		GameObject visualisation = Instantiate (ObjectToDeploy);//this object let the player know where he's going to place his building
		visualisation.name = "visualisation";
		visualisation.layer = 2;
		visualisation.AddComponent<Visualisation> ();
		if (!visualisation.GetComponent<Rigidbody> ()) {//we had a rigidbody to check collision
			Rigidbody r = visualisation.AddComponent<Rigidbody> ();
			r.useGravity = false;//we don't want gravity
			r.constraints = RigidbodyConstraints.FreezeAll;//we don't want the gameobject to interact with anything following physics, it's a trigger
			r.GetComponent<Collider> ().isTrigger = true;
		}
		//Material visualMat = visualisation.GetComponent<Renderer> ().material;
		//visualMat.color = new Color (visualMat.color.r, visualMat.color.g, visualMat.color.b, 0.4f);//we change the color of the GameObject to make it transparent

		Vector3 visualSize = visualisation.GetComponent<Collider> ().bounds.size;//we store the size of the bounding box of the object for Terrain checking
		visualisation.GetComponent<BoxCollider> ().size *= 0.9f;
		while (_onDeployement) {
		
			//we raycast from the cursor to the ground
			Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast (r, out hit)) {
				Vector3 pos = hit.point;//we store the hit point to apply modification for Terrain checking
				pos -= visualSize / 2;
				pos.x = (pos.x % _gridCellSize < _gridCellSize / 2f) ? _gridCellSize * ((int)(pos.x / _gridCellSize)) : _gridCellSize * ((int)(pos.x / _gridCellSize) + 1);
				pos.z = (pos.z % _gridCellSize < _gridCellSize / 2f) ? _gridCellSize * ((int)(pos.z / _gridCellSize)) : _gridCellSize * ((int)(pos.z / _gridCellSize) + 1);
				pos += visualSize / 2;
				visualisation.transform.position = pos;//we move the object according to the cursor				

				pos -= visualSize / 2;//to get the left bottom corner of the bounding box
				pos.y += visualSize.y;
				visualisation.GetComponent<Visualisation> ().BuildAble = CheckForConstruction (pos, visualSize, 5, 0.1f);//we check if we can build on the Terrain and we set the according propreties fo the visualition
				if (!visualisation.GetComponent<Visualisation> ().BuildAble) {//if we can't construct we jump at the end of the while
					goto end;
				}
			}
			//if we can build on the Terrain and the visualisation object doesn't collide with any other objet we build it and destoy the visualisation object
			if (Input.GetMouseButtonDown (0) && !visualisation.GetComponent<Visualisation> ().Collide) {
				GameObject module = Instantiate (ObjectToDeploy, new Vector3 (visualisation.transform.position.x, visualisation.transform.position.y, visualisation.transform.position.z), visualisation.transform.rotation) as GameObject;
				_onDeployement = false;

				if (ObjectToDeploy.name == "landingzone") {
					Camera.main.GetComponent<Modules> ()._landingZoneCreated = true;
				}

				Debug.Log (module);
				module.GetComponent<ProductionAndNeeds> ()._associateNode = Terrain.activeTerrain.GetComponent<Grid> ().AddBuilding (new Vector3 (visualisation.transform.position.x, visualisation.transform.position.y, visualisation.transform.position.z), visualSize);
				//Camera.main.GetComponent<Modules> ()._maximumBuildings --;//tmp
				Camera.main.GetComponent<gameStats> ()._actualConstructionMaterials -= _price;
				DestroyImmediate (visualisation);
				_OnSound = true;
			}
			end:
			if (Input.GetMouseButtonDown (1)) {
				_onDeployement = false;
				DestroyImmediate (visualisation);
			}
			yield return null;
		}
	}

	/// <summary>
	/// Checks for construction.
	/// </summary>
	/// <returns><c>true</c>if consctruction is possible <c>false</c> if consctruction isn't possible.</returns>
	/// <param name="pos">The position of the object (left bottom corner)</param>
	/// <param name="size">The size of the bound</param>
	/// <param name="raycastMultiplier">The raycast multiplier (number of raycast = (raycastMultiplier + 1)²</param>
	/// <param name="DiffMax">The maximum height difference for construction</param>
	bool CheckForConstruction (Vector3 pos, Vector3 size, int raycastMultiplier, float DiffMax)
	{
		Vector3 Unit = size / raycastMultiplier;
		float Height = 0f;

		for (int i = 0; i <= raycastMultiplier; i++) {
			for (int j = 0; j <= raycastMultiplier; j++) {
				Vector3 raypos = Unit;
				raypos.x *= i;
				raypos.z *= j;
				RaycastHit hit;
				if (Physics.Raycast (pos + raypos, -Vector3.up, out hit)) {
					Height += hit.point.y;
				}
			}
		}
		Height /= raycastMultiplier * raycastMultiplier;

		return Height < DiffMax;
	}



}
