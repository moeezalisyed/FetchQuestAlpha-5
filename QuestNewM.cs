using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quest : MonoBehaviour {

    //Constant (immutable) variables representing time availability of this quest.



	// These are the possible QuestTypes:
    public const int LIMITEDAVAIL = 1;

    public const int ANYTIME = 0;
	public string dname = "";
	//Possibilities for questtypes end here

    //Same as above but for questCategory

	//These are questcategories
    public const int FETCH = 0;

    public const int GATHER =3;

    public const int DELIVER = 2;

    public const int GRIND = 1;

    public const int SPECIAL = 4;

	public string xname = "";
	public int whenAvailable = 0;
	public int isLimitedStarted = 0;
	//public int checker = 0;
	//Quest categories end here.


    //Previous and next quest in chain, used for linked list style implementation of quest chains

	public Quest previous;

	public Quest next;

    //Boolean value for whether or not this quest is the final in a chain

    public bool isFinalInChain;

    //Ints for quest time,

	public int goldReward;
	public int XPReward;
	public int HeroXPReward;
	public int timeReamining;


	public int QuestType;
	public int QuestCategory; 
	public int TimeNeeded; 
	public int reqLevel;
	public int reqXP;

    //List of integer represeentations of the required classes

	public List<int> reqClasses;
	public List<Hero> HeroesAssignedToThisQuest = new List<Hero>();

    private GameManager manager;

	/*
    Init for Quest object, takes in the numerical values listed in the design doc aas parameters
    to instantiate the above fields. Additionally, we pass in a list of two quests, representing previous and next in the chain.
    */
	public void init(GameManager man, int questType, int questCategory,int TimeNeeded, int reqLevel, int reqXP, List<int> reqClasses, List<Quest> previousAndNextList, int gReward) {
		this.goldReward = gReward;
		this.TimeNeeded = TimeNeeded;
        this.reqLevel = reqLevel;
        this.manager = man;
        this.QuestCategory = questCategory;
        this.reqXP = reqXP;
        this.reqClasses = reqClasses;
        this.QuestType = questType;
		timeReamining = -1;

		System.Random rnd = new System.Random();
	
		this.dname = "A " + this.manager.secondPhrase [this.manager.sindex] + this.manager.thirdPhrase [this.manager.tindex];
		this.manager.sindex ++;
		this.manager.sindex = this.manager.sindex % 7;
		this.manager.tindex -= 2;
		this.manager.tindex = this.manager.tindex % 7;
		this.manager.tindex = Mathf.Max (this.manager.tindex, -1 * this.manager.tindex);


        //If the quest is limited availability, we want to set its previous quest as specified by the parameter

		if (questCategory == 0) {
			this.xname = "FETCH:" + TimeNeeded + "sec," + goldReward + "Reward";
		} else if (questCategory == 1) {
			this.xname = "GRIND:" + TimeNeeded + "sec," + goldReward + "Reward";
		} else if (questCategory == 2) {
			this.xname = "DELIVER:" + TimeNeeded + "sec," + goldReward + "Reward";
		} else if (questCategory == 3) {
			this.xname = "GATHER:" + TimeNeeded + "sec," + goldReward + "Reward";
		} else if (questCategory == 4) {
			this.xname = "SPECIAL:" + TimeNeeded + " secs," + goldReward + "Reward";
		}

		if (questType == LIMITEDAVAIL)
        {
            this.previous = previousAndNextList[0];

            //Now, we can infer the isFinalInChain value by checking if the second value of
            //previousAndNextList is null. If it is, the value is true, false otherwise.

            if (previousAndNextList[1] == null)
            {
                this.isFinalInChain = true;
            }
            else
            {
                this.isFinalInChain = false;
            }
        }

		if (this.QuestType == 0) {
			man.AvailableQuestsSet.Add (this);
		} else {
		//	System.Random rnd = new System.Random();
			int x = rnd.Next (10000, manager.gameTimer);
			this.whenAvailable = x;
			this.isLimitedStarted = 0;
		}
    }


	public void beginQuest(List<Hero> UserGivenHeroesForThisQuest){
		StartCoroutine (idleTime (UserGivenHeroesForThisQuest ));	
	}




	public void beginQuest2(List<Hero> UserGivenHeroesForThisQuest){
		print ("x");

		//StartCoroutine (idleTime ());
		//HeroesAssignedToThisQuest.Clear();

		foreach (Hero x in UserGivenHeroesForThisQuest) {
			this.HeroesAssignedToThisQuest.Add (x);
		}
			



		foreach (Hero thisHero in this.HeroesAssignedToThisQuest) {
			thisHero.isOnQuest = true;
			if (thisHero.numWins == 0) {
				if (manager.THEPLAYER.Gold < 40) {
				}

				manager.THEPLAYER.Gold -= 40;
			}
			this.manager.AvailableQuestsSet.Remove (this);
			this.manager.AvailableHeroesSet.Remove (thisHero);

			if (this.QuestCategory == 0) {
				this.TimeNeeded += thisHero.fetchTime;
			} else if (this.QuestCategory == 1) {
				this.TimeNeeded += thisHero.grindTime;
			}
			else if (this.QuestCategory == 3 || this.QuestCategory == 2) {
				this.TimeNeeded += thisHero.deliverBonus;
			}
			//this.TimeNeeded += thisHero.fetchTime;
			//this.TimeNeeded += thisHero.grindTime;


		}


		timeReamining = TimeNeeded;

		//print ("quest Startedx:" +this.xname);

		StartCoroutine (questRoutine());


	}

	void Update(){
		if (this.QuestType == 1) {
			if (this.whenAvailable <= this.manager.gameTimer && this.isLimitedStarted == 0) {
				StartCoroutine ("newQuestActivated");
				manager.AvailableQuestsSet.Add (this);
				this.isLimitedStarted = 1;
			}
		}



	}



	//Want to call endQuest TimeNeeded seconds after beginQuest is called 

	IEnumerator newQuestActivated(){
		this.manager.NewQuestAvailablePanel.SetActive (true);
		yield return new WaitForSeconds (3);
		this.manager.NewQuestAvailablePanel.SetActive (false);
	}

	IEnumerator questRoutine(){
		this.timeReamining = TimeNeeded;
		while (timeReamining != 0) {
			yield return new WaitForSeconds (1);
			timeReamining--;	
		}


		this.endQuest();
	}

	IEnumerator idleTime(List<Hero> UserGivenHeroesForThisQuest){
		

			yield return new WaitForSeconds (0);

			beginQuest2 (UserGivenHeroesForThisQuest);





	}


	public void endQuest(){
		//print ("quest Ended");

		manager.THEPLAYER.Gold += this.goldReward;
		manager.THEPLAYER.XP += this.XPReward;
		foreach (Hero thisHero in this.HeroesAssignedToThisQuest) {
			
			thisHero.addWin (this);

			thisHero.XP += reqXP + reqXP / 2;
			thisHero.isOnQuest = false;
			manager.AvailableHeroesSet.Add (thisHero);

		}
		this.HeroesAssignedToThisQuest.Clear ();
		string z = "The Quest: " + this.xname + "ended - gave " + this.goldReward + " Gold ";
		this.manager.QuestJustEndedFunc (z);

		manager.QuestsInProgress.Remove (this);

		if (this.QuestType == ANYTIME) {
			this.reqXP = (int) Mathf.Floor (this.reqXP * 1.67f);
			this.goldReward = (int) Mathf.Floor (this.reqXP * 1.23f);
			this.TimeNeeded = (int) Mathf.Floor (this.reqXP * 1.23f);
			this.manager.sindex ++;
			this.manager.sindex = this.manager.sindex % 7;
			this.manager.tindex -= 2;
			this.manager.tindex = this.manager.tindex % 7;
			this.manager.tindex = Mathf.Max (this.manager.tindex, -1 * this.manager.tindex);
			this.dname = "A " + this.manager.secondPhrase [this.manager.sindex] + this.manager.thirdPhrase [this.manager.tindex];

			manager.AvailableQuestsSet.Add (this);
		}



	
	}


}
