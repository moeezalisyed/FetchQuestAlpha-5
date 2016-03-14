﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hero : MonoBehaviour {

    private const int ROUGE = 0;
    private const int WARRIOR = 1;
    private const int CASTER = 2;
    private const int RANGER = 3;
    private const int HEALER = 4;
	private GameManager parentGameManager;
	public bool isProtector = false;
	public bool isOnQuest = false;

	//Feilds for class specific strengths and weaknesses:


    //The three hero fields we discussed for this class - the variable names should be relatively self explanitory
	public List<int> ToNextLevel = new List<int>(){ 80, 230, 470, 820};
	public int numWins;
	public int experienceLevel;
    //This is from the above category
	public int heroClass;
	public int XP;

    //Optional list of quests for a hero
    private List<Quest> quests;
	//List of quests completed by the hero:
	private List<Quest> questsCompletedByHero;

	//Fetch and grind times for heroes

	public int fetchTime;
	public int grindTime;
	public int deliverBonus;
	public string xname = "";

		public int i = 1;
	public int j = 1;

    // Here we initialize the above variables and require an integer representation of "class" to be
    //passed as a parameter





	public void init (int Class, GameManager m) {

        this.numWins = 0;
		this.parentGameManager = m;
       // this.numWins = 0;
	//	this.parentGameManager = m;
        this.experienceLevel = 1;
        this.heroClass = Class;
		this.quests = new List<Quest>();
		this.questsCompletedByHero = new List<Quest> ();
		this.setBonuses ();
		this.setXname();



	}

		void setXname(){
		

			switch (this.heroClass) 
			{
			case ROUGE:
				if (this.experienceLevel == 1) {
				this.xname = "ROGUE: Thief";
				} else {
				this.xname = "ROGUE: Assasins";
				}
			break;

			case WARRIOR:
				if (this.experienceLevel == 1) {
				this.xname = "WARRIOR: Paladin";
				} else {
				this.xname = "WARRIOR: Juggernaut";
				}
			break;
			case RANGER:
				
				if (this.experienceLevel == 1) {
				this.xname = "RANGER: Scout";
				} else {
				this.xname = "RANGER: Hunter";
				}
			break;
			
			}
		
	}

	void setBonuses(){
		switch (this.heroClass) 
		{
		case ROUGE:
			if (this.experienceLevel == 1) {
				//this.xname = "ROGUE: Thief: "+ this.fetchTime;
				this.fetchTime = -10;
				this.grindTime =10;
				this.deliverBonus = 10;


			} else {
				//this.xname = "ROGUE: Assasins: "+ this.fetchTime;
				this.fetchTime = -20;
				this.grindTime =10;
				this.deliverBonus = 20;

			}
			break;


		case WARRIOR:
			if (this.experienceLevel == 1) {
				//this.xname = "WARRIOS: Paladin: "+ this.fetchTime;
				this.fetchTime = 10;
				this.grindTime = -10;
				this.deliverBonus = 10;

			} else {
				//this.xname = "WARRIOS: Juggernaut: "+ this.fetchTime;
				this.fetchTime = 10;
				this.grindTime =-20;
				this.deliverBonus = 20;

			}
			break;
		case RANGER:

			if (this.experienceLevel == 1) {
				//this.xname = "RANGER: Scout: "+ this.fetchTime;
				this.fetchTime = -10;
				this.grindTime =10;
				this.deliverBonus = 10;

			} else {
				//this.xname = "RANGER: Hunter: "+ this.fetchTime;
				this.fetchTime = -20;
				this.grindTime =10;
				this.deliverBonus = 20;

			}
			break;

		}
	}
	
	// Update is called once per frame
	void Update () {
		levelUP ();
		this.numWins = questsCompletedByHero.Count;
	}


    //General getters/setters for the parameters
	public int addWin(Quest q){
		this.questsCompletedByHero.Add (q);
		//If the hero is below level four or has 14 wins they will level up.
		if (this.experienceLevel < 4 ||this.numWins == 14) {
			//this.levelUP ();

		}
		return this.questsCompletedByHero.Count;
	}

    public int getXP()
    {
		return this.numWins;
    }



    public void addQuest(Quest q)
    {
        this.quests.Add(q);
    }

    public int getType()
    {
        return this.heroClass;
    }

	//Level Up method for hero, more will be added
	void levelUP(){
		if (this.XP >= 820) {
			this.experienceLevel = 5;
		} else if (this.XP >= 470) {
			this.experienceLevel = 4;
		} else if (this.XP >= 230) {
			this.experienceLevel = 3;
		} else if (this.XP >= 80) {
			this.experienceLevel = 2;
		} else if (this.XP >= 0) {
			this.experienceLevel = 1;
		}

		this.setXname();
	}

	void checkMaxLevel(){
		if (this.experienceLevel > parentGameManager.THEPLAYER.maxLevel) {
			parentGameManager.THEPLAYER.maxLevel = this.experienceLevel;
			print ("i did update");
		}
	}

	void promoteHero(){
		switch (this.heroClass) 
		{
		case ROUGE:
			this.fetchTime -= 10;
			break;
		case WARRIOR:
			this.grindTime -= 10;
			break;
		}
	}


}
