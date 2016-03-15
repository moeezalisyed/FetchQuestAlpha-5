using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
	public GameObject HeroFolder;
	public GameObject PlayerFolder;
	public HashSet<Hero> HeroesSet;
	public HashSet<Quest> QuestsSet;
	public HashSet<Quest> AvailableQuestsSet;
	public HashSet<Hero> AvailableHeroesSet;
	public HashSet<Hero> ProtectionHeroesSet;
	public HashSet<Quest> QuestsInProgress;
	public HashSet<TownStructure> TownStructureSet;
	public Player THEPLAYER;
	public GameObject QuestFolder;
	public GameObject TownStructureFolder;
	public List<int> InitialGoldReqForTownStructures;
	public List<string> secondPhrase;
	public List<string> thirdPhrase;
	public Hero HeroToDisplay = null;
	public int NumberOfExactHeroes = 0;
	public LinkedList<Hero> haftq = null;
	bool selectionHeroes = false;
	bool updateSucc = false;
	bool updateFail =  false;
	public HashSet<GameObject> QuestScrollContainer = new HashSet<GameObject>();
	public HashSet<GameObject> HeroScrollContainer = new HashSet<GameObject>();
	public bool gameStart = false;
	public int gameTimer = 1200;
	public bool updateSuccBuyCheck = false;
	public bool updateFailBuyCheck = false;
	public bool NotEnoughForTannerybool = false;
	public bool NotEnoughXPForQuestbool = false;
	public bool criticalTime = false;
	public Town THETOWN = null;
	public string QuestJustEnded = "";
	public bool HeroSelectionInProgress = false;
	public bool HeroSelectionInProgressError = false;
	public bool NotEnoughGoldForNewHerobool = false;
	public bool infraspielbool = false;
	public bool mainspielbool = false;
	public bool maintimerstart  = false;
	public bool welcomeGUI = true;
	public int welcomeTimer = 30;
	public string buildingNEGU = "";
	public int goldToBuySecs = 10;
	public string NoRougeforWorkshopString = "";
	public string NotJuggernautForBlackSmithString = "";
	public string NotWarriorForApothecaryString = "";
	public string NotOracleForChurchString = "";
	public string HeroNotGoodEnoughString = "";
	public bool QuestConfirmOption = false;
	public Quest questToConfirm = null;
	public bool BuildingConfirmOption = false;
	public int buildingToConfirm = -1;
	public bool startGameClicked = false;
	public bool quitcheck = false;
	public bool defenceOn = false;
	public string defenceDisplay = "";
	public bool firstAttack = false;
	//public Text instruction = null;
	//System.Random rnd = new System.Random();

	public int sindex = 0;
	public int tindex = 6;
	public Quest curQuestCheck = null;
	public int whenToStartAttack = 1000;
	public bool isAttackStart = false;
    public bool isBuyingBuilding = false;
    public int curStructType;


    //Make a Bar
    public Vector2 pos = new Vector2(Screen.width - 100 ,Screen.width - 40);
	public Vector2 size = new Vector2(100,20);
	public Texture2D emptyTex;
	//public Texture2D fullTexG = Resources.Load("gdot");
	//public Texture2D fullTexR = Resources.Load("rdot");
	//public TextAsset imageAsset;
	//public Texture2D fullTex;
	//fullTex.LoadImage(imageAsset.bytes);
	//GetComponent<Renderer>().material.mainTexture = fullTex;
	/*fullTexG.LoadImage(gdot.bytes);
	GetComponent<Renderer>().material.mainTexture = fullTexG;
	fullTexR.LoadImage(imageAsset.bytes);
	GetComponent<Renderer>().material.mainTexture = fullTexR;*/


	public bool HeroDisplay = false;
	public List<Hero> UserSelectedHeroes = null;
	//[SerializeField] private Scrollbar Scrollbarvertical=null;
	//[SerializeField] private Scrollbar Scrollbarhorizontal=null;



	/*Buttons for Standard (init) Panel*/
	[SerializeField] private Button BuyButton = null;
	[SerializeField] private Button UpgradeButton = null;
	[SerializeField] private Button HeroesButton = null;
	[SerializeField] private Button QuestsButton = null;

	/*Buttons for Buy Panel*/
	[SerializeField] private Button BuyBuildingsButton = null;


	/*Buttons for Buy Buildings Panel*/
	[SerializeField] private Button BuyWorkshop = null;
	[SerializeField] private Button BuyBlacksmith = null;
	[SerializeField] private Button BuyApothecary = null;
	[SerializeField] private Button BuyTannery = null;
	[SerializeField] private Button BuyChurch = null;
	[SerializeField] private Button BuyMiscStructures = null;
	[SerializeField] private Button DestroyBuilding = null;

	//TODO
	//1. Create a function that all of the buy buildings button can feed into-- DONE
	//2. Create a dynamically sizing list of buttons with a scroll element --DONE
	//3. Fill the AvailableHeroesSet, add a 'name' component, 'class' component, and 'level component'
				/**********/

	/*initializing the various panels*/
	public GameObject initPanel = null;
	public GameObject BuyPanel = null;
	public GameObject BuyBuildingsPanel = null;
	public GameObject HeroPanel = null;
	public GameObject QuestPanel = null;
	public GameObject NewQuestAvailablePanel = null;
	public GameObject QuestGoldPanel = null;
	public GameObject QuestTimerPanel = null;
	public GameObject QuestXPNeededPanel = null;
	public GameObject QuestXPRewardPanel = null;
	public GameObject gdotPanel = null;
	public GameObject rdotPanel = null;


	public GameObject prefabButton;
	public RectTransform ScrollbarHeroes;
	public RectTransform ScrollbarQuests;


	/*Initializing the various Texts*/
	public Text UpgradeButtonActiveText=null;
	public Text BuildOnPlotText = null;

    public float speed = 2f;




	// Use this for initialization


	Player PlayerCharacter;

    public GameObject plotCollider1Obj;
    public GameObject plotCollider2Obj;
    public GameObject plotCollider3Obj;
    public GameObject plotCollider4Obj;
    public GameObject plotCollider5Obj;
    public GameObject plotCollider6Obj;

    public BoxCollider2D plotCollider1;
    public BoxCollider2D plotCollider2;
    public BoxCollider2D plotCollider3;
    public BoxCollider2D plotCollider4;
    public BoxCollider2D plotCollider5;
    public BoxCollider2D plotCollider6;

    public List<BoxCollider2D> plotCols;

    public List<GameObject> plotObjs;



    /*void Start(){
		UpgradeButtonActiveText.GetComponent <Text> (); //When the Upgrade button is clicked
		UpgradeButtonActiveText.gameObject.SetActive (false);
		BuildOnPlotText.GetComponent<Text> (); //When goldCheckandBuy is successful
		BuildOnPlotText.gameObject.SetActive(false);

		//-150 x, -30 y
		//UpgradeButtonActiveText.GetComponent<Text>().enabled = false;
		//UpgradeButtonActiveText.gameObject.SetActive (false);


		//Initial requirements for buying a TownStructure
		initializeInitialGoldReqForTownStructures ();






		initPanel = GameObject.Find("Standard Panel"); /*Initial panel*/
    /*	BuyPanel = GameObject.Find ("Buy Panel"); /*Buy Window*/
    /*	BuyPanel.SetActive (false);


		BuyBuildingsPanel = GameObject.Find ("List of Buildings Panel"); /*List of buildings to buy*/
    /*	initPanel.SetActive (false); //initpanel sets to true
		BuyBuildingsPanel.SetActive(false);
		//NewQuestAvailabe
		NewQuestAvailablePanel = GameObject.Find("NewQuestAvailable");


		StartCoroutine (Welcome ());
	}*/

    void Start () {
		gameStart = true;	
		whenToStartAttack = 1000;
		
		StartCoroutine (Welcome ());
		
		
		HeroFolder = new GameObject();  
		HeroFolder.name = "Heroes";		// The name of a game object is visible in the hHerarchy pane.
		QuestFolder = new GameObject();  
		QuestFolder.name = "Quests";		// The name of a game object is visible in the hHerarchy pane.
		PlayerFolder = new GameObject();  
		PlayerFolder.name = "Player";		// The name of a game object is visible in the hHerarchy pane.
		TownStructureFolder = new GameObject();  
		TownStructureFolder.name = "TownStructures";		// The name of a game object is visible in the hHerarchy pane.
		HeroesSet = new HashSet<Hero>();
		QuestsSet = new HashSet<Quest>();
		AvailableQuestsSet = new HashSet<Quest>();
		AvailableHeroesSet = new HashSet<Hero> ();
	ProtectionHeroesSet =  new HashSet<Hero> ();
		QuestsInProgress = new HashSet<Quest> ();
		TownStructureSet = new HashSet<TownStructure> ();
		THEPLAYER = null;
	initializeInitialGoldReqForTownStructures ();
		initialiseHeroes ();
		initialiseQuests ();
		initialisePlayer ();
		//gameStart = true;
		//gameTimer = 1200;
		initialiseTown ();

        initialisePlotColliders();

        //this.THETOWN.startMaintenanceTimer ();

        /*Hiding the appropriate text windows*/
        UpgradeButtonActiveText.GetComponent <Text> (); //When the Upgrade button is clicked
		UpgradeButtonActiveText.gameObject.SetActive (false);
		BuildOnPlotText.GetComponent<Text> (); //When goldCheckandBuy is successful
		BuildOnPlotText.gameObject.SetActive(false);

		//-150 x, -30 y
		//UpgradeButtonActiveText.GetComponent<Text>().enabled = false;
		//UpgradeButtonActiveText.gameObject.SetActive (false);


		//Initial requirements for buying a TownStructure
		initializeInitialGoldReqForTownStructures ();








		initPanel = GameObject.Find("Standard Panel"); /*Initial panel*/
		BuyPanel = GameObject.Find ("Buy Panel"); /*Buy Window*/
	QuestGoldPanel = GameObject.Find("QuestGoldPanel");
	QuestTimerPanel = GameObject.Find("QuestTimerPanel");
	QuestXPNeededPanel = GameObject.Find("QuestXPNeededPanel");
	QuestXPRewardPanel = GameObject.Find("QuestXPRewardPanel");

	gdotPanel = GameObject.Find("gdotPanel");
		gdotPanel.SetActive (false);

	gdotPanel.transform.localPosition +=  new Vector3 (00.0F,0,0);
	rdotPanel = GameObject.Find("rdotPanel");
	rdotPanel.transform.localPosition +=  new Vector3 (00.0F,0,0);

	rdotPanel.SetActive (false);
	rdotPanel.SetActive (false);


	QuestGoldPanel.SetActive (false);

	QuestTimerPanel.SetActive (false);
	QuestXPRewardPanel.SetActive (false);
	QuestXPNeededPanel.SetActive (false);
		
		BuyBuildingsPanel = GameObject.Find ("List of Buildings Panel"); /*List of buildings to buy*/
	initPanel.SetActive (false); //initpanel sets to true

		//NewQuestAvailabe
		//NewQuestAvailablePanel = GameObject.Find("NewQuestAvailable");



		/*All of the other panels set to false*/
		BuyPanel.SetActive (false);
		BuyBuildingsPanel.SetActive (true);
	//	NewQuestAvailablePanel.SetActive (false);

		/*Setting up the buttons*/

		/*Standard Panel Button Activation*/
		BuyButton.GetComponent<Button> ();
		BuyButton.onClick.AddListener(() => BuyBuildingsActive());
		/*
		UpgradeButton.GetComponent<Button> ();
		UpgradeButton.onClick.AddListener (() => UpgradeButtonActive ());
		HeroesButton.GetComponent<Button> ();
		HeroesButton.onClick.AddListener (() => HeroesButtonActive ());
		QuestsButton.GetComponent<Button> ();
		QuestsButton.onClick.AddListener (() => QuestsButtonActive ());
*/
/************/


		/*Buy Panel Button Activation*/
		BuyBuildingsButton.GetComponent<Button> ();
		BuyBuildingsButton.onClick.AddListener (() => BuyBuildingsActive ());


		/*Buy Buildings Button Activation*/
		BuyWorkshop.GetComponent<Button> ();
	BuyWorkshop.onClick.AddListener (() => BuyBuildingsBuffer (0));
		BuyBlacksmith.GetComponent<Button> ();
	BuyBlacksmith.onClick.AddListener (() => BuyBuildingsBuffer (1));
		BuyApothecary.GetComponent<Button> ();
	BuyApothecary.onClick.AddListener (() => BuyBuildingsBuffer (2));
		BuyTannery.GetComponent<Button> ();;
	BuyTannery.onClick.AddListener (() => BuyBuildingsBuffer (3));
		BuyChurch.GetComponent<Button> ();
	BuyChurch.onClick.AddListener (() => BuyBuildingsBuffer (4));
		BuyMiscStructures.GetComponent<Button> ();
	BuyMiscStructures.onClick.AddListener (() => BuyBuildingsBuffer (5));

		//DestroyBuilding.GetComponent<Button> ();



		//BuyButton.GetComponent<Button> ();
		//BuyButton.onClick.AddListener(() => BuyButtonActive());




	}
    void initialisePlotColliders()
    {
        plotCollider1Obj = GameObject.Find("Collider Plot 1");
        plotObjs.Add(plotCollider1Obj);
        plotCollider2Obj = GameObject.Find("Collider Plot 2");
        plotObjs.Add(plotCollider2Obj);
        plotCollider3Obj = GameObject.Find("Collider Plot 3");
        plotObjs.Add(plotCollider3Obj);
        plotCollider4Obj = GameObject.Find("Collider Plot 4");
        plotObjs.Add(plotCollider4Obj);
        plotCollider5Obj = GameObject.Find("Collider Plot 5");
        plotObjs.Add(plotCollider5Obj);
        plotCollider6Obj = GameObject.Find("Collider Plot 6");
        plotObjs.Add(plotCollider6Obj);

        foreach (GameObject go in plotObjs)
        {
            Rigidbody2D tempRig = go.AddComponent<Rigidbody2D>();
            LandPlots tempPlots = go.AddComponent<LandPlots>();
            TownStructure tstruct = go.AddComponent<TownStructure>();
            tempPlots.init(this, go);
            tempRig.isKinematic = false;
            tempRig.gravityScale = 0f;
        }

        plotCollider1 = plotCollider1Obj.GetComponent<BoxCollider2D>();
        plotCols.Add(plotCollider1);
        plotCollider2 = plotCollider2Obj.GetComponent<BoxCollider2D>();
        plotCols.Add(plotCollider2);
        plotCollider3 = plotCollider3Obj.GetComponent<BoxCollider2D>();
        plotCols.Add(plotCollider3);
        plotCollider4 = plotCollider4Obj.GetComponent<BoxCollider2D>();
        plotCols.Add(plotCollider4);
        plotCollider5 = plotCollider5Obj.GetComponent<BoxCollider2D>();
        plotCols.Add(plotCollider5);
        plotCollider6 = plotCollider6Obj.GetComponent<BoxCollider2D>();
        plotCols.Add(plotCollider6);

        foreach (BoxCollider2D boxy in plotCols)
        {
            boxy.isTrigger = true;
        }


    }

    IEnumerator Welcome(){
	//	print ("This coroutine");
		welcomeTimer = 30;
		while (this.welcomeTimer > 0) {
			yield return new WaitForSeconds (1);
			welcomeTimer--;
	}
		StartCoroutine (startTimer ());
		StartCoroutine (HeroPanelDisplay ());
		StartCoroutine (QuestPanelDisplay ());
		//this.welcomeGUI = false;
		//Start2 ();
	}

	IEnumerator startTimer(){
	//	print ("This coroutine");
	gameTimer = 1200;
	while (this.gameTimer > 0) {
		yield return new WaitForSeconds (1);
		gameTimer--;
	}
		gameStart = false;
	//this.welcomeGUI = false;
	//Start2 ();
}


IEnumerator defenceOnR(){
	defenceDisplay = "You just turned on your defences \n This will give certain protection against attacks \n Defence consumes one gold per 3 seconds";
	yield return new WaitForSeconds (3);
		defenceDisplay = "";
		while (true) {
			if (this.defenceOn == false) {
				break; 
			}
			this.THEPLAYER.Gold -= 1;
			yield return new WaitForSeconds (3);
		}
}

IEnumerator defenceOff(){
	StopCoroutine (defenceOnR ());
	defenceDisplay = "You just turned off your defences \n You will be more vulnerable to attack";
	yield return new WaitForSeconds (3);
	defenceDisplay = "";

}



IEnumerator HeroPanelDisplay(){
		while (gameStart) {
		this.HeroBackButtonClicked ();
		this.HeroesButtonActive();

			yield return new WaitForSeconds (1);
		this.HeroBackButtonClicked ();
		this.HeroesButtonActive();

		}
		
		HeroPanel.SetActive (false);
}


IEnumerator QuestPanelDisplay(){
	while (gameStart) {
		this.QuestBackButtonClicked ();
		this.QuestsButtonActive();

		yield return new WaitForSeconds (1);
		this.QuestBackButtonClicked ();
		this.QuestsButtonActive();

	}

	HeroPanel.SetActive (false);
}

	IEnumerator AttackRoutine(){
		THETOWN.startSecurityLevel();
		int preSL = THETOWN.securityLevel;
		while (true) {
			yield return new WaitForSeconds (0);
			/*if (rdotPanel.activeSelf == true) {
			float diff = (preSL - THETOWN.securityLevel)/100.0f;
			preSL = THETOWN.securityLevel;
				rdotPanel.transform.localScale -= new Vector3 (diff, 0, 0);
			} else if (gdotPanel.activeSelf == true) {
			float diff = (preSL - THETOWN.securityLevel)/100.0f;
				preSL = THETOWN.securityLevel;
			gdotPanel.transform.localScale -= new Vector3 (diff, 0, 0);
			}
*/
		}

	}
  


    // Update is called once per frame
    void Update () {
        //QuestPanel.SetActive (true);
        //instruction.text = "90 ";

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }



        gdotPanel.SetActive (false);
	rdotPanel.SetActive (false);
		if (gameStart) {
			if (gameTimer == whenToStartAttack && isAttackStart == false) {
				isAttackStart = true;
				StartCoroutine (AttackRoutine ());
				print ("did start");
				//buyTownStructure (5);
				firstAttack = true;
			}
		}

		if (!gameStart) {
			initPanel.SetActive (false);
			QuestPanel.SetActive (false);
			HeroPanel.SetActive (false);
			BuyBuildingsPanel.SetActive (false);
			if (THETOWN.timer > 0) {
				quitcheck = true;
				
			}

			//THETOWN.timer = -1;
			
		}
		if (gameTimer <= 0) {
			gameStart = false;
		}

		if (QuestsInProgress.Count > 0 && this.maintimerstart == false) {
			this.THETOWN.startMaintenanceTimer ();	
			maintimerstart = true;
		}

		if (welcomeTimer > 0) {
			//welcomeTimer--;
		THEPLAYER.XP = 100;
			THEPLAYER.Gold = 1000;
		THETOWN.infrastructureLevel = 50;
		} else {
			welcomeGUI = false;	
			
		}


	switch (this.TownStructureSet.Count) {
	//0buildings
		case 0: 
		
			goldToBuySecs = 10;
			break;
		//1buildings
		case 1:
			goldToBuySecs = 12;
		break;
	case 2:
		goldToBuySecs = 14;

		break;
	case 3:
		goldToBuySecs = 16;

		break;
	case 4:
		goldToBuySecs = 18;

		break;
	case 5:
		goldToBuySecs = 18;

		break;
	case 6:
		goldToBuySecs = 20;

		break;
	default:
		goldToBuySecs = 25;

		break;

	}



	}

	// This is ot figure out how much gold for the initial buy of a townStucuture
	public void initializeInitialGoldReqForTownStructures(){
		InitialGoldReqForTownStructures = new List<int>();
		InitialGoldReqForTownStructures.Add (300);
		InitialGoldReqForTownStructures.Add (500);
		InitialGoldReqForTownStructures.Add (200);
		InitialGoldReqForTownStructures.Add (450);
		InitialGoldReqForTownStructures.Add (1000);

		secondPhrase.Add ("Handmaiden's ");
	secondPhrase.Add ("Dragon's ");
	secondPhrase.Add ("Fool's ");
	secondPhrase.Add ("King's ");
	secondPhrase.Add ("Queen's ");
	secondPhrase.Add ("Hunter's ");
	secondPhrase.Add ("Fox's ");
		
	thirdPhrase.Add ("Trail");
	thirdPhrase.Add ("Challenge");
	thirdPhrase.Add ("Errand");
	thirdPhrase.Add ("Request");
	thirdPhrase.Add ("Beckoning");
	thirdPhrase.Add ("Awakening");
	thirdPhrase.Add ("Quest");
		

	}






	void initialiseHeroes(){
		
		initialiseHero (0);
		initialiseHero (0);
		initialiseHero (1);
		initialiseHero (3);
		initialiseHero (1);
		initialiseHero (3);
	initialiseHero (0);
	initialiseHero (0);



	}

	void initialiseTown(){
		GameObject townObject = new GameObject ();			// Create a new empty game object that will hold a hero.
		Town theTown = townObject.AddComponent<Town> ();			// Add the hero.cs script to the object.
		// We can now refer to the object via this script.
		//theTown.transform.parent = TownStructureFolder.transform;
		//HeroFolder.Add (curHero);
		theTown.init(this );							// Initialize the hero script.
		theTown.name = "Town 1" ;						// Give the gem object a name in the Hierarchy pane.
		THETOWN = theTown;
		//print("initialised" + curHero.name);
	}

	void initialisePlayer(){
		GameObject playerObject = new GameObject ();			// Create a new empty game object that will hold a hero.
		Player thePlayer = playerObject.AddComponent<Player> ();			// Add the hero.cs script to the object.
		// We can now refer to the object via this script.
		thePlayer.transform.parent = PlayerFolder.transform;
		//HeroFolder.Add (curHero);
		thePlayer.init(this );							// Initialize the hero script.
		thePlayer.name = "Player 1" ;						// Give the gem object a name in the Hierarchy pane.
		THEPLAYER = thePlayer;
		//print("initialised" + curHero.name);
	}




	void initialiseHero(int heroClass){
		
		GameObject heroObject = new GameObject ();			// Create a new empty game object that will hold a hero.
		Hero curHero = heroObject.AddComponent<Hero> ();			// Add the hero.cs script to the object.
		// We can now refer to the object via this script.
		curHero.transform.parent = HeroFolder.transform;
		//HeroFolder.Add (curHero);



		//heroRig;


		curHero.init(heroClass, this);							// Initialize the hero script.
        curHero.XP = 81;
		curHero.name = "Hero "+ HeroesSet.Count;						// Give the gem object a name in the Hierarchy pane.
		HeroesSet.Add(curHero);
		AvailableHeroesSet.Add (curHero);
		//print("initialised" + curHero.name);
	}

	void initialiseQuests(){
		int TimeNeeded = 80;
		int reqLevel = 1;
		int questCategory = 0;
		int reqXP = 1;
		List<int> reqClasses = null;
		List<Quest> previousAndNextList = null;
		int questType = 0;

		initialiseQuest (this, 0, 0, 80, 1, 40, new List<int> {-1}, null, 80);
		initialiseQuest (this, 0, 1, 100, 2, 50, new List<int> {-1}, null, 100);
		initialiseQuest(this, 0, 0 , 120, 3, 60, new List<int> {-1}, null, 140); 
		initialiseQuest(this, 0, 2 , 140, 4, 70, new List<int> {1 , 1}, null, 165);
		initialiseQuest(this, 0, 1 , 40, 1, 40, new List<int> {-1}, null, 130);
		initialiseQuest(this, 0, 3 , 560, 2, 800, new List<int> {1}, null, 1000);
		initialiseQuest(this, 0, 1 , 60, 3, 60, new List<int> {-1}, null, 160);
		initialiseQuest(this, 0, 1 , 70, 4, 70, new List<int> {-1}, null, 175);
		initialiseQuest(this, 0, 2 , 80, 1, 40, new List<int> {-1}, null, 180);
		initialiseQuest(this, 0, 2 , 100, 2, 50, new List<int> {-1}, null, 210);
		initialiseQuest(this, 0, 2 , 120, 3, 60, new List<int> {-1}, null, 240);
		/*initialiseQuest(this, 1, 2 , 140, 4, 70, new List<int> {-1}, null, 270);
		initialiseQuest(this, 1, 4 , 240, 3, 160, new List<int> {0, 1}, null, 100);
		initialiseQuest(this, 1, 4 , 240, 3, 160, new List<int> {0, -1}, null, 100);
		initialiseQuest(this, 1, 4 , 560, 4, 800, new List<int> {0, 1, -1}, null, 1000);*/



	}



	void initialiseQuest(GameManager man, int questType, int questCategory, int TimeNeeded, int reqLevel, int reqXP, List<int> reqClasses, List<Quest> previousAndNextList, int gReward
	) {

			GameObject questObject = new GameObject ();			// Create a new empty game object that will hold a hero.
			Quest curQuest = questObject.AddComponent<Quest> ();			// Add the hero.cs script to the object.
			// We can now refer to the object via this script.
			curQuest.transform.parent = QuestFolder.transform;

		curQuest.init(this, questType, questCategory, TimeNeeded, reqLevel, reqXP, reqClasses, previousAndNextList, gReward);							// Initialize the hero script.

			curQuest.name = "Quest "+ QuestsSet.Count;						// Give the gem object a name in the Hierarchy pane.
			QuestsSet.Add(curQuest);
			
			
		}


	//***********
	//***********
	// This function will check if the user has enough gold to buy the townstructure that was selected through GUI
	//***********
	//***********
	public void goldCheckAndBuy(int TownStructureType){
		// Since we havent initialised this townStrucuture - we need to be able to check for its required Gold
		// For this Im thinking of adding a list indexed by townStructureType and their initial required gold
		int goldRequired = InitialGoldReqForTownStructures[TownStructureType];
		if (THEPLAYER.Gold >= goldRequired) {
			if (TownStructureType == 3) {
				if (TownStructureSet.Count >= 1) {
                    //buyTownStructure (TownStructureType);
                    curStructType = TownStructureType;
                    this.isBuyingBuilding = true;
                    THEPLAYER.Gold -= goldRequired;
					BuyBuildingsPanel.SetActive (false);
					//print("Successful");
					//BuildOnPlotText.gameObject.SetActive (true);
					initPanel.SetActive (true);
					//buyTownStructure (TownStructureType);
					StartCoroutine (updateSucccBuyCheck ());
				} else {
					StartCoroutine (NotEnoughForTannery ());
				} 
		} else if (TownStructureType == 0){
			bool canTrue = false;
				foreach (Hero p in HeroesSet) {
					if (p.heroClass == 0 && p.experienceLevel > 1) {
						canTrue = true;
						break;
					}
				}

				if (canTrue) {
                    //buyTownStructure (TownStructureType);
                    curStructType = TownStructureType;
                    this.isBuyingBuilding = true;
                    THEPLAYER.Gold -= goldRequired;
				BuyBuildingsPanel.SetActive (false);
				//print("Successful");
				//BuildOnPlotText.gameObject.SetActive (true);
				initPanel.SetActive (true);
				//buyTownStructure (TownStructureType);
				StartCoroutine (updateSucccBuyCheck ());
				} else {
					StartCoroutine (NoRougeforWorkshop ());
				}

		} else if (TownStructureType == 2){
			bool canTrue = false;
			foreach (Hero p in HeroesSet) {
				if (p.heroClass == 1 && p.experienceLevel >= 1) {
					canTrue = true;
					break;
				}
			}

			if (canTrue) {
                    //buyTownStructure (TownStructureType);
                    curStructType = TownStructureType;
                    this.isBuyingBuilding = true;
                    THEPLAYER.Gold -= goldRequired;
				BuyBuildingsPanel.SetActive (false);
				//print("Successful");
				//BuildOnPlotText.gameObject.SetActive (true);
				initPanel.SetActive (true);
				//buyTownStructure (TownStructureType);
				StartCoroutine (updateSucccBuyCheck ());
			} else {
				StartCoroutine (NotWarriorForApothecary ());
			}

		} else if (TownStructureType == 1){
			bool canTrue = false;
			foreach (Hero p in HeroesSet) {
				if (p.heroClass == 1 && p.experienceLevel > 1) {
					canTrue = true;
					break;
				}
			}

			if (canTrue) {
                    //buyTownStructure (TownStructureType);
                    curStructType = TownStructureType;
                    this.isBuyingBuilding = true;
                    THEPLAYER.Gold -= goldRequired;
				BuyBuildingsPanel.SetActive (false);
				//print("Successful");
				//BuildOnPlotText.gameObject.SetActive (true);
				initPanel.SetActive (true);
				//buyTownStructure (TownStructureType);
				StartCoroutine (updateSucccBuyCheck ());
			} else {
				StartCoroutine (NotJuggernautForBlackSmith ());
			}

		}

		else if (TownStructureType == 4){
			bool canTrue = false;
			foreach (Hero p in HeroesSet) {
				if (p.heroClass == 4 && p.experienceLevel > 1) {
					canTrue = true;
					break;
				}
			}

			if (canTrue) {
                    curStructType = TownStructureType;
                    this.isBuyingBuilding = true;
                    //buyTownStructure (TownStructureType);
                    THEPLAYER.Gold -= goldRequired;
				BuyBuildingsPanel.SetActive (false);
				//print("Successful");
				//BuildOnPlotText.gameObject.SetActive (true);
				initPanel.SetActive (true);
				//buyTownStructure (TownStructureType);
				StartCoroutine (updateSucccBuyCheck ());
			} else {
				StartCoroutine (NotOracleForChurch ());
			}

		} 
			else {

                curStructType = TownStructureType;
                this.isBuyingBuilding = true;
                //buyTownStructure (TownStructureType);
                THEPLAYER.Gold -= goldRequired;
				BuyBuildingsPanel.SetActive (false);
				//print("Successful");
				//BuildOnPlotText.gameObject.SetActive (true);
				initPanel.SetActive (true);
				//buyTownStructure (TownStructureType);
				StartCoroutine (updateSucccBuyCheck ());
			}

		} else {
			// Not enough gold to but the required townStrucuture
			//***********
			// Open a dialog box and tell it to the user. 
			//***********
		this.buildingNEGU = "You need " + InitialGoldReqForTownStructures[TownStructureType] + " Gold to buy this Building!";
			StartCoroutine (updateFailureBuyCheck());
			//print("Not enough gold");
		}
	}

	public void QuestJustEndedFunc(string x){
		this.QuestJustEnded = x;		
		StartCoroutine (QuestJustEndedRoutine());
	}

	IEnumerator QuestJustEndedRoutine(){
		yield return new WaitForSeconds (3);
		QuestJustEnded = "";
	}


	IEnumerator NotEnoughForTannery(){
		NotEnoughForTannerybool = true;
		yield return new WaitForSeconds (3);
		NotEnoughForTannerybool = false;

	}

IEnumerator NoRougeforWorkshop(){
	NoRougeforWorkshopString = "Workshop requires an Assassin";
	yield return new WaitForSeconds (3);
	NoRougeforWorkshopString = "";

}

IEnumerator NotJuggernautForBlackSmith(){
	NotJuggernautForBlackSmithString = "Blacksmith requires a Caster";
	yield return new WaitForSeconds (3);
	NotJuggernautForBlackSmithString = "";

}

IEnumerator NotWarriorForApothecary(){
	NotWarriorForApothecaryString = "Apothecary requires a Juggernaut";
	yield return new WaitForSeconds (3);
	NotWarriorForApothecaryString = "";


}

IEnumerator NotOracleForChurch(){
	NotOracleForChurchString = "Church requires an Oracle";
	yield return new WaitForSeconds (3);
	NotOracleForChurchString = "";
}

	





	IEnumerator updateSucccBuyCheck(){
        updateSuccBuyCheck = true;
        yield return new WaitForSeconds(3);
        updateSuccBuyCheck = false;
        this.curStructType = -1;

    }

	IEnumerator updateFailureBuyCheck(){
		
		updateFailBuyCheck =  true;
		yield return new WaitForSeconds(3);
		updateFailBuyCheck = false;
		this.buildingNEGU = "";
	}




    public void buyTownStructure(int TownStructureType, Vector3 pos)
    {
        GameObject townStructureObject = new GameObject();
        TownStructure newTownStructure = townStructureObject.AddComponent<TownStructure>();
        newTownStructure.transform.parent = TownStructureFolder.transform;
        Vector3 positionOnMap = pos;
        //		pos.z = -1;
        newTownStructure.transform.position = positionOnMap;

        BoxCollider2D townBox = townStructureObject.AddComponent<BoxCollider2D>();
        Rigidbody2D townRig = townStructureObject.AddComponent<Rigidbody2D>();
        print("made it this far");
        townBox.isTrigger = true;
        townRig.gravityScale = 0f;
        townRig.isKinematic = true;
        newTownStructure.init(TownStructureType, this, THEPLAYER);

        newTownStructure.name = "TownStructure " + TownStructureSet.Count;
        //TownStructureSet.Add(newTownStructure);

    }

    // We get the TownStructure from the GUI and Colliders

    public void goldCheckAndUpdate(TownStructure toCheck){
		int goldReq = toCheck.goldReq;
		if (THEPLAYER.Gold > goldReq + 15) {
			updateTownStructure (toCheck);
			StartCoroutine (updateSuccc());
		} else {
			// Gold is not enough to update - print error
			// We would probably need to open up a dialog box here as well
			StartCoroutine (updateFailure());
			print("Not enough gold to upgrade");
		}
	}

	IEnumerator updateSuccc(){
		updateSucc = true;
		yield return new WaitForSeconds (3);
		updateSucc = false;
	}

	IEnumerator updateFailure(){
		updateFail =  true;
		yield return new WaitForSeconds(3);
		updateFail = false;
	}


	void updateTownStructure(TownStructure toUpdate){
		toUpdate.updatelevel ();
	}



	// THIS SECTION (STARTING HERE) WORKS WITH CHECKING AND ASSIGNING QUESTS


	IEnumerator NotEnoughXPForQuest(){
		NotEnoughXPForQuestbool =  true;
		yield return new WaitForSeconds(3);
		NotEnoughXPForQuestbool = false;
	}
	//Will let GUI Function get the Quest that we have to work with
	public void questAssignCheck(Quest x){
		
		if (THEPLAYER.XP < x.reqXP) {
			StartCoroutine (NotEnoughXPForQuest ());
			// At this point, we cannot carry out this quest
			//print("this quest is not available due to lack of user XP);
			//Opena  GUI button and show this to the user.
		} else {
		// At this point we can check other things for the Quest:
			LinkedList<Hero> HeroesAvailableForThisQuest = new LinkedList<Hero>();
			// We find all the heroes available for teh Quet
			foreach (Hero nextAvailable in AvailableHeroesSet) {
				
				if (x.reqClasses == null) {
					HeroesAvailableForThisQuest.AddLast (nextAvailable);
				} else if(x.reqClasses.Count == 1 && x.reqClasses[0] == -1){
					HeroesAvailableForThisQuest.AddLast (nextAvailable);

				}else {


					foreach (int classNeeded in x.reqClasses) {
						if (classNeeded == nextAvailable.heroClass) {
							HeroesAvailableForThisQuest.AddLast (nextAvailable);
						}
					}
				}
			}

			// Create Buttons for all of these Heroes

			// Now we have the heroes available for this Quest: 
			// At this point, we give the user the ability to select heroes for this quest
			// Here a GUI Button will be needed
			// I have a function getUserSelectedHeroesForThisQuest(List<Hero> AvailableHeroesForThisQuest, int NumberOfExactHeroes)
			// this function returns a list of heroes the user has seleceted for this quest;

			HeroSelectionInProgress = true;

			//************* This is the function //*************
			NumberOfExactHeroes = x.reqClasses.Count;
			UserSelectedHeroes.Clear();
			if (HeroesAvailableForThisQuest.Count == 0) {
				StartCoroutine (HeroNotGoodEnough ());
			} else {
				curQuestCheck = x;
				createButtons (HeroesAvailableForThisQuest);
				
				//UserSelectedHeroes = getUserSelectedHeroesForThisQuest(List<Hero> AvailableHeroesForThisQuest, int NumberOfExactHeroes);

				//*************
			StartCoroutine(RoutineToAssignQuest(x));


				

			}

		
		}
	}
	void createButtons(LinkedList<Hero> haftq ){
		this.haftq = haftq;
		if (haftq.Count == 0) {
			StartCoroutine (HeroNotGoodEnough ());
		} else {
			selectionHeroes = true;
		}



	}

IEnumerator RoutineToAssignQuest(Quest x){
		if (this.UserSelectedHeroes.Count != x.reqClasses.Count) {
			yield return new WaitForSeconds (1);
			StartCoroutine (RoutineToAssignQuest (x));
		} else { 
			curQuestCheck = null;
			assignQuest (x, UserSelectedHeroes);
		}

}




IEnumerator HeroNotGoodEnough(){
		HeroNotGoodEnoughString =  "No available heroes are capable to finishing this Quest!";
			yield return new WaitForSeconds(3);
		HeroNotGoodEnoughString = "";
	}



	public void assignQuest(Quest x, List<Hero> UserSelectedHeroes){
		//Remove this quest from available quests
		AvailableQuestsSet.Remove (x);

		//Add this to ongoing quests
		QuestsInProgress.Add(x);

		//Remove the heroes selected from this quest from available heroes
		foreach (Hero thisHero in UserSelectedHeroes){
			AvailableHeroesSet.Remove(thisHero);
		}

		//Signal to Quest x to begin itself;
		x.beginQuest(UserSelectedHeroes);


	}


	void BuyButtonActive(){

		/*Set the functionality for all the buy buttons*/
		initPanel.SetActive (false);
		BuyPanel.SetActive (true);

	}
	void UpgradeButtonActive(){
		initPanel.SetActive(false);
		UpgradeButtonActiveText.gameObject.SetActive (true);
		//print ("Yeah");
		//UpgradeButtonActiveText.fontSize = 22;
	}
	void HeroesButtonActive(){
		//initPanel.SetActive (false);
		HeroPanel.SetActive (true);
		//Scrollbarhorizontal.gameObject.SetActive (false);
		//	for(int i = 1; i <= AvailableHeroesSet.Count; i++)
		int x = HeroesSet.Count;
		ScrollbarHeroes.sizeDelta = new Vector2 (ScrollbarQuests.sizeDelta.x, 30*(x+2));
		foreach (Hero curHero in HeroesSet)
		{

			//print ("Reached");

			GameObject goButton = (GameObject)Instantiate (prefabButton);
			goButton.transform.SetParent (ScrollbarHeroes, false);
			goButton.transform.localScale = new Vector3 (1, 1, 1);

			Button tempButton = goButton.GetComponent<Button> ();
			string status	= "";
			if (curHero.isProtector == true) {
				tempButton.GetComponentInChildren<Text> ().color = Color.green;
				status += " (P)";
			} else if (curHero.isOnQuest == true) {
					status += " (Q)";
				tempButton.GetComponentInChildren<Text> ().color = Color.red;
			} else {
				status += " (A)";
				tempButton.GetComponentInChildren<Text> ().color = Color.black;
			}


			
		tempButton.GetComponentInChildren<Text> ().text = curHero.xname + status;
			//heroes.name+"   " + heroes.heroClass + "    Lv:" heroes.experienceLevel; 
			/**Note: We need a format script so that the name and level and class are all aligned in the list **/
			//	int tempInt = i;
			HeroScrollContainer.Add(goButton);
			Hero heroToAdd = curHero;
				tempButton.onClick.AddListener (() => HeroButtonClicked (heroToAdd));


		}

		GameObject HeroBackButton = (GameObject)Instantiate (prefabButton);
		HeroBackButton.transform.SetParent (ScrollbarHeroes, false);
		HeroBackButton.transform.localScale = new Vector3 (1, 1, 1);
		Button tempButtonx = HeroBackButton.GetComponent<Button> ();
		tempButtonx.GetComponentInChildren<Text> ().text = "Refresh";
		tempButtonx.onClick.AddListener (() => HeroBackButtonClicked ());
		HeroScrollContainer.Add(HeroBackButton);

	}

	void HeroBackButtonClicked(){
		//Need to delete all buttons from the scroll bar
		initPanel.SetActive (true);
		HeroPanel.SetActive (false);
		foreach (GameObject x in HeroScrollContainer) {
			Destroy (x);
		}
		//HeroesButtonActive ();


		//Deleting all previous buttons

	}

	void HeroButtonClicked(Hero curHero){
		// We have a hero here. 
		//Display its Properties?
		HeroDisplay = true;
		HeroToDisplay = curHero;


	}

	void ButtonClicked(int buttonNo)
	{
			Debug.Log ("Button clicked = " + buttonNo);
	}
		
	
	void QuestsButtonActive(){
		//initPanel.SetActive (false);
		QuestPanel.SetActive (true);
		int x = AvailableQuestsSet.Count;
		
		ScrollbarQuests.sizeDelta = new Vector2 (ScrollbarQuests.sizeDelta.x, 30*(x+4));
		
		foreach (Quest curQuest in AvailableQuestsSet)
			
		{

			

			GameObject goButton = (GameObject)Instantiate (prefabButton);
			
		goButton.transform.SetParent (ScrollbarQuests, false);
			goButton.transform.localScale = new Vector3 (1, 1, 1);

			Button tempButton = goButton.GetComponent<Button> ();
			string timeforthis = "";
			timeforthis = " (" + curQuest.TimeNeeded + " secs)";
			tempButton.GetComponentInChildren<Text> ().fontSize = 20; 
			tempButton.GetComponentInChildren<Text> ().text = curQuest.dname + timeforthis;
			//heroes.name+"   " + heroes.heroClass + "    Lv:" heroes.experienceLevel; 
			/**Note: We need a format script so that the name and level and class are all aligned in the list **/

			QuestScrollContainer.Add (goButton);
			Quest questToClick = curQuest;
			tempButton.onClick.AddListener (() => QuestButtonClickedBuffer (questToClick));


		}
		GameObject QuestBackButton = (GameObject)Instantiate (prefabButton);
		QuestBackButton.transform.SetParent (ScrollbarQuests, false);
		QuestBackButton.transform.localScale = new Vector3 (1, 1, 1);
		Button tempButtonx = QuestBackButton.GetComponent<Button> ();
		tempButtonx.GetComponentInChildren<Text> ().text = "Refresh";
		tempButtonx.onClick.AddListener (() => QuestBackButtonClicked ());

		QuestScrollContainer.Add (QuestBackButton);

	}

	void QuestBackButtonClicked(){
		//Need to delete all buttons from the scroll bar
		initPanel.SetActive (true);
		QuestPanel.SetActive (false);
		foreach (GameObject x in QuestScrollContainer) {
			Destroy (x);
		}
		//QuestsButtonActive ();
	}

	void QuestButtonClickedBuffer(Quest x){
		
		if (gameStart == true) {
			QuestConfirmOption = true;
			questToConfirm = x;
		}
	}



	

	void QuestButtonClicked(Quest quest){
		if (HeroSelectionInProgress == false) {
			print ("Quest Button Clicked" + quest.xname);
			questAssignCheck (quest);
		} else {
			StartCoroutine (HeroSelectionInProgressErrorC ());
		}
		QuestBackButtonClicked ();






	}

	IEnumerator HeroSelectionInProgressErrorC(){
		HeroSelectionInProgressError =  true;
		yield return new WaitForSeconds(3);
		HeroSelectionInProgressError = false;
	}






	void BuyBuildingsActive()
	{
		BuyPanel.SetActive (false);
		BuyBuildingsPanel.SetActive (true);
	}

/*
IEnumerator BuyBuildingsBufferR(int x){
		BuyBuildingsBuffer (x);
}
*/
void BuyBuildingsBuffer(int x){
		print ("to the buffer functio");
		if (gameStart == true) {
			this.BuildingConfirmOption = true;
			this.buildingToConfirm = x;
		}
}


	void BuyBuildings(int BuildingType)

	{
		print ("Reached Buy Buildings Function");
		goldCheckAndBuy (BuildingType);

	}

	void addMaintenance(int goldSpent){
	this.THEPLAYER.Gold -= goldSpent;
		if (this.THETOWN.timer + 25 >= 60) {
			this.THETOWN.timer = 60;
		} else {
			this.THETOWN.timer += 25;
		}
	}

	void spendGoldToAddMaintenance(){

		if(this.TownStructureSet.Count > 6 ){
			addMaintenance(25);
			return;
		}

		switch (this.TownStructureSet.Count) {
		//0buildings
		case 0: 
			if (THEPLAYER.Gold > 10) {
				addMaintenance (10);
			}
			break;
			//1buildings
		case 1:
		if (THEPLAYER.Gold > 12) 
			addMaintenance(12);
			break;
		case 2:
		if (THEPLAYER.Gold > 14)
			addMaintenance(14);
			break;
		case 3:
		if (THEPLAYER.Gold > 16)
			addMaintenance(16);
			break;
		case 4:
		if (THEPLAYER.Gold > 18)
			addMaintenance(18);
			break;
		case 5:
		if (THEPLAYER.Gold > 18)
			addMaintenance(18);
			break;
		case 6:
		if (THEPLAYER.Gold > 20)
			addMaintenance(20);
			break;
		default:
		if (THEPLAYER.Gold > 25)
			addMaintenance(25);
			break;

		}

	
	
	}

	IEnumerator NotEnoughGoldForNewHero(){
		NotEnoughGoldForNewHerobool =  true;
		yield return new WaitForSeconds(3);
		NotEnoughGoldForNewHerobool = false;
	}

	IEnumerator infraspiel(){
		infraspielbool =  true;
		yield return new WaitForSeconds(3);
		infraspielbool = false;
	}

	IEnumerator mainspiel(){
		mainspielbool =  true;
		yield return new WaitForSeconds(3);
		mainspielbool = false;
	}



	void OnGUI () {
		GUI.color = Color.white;

		if (gameStart) {
			if (THEPLAYER.Gold <= 30) {
				GUI.color = Color.red;
				GUI.Button (new Rect (10, Screen.height - 80, 150, 40), "Player Gold: " + THEPLAYER.Gold + " \n Player XP: " + THEPLAYER.XP); 
				GUI.color = Color.white;
			} else {
				GUI.Button (new Rect (10, Screen.height - 80, 150, 40), "Player Gold: " + THEPLAYER.Gold + " \n Player XP: " + THEPLAYER.XP); 
			}
		}

		if (gameStart) {
			GUI.color = Color.red;
			if (GUI.Button (new Rect (10, Screen.height - 135, 150, 40), "QUIT GAME!")) {
				gameStart = false;
			}
			GUI.color = Color.white;
		}
		
		
		if (gameStart) {
			if (THETOWN.timer <= 15) {
				if (THETOWN.timer % 2 == 1) {

					GUI.color = Color.red;
				} else {
					GUI.color = Color.yellow;
				}
				GUI.skin.button.fontSize = 19;
			GUI.skin.button.alignment = TextAnchor.MiddleLeft;
				//GUI.color = Color.green;
				string p = "";
			for(int i = 0; i<THETOWN.timer/2; i++){
				p += "I";
			}


				if (GUI.Button (new Rect (Screen.width - 220, Screen.height - 90, 220, 50), p)) {
					spendGoldToAddMaintenance ();
					
				}
			GUI.skin.button.alignment = TextAnchor.MiddleCenter;
			GUI.color = Color.white;
			GUI.skin.button.fontSize = 13;
				GUI.color = Color.white;

			} else {
			GUI.skin.button.alignment = TextAnchor.MiddleLeft;
			GUI.color = Color.green;
			GUI.skin.button.fontSize = 19;
			string zz = "";
			for(int i = 0; i<THETOWN.timer/2; i++){
				zz += "I";
			}

				if (GUI.Button (new Rect (Screen.width - 200, Screen.height - 80, 162, 30),  zz)) {
				spendGoldToAddMaintenance ();	

				}
			GUI.skin.button.fontSize = 13;
			GUI.skin.button.alignment = TextAnchor.MiddleCenter;
			GUI.color = Color.white;
			}

		}

		string ii = "";
		for (int i = 0; i < THETOWN.infrastructureLevel; i+=2) {
			ii += "I";
		}
		
		GUI.color = Color.green;
		GUI.skin.button.alignment = TextAnchor.MiddleLeft;
		GUI.skin.button.fontSize = 20;
		if (GUI.Button (new Rect (Screen.width - 200, Screen.height - 40, 162, 30), ii)) {
			StartCoroutine (infraspiel() );
		}
	GUI.skin.button.alignment = TextAnchor.MiddleCenter;
	GUI.skin.button.fontSize = 13;
	GUI.color = Color.white;
		// Printing goes to the Console pane.  
		// If an object doesn't extend monobehavior, calling print won't do anything.  
		// Make sure "Collapse" isn't selected in the Console pane if you want to see duplicate messages.
		if (HeroDisplay == true) {
			int heroCost = 0;
			if (HeroToDisplay.numWins == 0) {
				heroCost = 40;
			}
			if (GUI.Button (new Rect (Screen.width - 260, Screen.height - 290, 250, 110), 
				   "Hero Name: " + HeroToDisplay.xname + "\n" +
				   "Hero Type: " + HeroToDisplay.heroClass + "\n" +
				   "Cost to Hire: " + heroCost + "\n" +
				   "Hero Fetch Time: " + HeroToDisplay.fetchTime + "\n" +
				   "Hero Grind Time: " + HeroToDisplay.grindTime + "\n" +
				   "Hero Delivery Bonus: " + HeroToDisplay.deliverBonus + " "
			   )) {
				HeroToDisplay = null;
				HeroDisplay = false;
			//	initPanel.SetActive (true);
			}


			if (HeroToDisplay.isProtector == false) {
				GUI.color = Color.green;
			

				if (GUI.Button (new Rect (Screen.width - 260, Screen.height - 170, 250, 30), 
					    "Use this Hero as a Protector"
				    )) {
					ProtectionHeroesSet.Add (HeroToDisplay);
					HeroToDisplay.isProtector = true;
					if (AvailableHeroesSet.Contains (HeroToDisplay)) {
						AvailableHeroesSet.Remove (HeroToDisplay);
					}
					HeroToDisplay = null;
					HeroDisplay = false;
					//initPanel.SetActive (true);
				}


				GUI.color = Color.white;

			} else {
				
			GUI.color = Color.red;


			if (GUI.Button (new Rect (Screen.width - 260, Screen.height - 170, 250, 30), 
				"Disengage this hero from protection"
			)) {
				ProtectionHeroesSet.Remove (HeroToDisplay);
				HeroToDisplay.isProtector = false;

				AvailableHeroesSet.Add (HeroToDisplay);
				HeroToDisplay = null;
				HeroDisplay = false;
				//initPanel.SetActive (true);
			}


			GUI.color = Color.white;
		
		
		
			}
		}

		if (gameStart) {
			if (QuestsInProgress.Count > 0) {
				GUI.color = Color.green;
				GUI.Box (new Rect (10, 220, 200, 20), "Active Quests: "); 
				GUI.color = Color.white;
				int y = 242;
				foreach (Quest p in QuestsInProgress) {
					string z = "" + p.xname + "\n";
					z = z + "Time Left: " + p.timeReamining + " Secs";
					/*
				int i = 1;	
				foreach (Hero h in p.HeroesAssignedToThisQuest){
					z= z+ "Hero " + i + ": " + h.xname + "\n";
					i++;
					}*/
					GUI.Box (new Rect (10, y, 200, 40), z); 

					y += 42;
				}

			}
		}

		if (gameStart) {
			if (selectionHeroes == true && NumberOfExactHeroes != 0) {
				int y = 110;
				int x = Screen.width / 2 - 175;
				GUI.Box (new Rect (x, 80, 350, 20), "Choose remaining " + NumberOfExactHeroes + " heroes for this quest");
				foreach (Hero HeroToDisplay in haftq) {
					int heroCost = 0;
					if (HeroToDisplay.numWins == 0) {
						heroCost = 40;
					}

					int timeBonus = 0;
				if(curQuestCheck.QuestCategory == 0 ){
						timeBonus = HeroToDisplay.fetchTime;
				}

				if(curQuestCheck.QuestCategory == 1 ){
					timeBonus = HeroToDisplay.grindTime;
				}

					if (timeBonus < 0) {
						GUI.color = Color.green;
					} else if (timeBonus == 0) {
						if (curQuestCheck.QuestCategory == 2) {
							if (HeroToDisplay.deliverBonus > 0) {
								GUI.color = Color.green;
							} else {
								GUI.color = Color.red;
							}
						} else {

							GUI.color = Color.yellow;
						}
					} else {
						GUI.color = Color.red;
					}




					if (GUI.Button (new Rect (x + 50, y, 250, 80), 
				

						"Hero Name: " + HeroToDisplay.xname + "\n" +
						    "Cost to Hire: " + heroCost + "\n" +
						    "Hero XP: " + HeroToDisplay.XP + "\n" +
							"Time Bonus: " + timeBonus + "\n" +
						"Delivery Bonus: " + HeroToDisplay.deliverBonus + " "



					    )) {
						if (HeroToDisplay.numWins == 0 && this.THEPLAYER.Gold < 40) {
							StartCoroutine (NotEnoughGoldForNewHero ());
						} else {

							//HeroDisplay = false;
							NumberOfExactHeroes--;
							UserSelectedHeroes.Add (HeroToDisplay);
							haftq.Remove (HeroToDisplay);
							AvailableHeroesSet.Remove (HeroToDisplay);
							if (NumberOfExactHeroes == 0) {
								//print ("y");
								selectionHeroes = false;
								HeroSelectionInProgress = false;
								QuestPanel.SetActive (false);
								initPanel.SetActive (true);

							}
						}
					}
					GUI.color = Color.white;
			
					y += 83;
				}
				
			}
		}

		if (updateSucc) {
			GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 20, 300, 40), "Building Updated Successful");
		}


		if (updateFail) {
			GUI.color = Color.red;
			GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 20, 300, 40), "Not enough gold to update");
			GUI.color = Color.white;
		}

		if (updateSuccBuyCheck) {
			GUI.color = Color.green;
			GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 20, 300, 60), "You just bought a building \n Buildings help provide XP and Gold");
			GUI.color = Color.white;
		}


		if (updateFailBuyCheck) {
			GUI.color = Color.red;
			GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 20, 300, 40), this.buildingNEGU);
			GUI.color = Color.white;
		}

		if (NotEnoughForTannerybool) {
			GUI.color = Color.red;
			GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 20, 300, 40), "Tannery requires at least one functional building.");
			GUI.color = Color.white;
		}

		if (NotEnoughXPForQuestbool) {
			GUI.color = Color.red;
			GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 20, 300, 40), "You need more XP to initiate this Quest");
			GUI.color = Color.white;
		}



		if (gameStart) {
			if (gameTimer <= 0) {
				gameStart = false;
			}
			if (THEPLAYER.Gold <= 0) {
				gameStart = false;
			}
			GUI.color = Color.green;
			GUI.Box (new Rect (10, Screen.height - 35, 150, 22), "Time Left: " + gameTimer);
			GUI.color = Color.white;
			//	gameTimer--;

		
		}






		if (!gameStart) {
		
			int x = THEPLAYER.Gold;
			GUI.skin.box.fontSize = 40;
			if (x <= 0) {
				GUI.color = Color.red;
				GUI.Box (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 88, 400, 175), "GAME OVER! \n Ran out of Gold!");
				GUI.color = Color.white;
			} else if (quitcheck == false) {
				GUI.color = Color.red;
				GUI.Box (new Rect (Screen.width / 2 - 300, Screen.height / 2 - 95, 600, 190), "GAME OVER! \n Ran out of Maintenance \n Your score: \n " + x + "!");
				GUI.color = Color.white;
			} else {
				GUI.color = Color.red;
				GUI.Box (new Rect (Screen.width / 2 - 300, Screen.height / 2 - 95, 600, 190), "GAME OVER! \n You are a quitter!");
				GUI.color = Color.white;

			}
			GUI.skin.box.fontSize = 13;
		}

		if (criticalTime) {
			GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height - 60, 300, 40), "Please focus on maintaining your town \n It is reaching Critical Levels");
		}

		if (!QuestJustEnded.Equals ("")) {
			GUI.color = Color.green;
			GUI.Box (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 20, 400, 40), this.QuestJustEnded);
			GUI.color = Color.white;	
		}

		if (gameStart) {
			if (GUI.Button (new Rect (Screen.width - 210, Screen.height - 120, 180, 30), "" + goldToBuySecs + " Gold gets 25 maintenace")) {
				StartCoroutine (mainspiel ());
				spendGoldToAddMaintenance ();
			}
		}
		/*
	if (GUI.Button (new Rect (10, 10, 280, 40), "Available Quests") ) {
			if (QuestPanel.activeSelf != true) {
				QuestsButtonActive ();
			} else {
				QuestBackButtonClicked ();
				
			}
		}
	
		if (GUI.Button (new Rect (300, 10, 280, 40), "Available Heroes")) {
			if (HeroPanel.activeSelf != true) {
				HeroesButtonActive ();
	} else {
				HeroBackButtonClicked ();

	}
		}*/

		if (HeroSelectionInProgressError) {
			GUI.color = Color.red;
			GUI.Box (new Rect (Screen.width / 2 - 150, 50, 300, 25), "Assign heroes for this quest first");
			GUI.color = Color.white;
		}

		if (NotEnoughGoldForNewHerobool) {

			GUI.color = Color.red;
			GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height - 40, 300, 25), "You need 40 Gold to select this Hero");
			GUI.color = Color.white;
		}

		if (infraspielbool) {
			GUI.Box (new Rect (Screen.width / 2 - 250, Screen.height - 40, 500, 25), "Buy buildings to sustain the infrastructure level");
		}

		if (mainspielbool) {
			GUI.Box (new Rect (Screen.width / 2 - 250, Screen.height - 40, 500, 25), "Use gold to buy maintenance and stay in the game");
		}

		if (welcomeGUI) {
			GUI.Box (new Rect (Screen.width / 2 - 185, Screen.height - 45, 370, 40), "The game timer on the left shows the time left for \n you to build up your town - You start with 1200 seconds");
			GUI.Box (new Rect (Screen.width / 2 - 185, Screen.height - 90, 370, 40), "Never let the Maintenance bar empty out! \n It starts decreasing when you being a Quest");
			GUI.Box (new Rect (Screen.width / 2 - 185, Screen.height / 2 - 105, 370, 30), "Initiate Quests to earn Gold and XP Multipliers");
			GUI.Box (new Rect (Screen.width / 2 - 185, Screen.height / 2 - 70, 370, 30), "Invest in buildings and complete quests to increase XP faster");
			GUI.Box (new Rect (Screen.width / 2 - 185, Screen.height / 2 - 35, 370, 30), "Heroes upgrade as they complete more quests");
			GUI.skin.box.fontSize = 15;
			GUI.color = Color.yellow;
			if (GUI.Button (new Rect (Screen.width / 2 - 185, Screen.height / 2, 370, 50), "Your game will being in: " + this.welcomeTimer + " seconds! \n Click here to begin the game now!")) {
				this.welcomeTimer = 0;
			}
			GUI.color = Color.white;
			GUI.skin.box.fontSize = 13;
			GUI.Box (new Rect (Screen.width / 2 - 185, Screen.height / 2 + 55, 370, 30), "Be Smaug! Accumulate as much gold as possible");
			GUI.Button (new Rect (10, Screen.height - 130, 150, 40), "The Player's Stats \n are shown below: "); 
	
	
	
	
	
	
		}




		if (!NoRougeforWorkshopString.Equals ("")) {
			GUI.color = Color.red;
			GUI.Box (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 20, 400, 40), this.NoRougeforWorkshopString);
			GUI.color = Color.white;
		}

		if (!NotJuggernautForBlackSmithString.Equals ("")) {
			GUI.color = Color.red;
			GUI.Box (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 20, 400, 40), this.NotJuggernautForBlackSmithString);
			GUI.color = Color.white;
		}

		if (!NotWarriorForApothecaryString.Equals ("")) {
			GUI.color = Color.red;
			GUI.Box (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 20, 400, 40), this.NotWarriorForApothecaryString);
			GUI.color = Color.white;
		}

		if (!NotOracleForChurchString.Equals ("")) {
			GUI.color = Color.red;
			GUI.Box (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 20, 400, 40), this.NotOracleForChurchString);
			GUI.color = Color.white;
		}


		if (!HeroNotGoodEnoughString.Equals ("")) {
			GUI.color = Color.red;
			GUI.Box (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 20, 400, 40), this.HeroNotGoodEnoughString);
			GUI.color = Color.white;
		}

		if (QuestConfirmOption == true) {
		QuestGoldPanel.SetActive (true);

		QuestTimerPanel.SetActive (true);
		QuestXPRewardPanel.SetActive (true);
		QuestXPNeededPanel.SetActive (true);

			Quest doesDisplay = questToConfirm;
			GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 170, 300, 30), "Quest Name: " + doesDisplay.dname);
		GUI.skin.box.fontSize = 15;
			GUI.Box (new Rect (Screen.width / 2 - 140f, Screen.height / 2 - 63, 50, 25), ""+ doesDisplay.goldReward);
		GUI.Box (new Rect (Screen.width / 2 - 60, Screen.height / 2 - 63, 50, 25), ""+ doesDisplay.TimeNeeded);
		GUI.Box (new Rect (Screen.width / 2 +20, Screen.height / 2 - 63, 50, 25), ""+ doesDisplay.reqXP);
		GUI.Box (new Rect (Screen.width / 2 + 95, Screen.height / 2 - 63, 50, 25), ""+ doesDisplay.HeroXPReward);
		GUI.skin.box.fontSize = 13;
		/*GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 60, 300, 80), 
				"Quest Name: " + doesDisplay.xname + "\n" +
				"XP Needed " + doesDisplay.reqXP + "\n" +
				"Level Needed " + doesDisplay.reqLevel + "\n" +
				"Hero XP Reward: " + doesDisplay.HeroXPReward + " "
			); */
			GUI.color = Color.green;
			if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 -30, 100, 40), "Initiate Quest")) {
				QuestConfirmOption = false;
				questToConfirm = null;
				QuestButtonClicked (doesDisplay);
			QuestGoldPanel.SetActive (false);

			QuestTimerPanel.SetActive (false);
			QuestXPRewardPanel.SetActive (false);
			QuestXPNeededPanel.SetActive (false);

			}
			GUI.color = Color.red;
			if (GUI.Button (new Rect (Screen.width / 2 + 50, Screen.height / 2 -30, 100, 40), "Go Back!")) {
				QuestConfirmOption = false;
				questToConfirm = null;
			QuestGoldPanel.SetActive (false);

			QuestTimerPanel.SetActive (false);
			QuestXPRewardPanel.SetActive (false);
			QuestXPNeededPanel.SetActive (false);

				
			}
			GUI.color = Color.white;

			
		}



		if (BuildingConfirmOption == true) {
			int builDisplay = this.buildingToConfirm;
			string toDisplay = "";
			switch (builDisplay) {
			
			case 0:
				toDisplay = "Name: Workshop \n Requires: An Assassin \n Cost: 300 Gold \n Effect: Generates 1 XP per second";
				break;
			case 1:
				toDisplay = "Name: Blacksmith \n Requires: A Juggernaut \n Cost: 500 Gold \n Effect: Increases quests reward by 10 Gold";
				break;
			case 2:
				toDisplay = "Name: Apothecary \n Requires: A Juggernaut \n Cost: 300 Gold \n Effect: Generates 1 XP per second";
				break;
			case 3:
				toDisplay = "Name: Tannery \n Requires: Another Building \n Cost: 450 Gold \n Effect: Reduces Quest's time by 10 seconds";
				break;
			case 4:
				toDisplay = "Name: Church \n Requires: An Oracle \n Cost: 1000 Gold \n Effect: Generates 1 Gold per second";
				break;
			default:
				toDisplay = "Building doesn't exist";
				break;
			}
			
			GUI.Box (new Rect (Screen.width - 580, 20, 300, 75), toDisplay); 
			GUI.color = Color.green;
			if (GUI.Button (new Rect (Screen.width - 580, 97, 100, 40), "Buy Building")) {
				BuildingConfirmOption = false;
				buildingToConfirm = -1;
				BuyBuildings (builDisplay);
			}
			GUI.color = Color.red;
			if (GUI.Button (new Rect (Screen.width - 380, 97, 100, 40), "Go Back!")) {
				BuildingConfirmOption = false;
				buildingToConfirm = -1;

			}
			GUI.color = Color.white;


		}


		if (!gameStart) {
			GUI.skin.box.fontSize = 50;
			GUI.color = Color.green;
			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 100, 200, 100), "RESTART GAME!")) {
				Application.LoadLevel ("QGiver");
			}
			GUI.skin.box.fontSize = 12;
			GUI.color = Color.white;
		}

		if (gameStart) {
			if (defenceOn ) {
				GUI.color = Color.green;
				if (GUI.Button (new Rect (Screen.width/2  - 400, 85, 200, 45), "Defences are on!")) {
					defenceOn = false;
					StopCoroutine(	defenceOff());
				}
			GUI.color = Color.white;


				
			} else {
				GUI.color = Color.red;
			if (GUI.Button (new Rect (Screen.width /2 - 400, 85, 200, 45), "Defences are off!")) {
				defenceOn = true;
				StartCoroutine(	defenceOnR ());
			}
			GUI.color = Color.white;
		
			}
	
	
	
	
		}


		if (gameStart) {
			if (!defenceDisplay.Equals ("")) {
				GUI.Box (new Rect (Screen.width / 2 - 185, Screen.height/2 - 30, 370, 60), defenceDisplay);
			}
				
		}


	if (!welcomeGUI){
	//draw the background:
		GUI.color = Color.white;
		GUI.Box (new Rect (Screen.width/2 - 400, 20, 200 , 30), "Protection Meter: ");
	
		//GUI.Box (new Rect (Screen.width/2 - 400, 52, 200, 30), emptyTex);

		//draw the filled-in part:

			
			
		//fullTex.color = Color.green;
		/*GUI.color = Color.green;
			///int nwidth = 200 * ((gameTimer % 10) / 10.0f);

		for (int y = 0; y < fullTex.height; ++y)
		{
			for (int x = 0; x < fullTex.width; ++x)
			{
				

				fullTex.SetPixel(x, y, Color.red);
			}
		}
		fullTex.Apply();
		*/

		//GUI.Box (new Rect (Screen.width/2 - 400, 52, 200 * ((gameTimer % 10) / 10.0f), 30), fullTex);
		//GUI.contentColor = Color.white;
		//GUI.color = Color.white;


		//ScrollbarHeroes.sizeDelta = new Vector2 (ScrollbarQuests.sizeDelta.x, 30*(x+2));
		//gdotPanel.transform.localScale = Camera.main.ScreenToWorldPoint(new Vector3(200 * ((gameTimer % 10) / 10.0f), 30, 0));
		//
		//gdotPanel.transform.localScale += new Vector3 (0.00F,0,0);

	//	gdotPanel.SetActive (true);


			// For the security timer
			if (gameStart) {
				if (this.THETOWN.securityLevel > 30) {
				// Create the green bar
				GUI.color = Color.green;
				GUI.skin.box.fontSize = 19;
				GUI.skin.box.alignment = TextAnchor.MiddleLeft;
					string bar = "";
					for (int i = 0; i < this.THETOWN.securityLevel; i += 3) {
						bar += "I";
					}
					GUI.Box (new Rect (Screen.width/2 - 393, 52, 185, 30), bar);

				GUI.skin.box.alignment = TextAnchor.MiddleCenter;
			
			
				GUI.skin.box.fontSize = 13;
				GUI.color = Color.white;
				} else{
				// Create the red bar
				GUI.color = Color.red;
				GUI.skin.box.fontSize = 19;
				GUI.skin.box.alignment = TextAnchor.MiddleLeft;
				string bar = "";
				for (int i = 0; i < this.THETOWN.securityLevel; i += 3) {
					bar += "I";


				}
				GUI.Box (new Rect (Screen.width/2 - 393, 52, 185, 30), bar);
			
				GUI.skin.box.alignment = TextAnchor.MiddleCenter;
				GUI.skin.box.fontSize = 13;
				GUI.color = Color.white;
				}
		
		
		
			}


	}


			


	
	}






}




