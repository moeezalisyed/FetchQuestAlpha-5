using UnityEngine;
using System.Collections;

public class LandPlots : MonoBehaviour {

	private GameManager owner;
	public int STRUCTTYPE=-1;
	private GameObject plotOwn;
	private TownStructure model = null;



	// Use this for initialization
	public void init (GameManager man, GameObject pO) {
		this.owner = man;
		this.plotOwn = pO;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	

	void OnMouseUp(){
		if (owner.isBuyingBuilding == true) {
			//print ("grats");
			this.STRUCTTYPE = this.owner.curStructType;
			print (plotOwn.transform.position);
            TownStructure tstru = plotOwn.GetComponent<TownStructure>();
            tstru.init(STRUCTTYPE, owner, owner.THEPLAYER);
            owner.TownStructureSet.Add(tstru);
			owner.isBuyingBuilding = false;
			//var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
			//model = modelObject.AddComponent<TownStructure>();						// Add a gemModel script to control visuals of the gem.
			//model.init(STRUCTTYPE, owner, owner.THEPLAYER);	
			//print (this.transform.position);
		}
	}

}