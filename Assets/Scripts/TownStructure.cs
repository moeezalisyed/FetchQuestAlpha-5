using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//[RequireComponent(typeof(MeshCollider))]
public class TownStructure : MonoBehaviour {
    //Constant values for building type
	private Vector3 screenPoint;
	private Vector3 offset;

	private const int WORKSHOP = 0;
    private const int BLACKSMITH = 1;
    private const int APOTHECARY = 2;
    private const int TANNERY = 3;
    private const int CHURCH = 4;
    private const int MISCSTRUCTURE = 5;
	private TownStructModel model = null;

	public int updateLevel = 1;
    private List<Hero> employees;

    public int structureType;

	// Created goldreq as a function of goldgenerated. Everytime you need goldGeneration*3 in order to upgrade
	public int goldReq;

    private int goldGeneration;
	public int timer;
	//public Vector3 TownStructurePosition = null;


	private Player owner;

	public GameManager parentGameManager;
	// Use this for initialization
	public string xname = "";


	// Use this for initialization


	public GameManager structManager;

	// Use this for initialization


	public void init (int structType, GameManager m, Player owner) {
		this.structureType = structType;

		this.structManager = m;

		this.owner = owner;

		if (structType == 0) {
			//Reduces the fetch time for random heroes
			foreach (Hero x in m.HeroesSet) {
				System.Random rnd = new System.Random();
				int xs = rnd.Next(0, 3);
				if (xs == 1) {
					x.fetchTime -= 10;
					x.grindTime -= 10;
				}
			}

			xname = "" + "Name: Workshop \n Requires: An Assassin \n Cost: 300 Gold \n Effect: Generates 1 XP per second";

		}

		if (structType == 1) {
			xname = "" + "Name: Blacksmith \n Requires: A Juggernaut \n Cost: 500 Gold \n Effect: Increases quests reward by 10 Gold";
		}

		if (structType == 2) {
			xname = "" + "Name: Apothecary \n Requires: A Juggernaut \n Cost: 300 Gold \n Effect: Generates 1 XP per second";
		}

		if (structType == 3) {
			xname = "" + "Name: Tannery \n Requires: Another Building \n Cost: 450 Gold \n Effect: Reduces Quest's time by 10 seconds";
		}

		if (structType == 4) {
			xname = "" + "Name: Church \n Requires: An Oracle \n Cost: 1000 Gold \n Effect: Generates 1 Gold per second";
		}



		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<TownStructModel>();						// Add a gemModel script to control visuals of the gem.
		model.init(this, structType);	


		//transform.localPosition = new Vector3 (20, 10, 0);
		//var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);

		//mat = GetComponent<Renderer>().material;								// Get the material component of this quad object.
		//mat.mainTexture = Resources.Load<Texture2D>("Textures/testTownStructure0");	// Set the texture.  Must be in Resources folder.
		//	mat.color = new Color(1f,1f,1);											// Set the color (easy way to tint things).
		//mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency. 
		//modelObject.AddComponent<mat> ();



		// Initialized GoldGeneration as this fucntion; I dont know what the better way to design this is. 
		//goldGeneration = 100 + (15 * structType % 2);

		if (this.structureType == WORKSHOP || this.structureType == APOTHECARY) {
			StartCoroutine (addXPReward ());
		}

		if ( this.structureType == CHURCH ) {
			StartCoroutine (addGoldReward ());
		}

		if ( this.structureType == BLACKSMITH ) {
			foreach (Quest x in this.structManager.QuestsSet) {
				x.goldReward += 10;
			}
		}

		if (this.structureType == TANNERY) {
			foreach (Quest x in this.structManager.QuestsSet) {
				x.TimeNeeded -= 10;
			}
		}



		//goldReq = goldGeneration * 3;
	}

	public void updatelevel(){
		int newLevel = updateLevel + 1;
		if (newLevel > 5) {
			newLevel = 5;
		}
		this.goldGeneration = (goldGeneration / updateLevel) * newLevel;
		goldReq = goldGeneration * 3;
		this.updateLevel = newLevel;

	}
	/*
	void OnMouseUpAsButton () {
		//print ("hello");
		//structManager.goldCheckAndUpdate (this);
		//structManager.infoAboutBuilding();
		float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
		Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
		transform.position = new Vector3( pos_move.x, transform.position.y, pos_move.z );
	}*/

	void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void onMouseDrag(){
		print ("hello");
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		transform.position = cursorPosition;
	
	}


	


	IEnumerator addXPReward(){
		while (true) {
			yield return new WaitForSeconds (1);
			if (structManager.gameStart == true){
				structManager.THEPLAYER.XP += 1;
		}
			//print (goldGeneration);
			//yield return new WaitForSeconds (400);
		}
	}

	IEnumerator addGoldReward(){
		while (true) {
			yield return new WaitForSeconds (1);
			if (structManager.gameStart == true) {
				structManager.THEPLAYER.Gold += 1;
			}
			//print (goldGeneration);
			//yield return new WaitForSeconds (400);
		}
	}




	// Update is called once per frame
	void Update () {

	}
}
