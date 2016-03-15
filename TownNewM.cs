using UnityEngine;
using System.Collections;

public class Town : MonoBehaviour {

	// Use this for initialization
	public GameManager manager;
	public int infrastructureLevel = 0;
	public int buildingTimer = 1;
	public int numBuildings = 0;
	public int moder = 1000;
	public int timer = 60;
	public int securityLevel = 100;



	public void init(GameManager m){
		this.manager = m;
		this.infrastructureLevel = 50;
		StartCoroutine (infraCheck ());

	}

	public void startMaintenanceTimer(){
		StartCoroutine (MaintenanceTimer ());
	}

	IEnumerator MaintenanceTimer(){
		while (this.timer > 0) {
			yield return new WaitForSeconds (1);
			timer--;
		}
		manager.gameStart = false;
		// This would be game over
		// Signal this to the game manager
	}

	public void startSecurityLevel(){
		StartCoroutine (securityLevelRoutine ());
	}

	IEnumerator securityLevelRoutine(){
		//manager.gdotPanel.SetActive (true);
		if (securityLevel <= 0) {
			this.manager.gameStart = false;
			this.manager.securityFail = true;
			StopCoroutine (securityLevelRoutine ());
			return true;
		
		}
		while (true) {
			yield return new WaitForSeconds (3);
			if (manager.defenceOn == false) {
			//	print (" get to d false");
				this.securityLevel--;

			} else if (manager.defenceOn == true && manager.ProtectionHeroesSet.Count != 0) {
				this.securityLevel += 2*manager.ProtectionHeroesSet.Count;

			} else if (manager.defenceOn == true) {
				// securityLevel retains constant
				securityLevel += 0;
				yield return new WaitForSeconds (6);
				securityLevel--;
			//	print (" get to d true");

			}
		
		
		
		}


	}


	
	IEnumerator infraCheck(){
	while(true){	
		yield return new WaitForSeconds (20);
		int numBuidings = this.manager.TownStructureSet.Count;
		if (numBuidings <= 0) {
			infrastructureLevel -= 3;
			
		} else {



			int x = ((buildingTimer / 1000) * numBuidings) % 20;
			infrastructureLevel += (x * 10) / infrastructureLevel;
		}
	}


	}


	void infrastructureCheck(){
		

		if (infrastructureLevel < 20) {
			StartCoroutine(criticalRoutine ());
		}



		

	}
	
	// Update is called once per frame
	void Update () {
		if (infrastructureLevel <= 0) {
			manager.gameStart = false;
		}
		buildingTimer++;

		/*if (buildingTimer % moder == 0) {
			//At this point, effect the infrastructure
			infrastructureCheck();

		}*/






	}

	IEnumerator criticalRoutine(){
		manager.criticalTime = true;
		yield return new WaitForSeconds (5);
		manager.criticalTime = false;
	}







}
