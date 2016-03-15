using UnityEngine;
using System.Collections;

public class TownStructModel : MonoBehaviour {

	private TownStructure own;
	public float posx;
	public float posy;
	public float posyu = Screen.height - 60;
	private Material mat;

    private Renderer rend;

    // Use this for initialization
    public void init(TownStructure owner, int TownStructure){
        //	this.own = owner;
        this.own = owner;


        transform.parent = own.transform;                   // Set the model's parent to the gem.
        System.Random rnd = new System.Random();

        //Camera camera = own.parentGameManager.GetComponent<Camera>();

        //transform.localPosition = camera.ScreenToWorldPoint(own.transform.position);		// Center the model on the parent.
        Vector3 p = own.transform.position;
        p = Camera.main.ScreenToWorldPoint(p);
        //print(p);
        p.z = -1;
        transform.localPosition = new Vector3(0f, 0.2f, -1.0f);
        //owner.GetComponent<BoxCollider2D> ().offset = p;

        name = "Struct Model";                                  // Name the object.



        mat = GetComponent<Renderer>().material;                                // Get the material component of this quad object.
        mat.mainTexture = Resources.Load<Texture2D>("TextureFold/361_structure2");  // Set the texture.  Must be in Resources folder.
        mat.color = new Color(1f, 1f, 1);                                           // Set the color (easy way to tint things).
        mat.shader = Shader.Find("Sprites/Default");

        rend = GetComponent<Renderer>();
        rend.sortingLayerName = "buildingLayer";


    }


    //	transform.parent = own.transform;					// Set the model's parent to the gem.
    //	System.Random rnd = new System.Random();
    //	 posx = rnd.Next (Screen.width/2 - 300 , Screen.width/2 + 300);
    //	posy = rnd.Next (Screen.height/2 - 300, Screen.height/2 + 300 );

    //	if (TownStructure == 5) {
    //		posx = Screen.width / 2;
    //		posy = Screen.height - 60;
    //		StartCoroutine (moveAttack ());
    //	}
    //	 //posx = 0;
    //	 //posy = 0;
    //	//StartCoroutine (getPosition ());

    ////	Camera camera = own.parentGameManager.GetComponent<Camera>();

    //	//transform.localPosition = camera.ScreenToWorldPoint(new Vector3(x,y,0));		// Center the model on the parent.
    //	Vector3 p =new Vector3(posx,posy,-1);
    //	p = Camera.main.ScreenToWorldPoint (p);
    //	//print (p);
    //	p.z = -1;
    //	transform.localPosition = p;
    //	print(p);
    //	owner.GetComponent<BoxCollider2D> ().offset = p;

    //	name = "Struct Model";									// Name the object.

    //	mat = GetComponent<Renderer>().material;								// Get the material component of this quad object.
    //	mat.mainTexture = Resources.Load<Texture2D>("TextureFold/testtownstructure"+TownStructure);	// Set the texture.  Must be in Resources folder.
    //		mat.color = new Color(1f,1f,1);											// Set the color (easy way to tint things).
    //	mat.shader = Shader.Find ("Sprites/Default");	




	//IEnumerator getPosition(){
	////	displayForPlace = "Please click on a position for this building";
	//	bool x = true;
	//	Vector3 pos = new Vector3(0,0,0);
	//	while (x) {
	//		yield return new WaitForSeconds (1);
	//		if(Input.GetMouseButtonUp(0)) 
	//			pos = Input.mousePosition;

	//		if (pos.z != 0) {
	//			x = false;
	//		}
	//	}
	//	posx = pos.x;
	//	posy = pos.y;


	//	//displayForPlace = "";

	//}

	void Update(){
		/*if (this.own.structureType == 5) {
			Vector3 p = new Vector3 (posx, posy--, -1);
			transform.localPosition = p;
			own.GetComponent<BoxCollider2D> ().offset = p;
		}*/

	}

	IEnumerator moveAttack(){
		while (true) {
			yield return new WaitForSeconds (1);
			posyu -= 0.01f;/*
			print ("prepos: " + this.transform.localPosition);
			Vector3 p = new Vector3 (0, this.posyu, -1);
			p.z = -1;
			print ("Second: "+p);
			p = Camera.main.ScreenToWorldPoint (p);
			p.x = -1;
			transform.localPosition = p;*/
			transform.localPosition -= new Vector3 (0, 0.4f, 0);
			Vector3 p = transform.localPosition;
			if(transform.localPosition.y <= 0){

				//this.own.structManager.gameStart = false;
				//this.own.destroy ();
				//this.destroy ();
				StopCoroutine (moveAttack ());
				break;
			} 
			own.GetComponent<BoxCollider2D> ().offset = p;
		}
	}


}
