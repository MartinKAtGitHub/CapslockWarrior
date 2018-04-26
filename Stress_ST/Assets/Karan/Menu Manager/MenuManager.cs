using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	[HideInInspector]
	public string LevelName; // this is just for holding data
	[Space(10)]
    /// <summary>
    /// 1. the Pages and PageName has to be in the same order for this to work. WHY NOT save the name of the game object at the same time/ use that name insted of another array
    /// </summary>
    public GameObject[] pages;
    public string[] pageNames; // the ID of a menu page.

    GameObject currentPage;
    //private bool enterScreen = false; // this can be used for pause.



	// Use this for initialization
	void Start ()
    {
        SetCurrentPage(pages[0]);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void SetCurrentPage(GameObject page)
    {
        GameObject p = Instantiate(page as GameObject);

        Debug.Log("Instantiating = " + p.name);

        //Debug.Log(p.GetComponent<RectTransform>().sizeDelta.y);
        //Debug.Log(p.GetComponent<RectTransform>().sizeDelta.x);


        p.transform.SetParent(transform, false);
		//p.transform.SetParent(transform); // Original
        
       // RectTransform rt = p.GetComponent<RectTransform>();
        //Transition t = p.GetComponent<Transition>(); //  each page is going to have a transition page on it


        //rt.offsetMax = new Vector2(t.spawnPosition.x, t.spawnPosition.y); // original
        //rt.offsetMin = new Vector2(t.spawnPosition.x, t.spawnPosition.y);// original

        p.transform.localScale = Vector3.one;

        // rt.sizeDelta = new Vector2(p.GetComponent<RectTransform>().sizeDelta.x, p.GetComponent<RectTransform>().sizeDelta.y); // dose not work for some reason
        //rt.sizeDelta = new Vector2(400f, 400f);


        //Debug.Log("WIDTH =" + rt.sizeDelta.x);
      //  Debug.Log("HEIGHT =" + rt.sizeDelta.y);
        
        currentPage = p;
    }

    // the menu controller listens for click events from a certain buttons and runs this method when they are clicked
    public void SetNextPage(string PAGE_CODE)
    {
        for (int i = 0; i < pageNames.Length; i++)
        {
            if (PAGE_CODE == pageNames[i])
            {

				if(PAGE_CODE == "LoadoutPageSinglePlayer") // This whole If is to send the name of lvl to the btn so it can be sendt
	            {
					GameObject btn;
					Debug.Log("LoadeOutPage");
					OnBtnClickStartLevel t;
	            	
					for (int p = 0; p < pages[i].transform.childCount; p++) 
					{
						if(pages[i].transform.GetChild(p).name == "StartLevel_btn")
						{
							btn = pages[i].transform.GetChild(p).gameObject;
							if(btn == null)
			            	{
			            		Debug.Log("Cant find the Btn");
			            	}

							t = btn.GetComponent<OnBtnClickStartLevel>();

							if(t == null)
			            	{
			            		Debug.LogError("Cant find Btn Or Script On btn");
			            	}
			            	else
			            	{
			            		t.LevelName = LevelName;
			            	}
						}
	            	}



	            	//btn = GameObject.FindGameObjectWithTag("StartLevelBtn");




	            }
                   // the pageNames have to be in the same order as the pages for this to work   
                RevealPageInUI(i);
            }
        }
    }

    // experimental method uses only the gameobject array and not the page name
	public void SetNextPageWithoutNameArray(string GAMEOBJECT_NAME)
    {
		for (int i = 0; i < pages.Length; i++)
        {
			if (GAMEOBJECT_NAME == pages[i].name)
            {
                // the pageNames have to be in the same order as the pages for this to work
               
                RevealPageInUI(i);
            }
        }
    }

    private void RevealPageInUI(int index)
    {
        Transition t = currentPage.GetComponent<Transition>();
        //start transitioning the current page this will active the "out" Transition
        t.StartTransition();
        // set the currentPage to the page that is comming in
        currentPage = t.InitializeTransitionPage(pages[index]);
        // position the page based on the page offset
       // RectTransform rt = currentPage.GetComponent<RectTransform>();
        t = currentPage.GetComponent<Transition>();

        //rt.offsetMax = new Vector2(t.spawnPosition.x, t.spawnPosition.y); // original
        //rt.offsetMin = new Vector2(t.spawnPosition.x, t.spawnPosition.y); // original
    }

}
