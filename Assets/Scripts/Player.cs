﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
Player.cs
This script will hold player specific information: current gold, experience/influence points, etc. 
*/

public class Player : MonoBehaviour {

    //Player's gold - self explanitory
	public int Gold;
	public int maxLevel = 0;

    /*
    Integer represtentaion of experience points, we might change this to float
    but that depends on how we actually calculate this value.
    */
	public  int XP;

	/*Reference variable to Game Manager*/
	private GameManager m;


    /*
    This variable is for incrementalIncome, the amount of gold earned every "tick" of the game.
    It will be based on the current state of the town as we discussed in Mudd. 
    */
    private int incrementalIncome;

    /*
    List of hero objects - questees - that will be used to keep track of heroes currently under
    the player's employ.
    */

    private List<Hero> questees;
	public GameManager playerManager;


	/*
    Here we initialize the gold and XP to 0, and instantiate questees as an empty list of Hero objects.
    For gold and XP we talked about setting each to a random number such that gold+XP doesnt exceed a certain amount.
    We can either do that here or in another method, I'll leave it to your discretion.
    Also, incremental income here starts at 0, but we need to decide if we want to start the player out with
    some low number - more than 0 per "tick."
    */

	public void init (GameManager m) {
		playerManager = m;
		this.Gold = 100;
        this.XP = 100;

        this.incrementalIncome = 0;
        this.questees = new List<Hero>();
		StartCoroutine(passiveXP ());

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	IEnumerator passiveXP(){
		while (true) {
			yield return new WaitForSeconds (2);
			if (this.playerManager.gameStart == true) {
				this.XP += this.increaseXp ();
			}
		}

	}

    /*
    Straightforward addGold method increments gold by the parameter "toAdd". 
    */
    int addGold(int toAdd)
    {
        this.Gold += toAdd;
        return this.Gold;
    }

    //Same as above, but used to increase the gold gained "per tick." 



    public int addToIncome(int toAdd)
    
	{
        this.incrementalIncome += toAdd;
        return this.incrementalIncome;
    }





	public int increaseXp(){
		print ("herexp");


		/*foreach (Hero x in m.AvailableHeroesSet) {
			maxLevel = System.Math.Max (x.experienceLevel, maxLevel);
		}*/

		switch (maxLevel) {
		case 1:
			return 2;
			break;
		case 2:
			return 3;
			break;
		case 3:
			return 4;
			break;
		case 4:
			return 5;
			break;
		case 5:
			return 6;
			break;
		default:
			return 1;
			break;



		}


	}





}
