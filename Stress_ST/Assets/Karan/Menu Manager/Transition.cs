using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Transition : MonoBehaviour {

    /// <summary>
    /// This scripts sould be placed on all pages that transition
    /// 
    /// </summary>

    public enum OutTransitionType
    {
        Fade,
        FadeInstant,
        Flicker
    };
    public enum InTransitionType
    {
        Fade,
        FadeInstant,
        Flicker
    };

    public OutTransitionType outTransitionType;
    public InTransitionType inTransitionType;

    public string parentTag; // setting the canvas as parent so we can see the UI

    public Vector3 spawnPosition = Vector3.zero; //change this if u want to spawn the menus out side of screen

    public float fadeSpeed = 2;
    public float flickerRate = 0.025f;

    // variabels to handle bringing the new page in

   // private bool transitionInitialized = false;
    private bool startTransition = false;
    
    private float inColorAlpha = 0;
    private float outColorAlpha = 0;

    private Text[] transitionTexts;
    private Text[] texts;

    private Image[] transitionImage;
    private Image[] images;

    private RectTransform transitionPage;
   // private RectTransform thisPage;


    // Use this for initialization
    void Start ()
    {
	    if(outTransitionType == OutTransitionType.Fade)
        {
            outColorAlpha = 1;
        }
       // thisPage = GetComponent<RectTransform>();
        // hold images and texts that are going to be faded
       images = GetComponentsInChildren<Image>();
       texts = GetComponentsInChildren<Text>();

		if(parentTag == null)
       {
       		Debug.LogError("You Have Forgoten To add Parent tag");
       }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(startTransition)
        {
            switch (outTransitionType)// page leaving
            {
                case OutTransitionType.Fade:
                    FadePageOut();
                    break;
                case OutTransitionType.FadeInstant:
                    outColorAlpha = 0;
                    break;
                case OutTransitionType.Flicker:
                    StartCoroutine("FlickerOut", flickerRate);
                    break;
                default:
                    Debug.LogError("outTransitionType switch failed");
                    break;
            }

            switch (inTransitionType)
            {
                case InTransitionType.Fade:
                    FadePageIn();
                    break;
                case InTransitionType.FadeInstant:
                    inColorAlpha = 1;
                    break;
                case InTransitionType.Flicker:
                    StartCoroutine("FlickerIn", flickerRate);
                    break;
                default:
                    break;
            }

            UpdateTransitionPageColors();
            UpdateCurrentPageColors();

        }
	}


    public void StartTransition()
    {
        startTransition = true;
    }


    public GameObject InitializeTransitionPage(GameObject transition)
    {
        // set the transition page
        GameObject go = Instantiate(transition as GameObject);
        transitionPage = go.GetComponent<RectTransform>();

        // the transition page parent needs to be the canvas (or one of its children)
        transitionPage.transform.SetParent(GameObject.FindGameObjectWithTag(parentTag).transform, false);
        transitionPage.transform.localScale = Vector3.one;
        // fill the text and image arrays with componants that need to be faded
        transitionTexts = transitionPage.GetComponentsInChildren<Text>();
        transitionImage = transitionPage.GetComponentsInChildren<Image>();

        // starts the page transparent
        foreach (var txt in transitionTexts)
        {
            txt.color = new Vector4(txt.color.r, txt.color.g, txt.color.b, 0);
        }
        foreach (var img in transitionImage)
        {
            img.color = new Vector4(img.color.r, img.color.g, img.color.b, 0);
        }

        //transitionInitialized = true;

        return transitionPage.gameObject;
    }

    // OUT TRANSITIONING FUNCTIONS
    private void FadePageOut()
    {
        outColorAlpha = Mathf.Lerp(outColorAlpha, 0, fadeSpeed * Time.deltaTime);
    }
    
    private void FadePageIn() // this wont work we nneed more transition styles
    {
          inColorAlpha = Mathf.Lerp(inColorAlpha, 1, fadeSpeed * Time.deltaTime);

       /* 
       	// trying to find all the buttons and interactiv = false so that they cant be pressed while transitioing
       if(gameObject.GetComponentInChildren<Button>() != null)
        {
        	Debug.Log("This gameobject is a button");
			//gameObject.GetComponentsInChildren<Button>().Length;
        	
        }*/
        // Because when LERP reaches close to target(1) its slows down, so we have to force ouer way to target to avoid a very long and costly function call.
        if (inColorAlpha > 0.99f)
        {
            inColorAlpha = 1;
        }
        if(inColorAlpha == 1) // CHANGE this to else if maybe ?
        {
            Destroy(gameObject);
        }
    } 

    IEnumerator FlickerOut (float frequency) // HardCoded stuff we migth need to change this 
    {
        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(frequency);
            outColorAlpha = 0.35f;
            yield return new WaitForSeconds(frequency);
            outColorAlpha = 0.8f;

        }

    }
    IEnumerator FlickerIn(float frequency)
    {
        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(frequency);
            outColorAlpha = 0.35f;
            yield return new WaitForSeconds(frequency);
            outColorAlpha = 1f;
        }

        if (inColorAlpha == 1.0f)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateTransitionPageColors()
    {
        if(transitionImage != null)
        {
            foreach (Image img in transitionImage)
            {
				// TODO if i click to fast this casts an error
				// the reason is i am trying to accses this when it is gone by clicking on back to fast 
                img.color = new Vector4(img.color.r, img.color.g, img.color.b, inColorAlpha); 
            }
        }
        if(transitionTexts != null)
        {
            foreach (Text txt in transitionTexts)
            {
                txt.color = new Vector4(txt.color.r, txt.color.g, txt.color.b, inColorAlpha);
            }
        }
    }

    private void UpdateCurrentPageColors()
    {
        if(images != null)
        {
            foreach (Image img in images)
            {
                img.color = new Vector4(img.color.r, img.color.g, img.color.b, outColorAlpha);
            }
        }
        if(texts != null)
        {
            foreach (Text txt in texts)
            {
                txt.color = new Vector4(txt.color.r, txt.color.g, txt.color.b, outColorAlpha);
            }
        }
    }

}
