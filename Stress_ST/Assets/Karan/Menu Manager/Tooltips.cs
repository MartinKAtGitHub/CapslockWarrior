using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltips : MonoBehaviour {


    /// <summary>
    ///  WE NEED TO ADJUST THIS
    /// 1 . so you need to manully need to put in the TEXTBOX size which can be automated can be done with tags, look in child for boc, gameobject cacheing.
    /// 2 . connect it to the TEXT so it also scales withh the text box 
    /// 3 . make the StartOpen() to work on hover 
    /// 4 . make the life span(uses a timer) as long as player is hovering over the spell keep the tool tip open.
    /// </summary>
	


    [System.Serializable]
    public class AnimSettings
    {
        public enum OpenStyle
        {
            WidthToHeight,
            HeightToWidth,
            HeightAndWidth
        };
        public OpenStyle openStyle;
        public float widthSmooth = 4.6f;
        public float heightSmooth = 4.6f;
        public float textSmooth = 0.1f;

        [HideInInspector]
        public bool widthOpen = false;
        [HideInInspector]
        public bool heightOpen = false;

		

        public void Initialize()
        {
            widthOpen = false;
            heightOpen = false;
        }

    }

    [System.Serializable]
    public class UISettings
    {
        public Image textBox; // container for the text
        public Text text; // holding tooltip
        public Vector2 initialBoxSize = new Vector2(0.25f, 0.25f); // the size on the textBox initially, without any scaling
        public Vector2 openedBoxSize = new Vector2(400, 200);
        public float snapToSizeDistance = 0.25f;

        public float lifeSpan = 5f; // dont need this anymore

        [HideInInspector]
        public bool opening = false;
        [HideInInspector]
        public Color textColor;
        [HideInInspector]
        public Color textBoxColor;
        [HideInInspector]
        public RectTransform textBoxRect;
        [HideInInspector]
        public Vector2 currentSize;
		[HideInInspector]
  		public bool stillHovring = false;

        public void Initialize ()
        {
            textBoxRect = textBox.GetComponent<RectTransform>();
            textBoxRect.sizeDelta = initialBoxSize;
            currentSize = textBoxRect.sizeDelta;
            opening = false;
            // Set the text color alpha back to 0;
            textColor = text.color;
            textColor.a = 0;
            text.color = textColor;
            textBoxColor = textBox.color;
            textBoxColor.a = 1;
            textBox.color = textBoxColor;

            textBox.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
    }

    // ----------------- Dont think we need this but the Reasoning behind it is that we need this to see them in the inspector --------------------------------------

    public AnimSettings animSettings = new AnimSettings();
    public UISettings uiSettings = new UISettings();

    // -----------------------------------
	
    float lifeTimer = 0;
    // Use this for initialization
    void Start ()
    {
        animSettings.Initialize();
        uiSettings.Initialize();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (uiSettings.opening)
        {
            OpenToolTip();

			if(uiSettings.stillHovring == true)
			{
				if(animSettings.widthOpen && animSettings.heightOpen)
	            {
					FadeTextIn();
	            }

			}
			else 
	        {
	          	FadeToolTipOut();
	        }
				

	            /*if(animSettings.widthOpen && animSettings.heightOpen)
	            {


	                lifeTimer += Time.deltaTime; // if you evener need to do it on click
	                if(lifeTimer > uiSettings.lifeSpan)
	                {
	                    FadeToolTipOut();
	                }
	                else
	                {
	                    FadeTextIn();
	                }

					if(uiSettings.stillHovring == true)
	                {
						FadeTextIn();
	                }
	                else
	                {
	                	FadeToolTipOut();
	                }
	            }*/
            }

    }
    public void CloseToolTip()
    {
		uiSettings.stillHovring = false;
    }
    public void StartOpen() // on click do this || or on hover
    {
        uiSettings.opening = true;
		uiSettings.stillHovring = true;
        uiSettings.textBox.gameObject.SetActive(true);
        uiSettings.text.gameObject.SetActive(true);
    }
    private void OpenToolTip()
    {
        switch(animSettings.openStyle)
        {
            case AnimSettings.OpenStyle.HeightToWidth:
                OpenHeightToWidth();
                break;
            case AnimSettings.OpenStyle.WidthToHeight:
                OpenWidthToHeight();
                break;
            case AnimSettings.OpenStyle.HeightAndWidth:
                OpenHeightAndWidt();
                break;
            default:
                Debug.LogError("No animation is set for the selected open style");
                break;
        }
        uiSettings.textBoxRect.sizeDelta = uiSettings.currentSize;
    }

    private void OpenWidthToHeight()
    {
        if(!animSettings.widthOpen)
        {
            OpenWidth();
        }
        else
        {
            OpenHeight();
        }

    }
    private void OpenHeightToWidth()
    {
        if(!animSettings.heightOpen)
        {
            OpenHeight();
        }
        else
        {
            OpenWidth();
        }
    }
    private void OpenHeightAndWidt()
    {
        if(!animSettings.widthOpen)
        {
            OpenWidth();
        }
        if(!animSettings.heightOpen)
        {
            OpenHeight();
        }
    }

    private void OpenWidth()
    {
        uiSettings.currentSize.x = Mathf.Lerp(uiSettings.currentSize.x, uiSettings.openedBoxSize.x, animSettings.widthSmooth * Time.deltaTime);

        // think opened box size as the target size. we check the distance regardelss of neative or pos, when it gets close just snap to target size
        if (Mathf.Abs(uiSettings.currentSize.x - uiSettings.openedBoxSize.x) < uiSettings.snapToSizeDistance)
        {
            uiSettings.currentSize.x = uiSettings.openedBoxSize.x;
            animSettings.widthOpen = true;
        }
    }
    private void OpenHeight()
    {
        uiSettings.currentSize.y = Mathf.Lerp(uiSettings.currentSize.y, uiSettings.openedBoxSize.y, animSettings.heightSmooth * Time.deltaTime);

        if (Mathf.Abs(uiSettings.currentSize.y - uiSettings.openedBoxSize.y) < uiSettings.snapToSizeDistance)
        {
            uiSettings.currentSize.y = uiSettings.openedBoxSize.y;
            animSettings.heightOpen = true;
        }
    }

    private void FadeTextIn()
    {
        uiSettings.textColor.a = Mathf.Lerp(uiSettings.textColor.a, 1, animSettings.textSmooth * Time.deltaTime);
        uiSettings.text.color = uiSettings.textColor;
    }
    private void FadeToolTipOut()
    {
        uiSettings.textColor.a = Mathf.Lerp(uiSettings.textColor.a, 0 , animSettings.textSmooth * Time.deltaTime);
        uiSettings.text.color = uiSettings.textColor;
        uiSettings.textBoxColor.a = Mathf.Lerp(uiSettings.textBoxColor.a, 0, animSettings.textSmooth * Time.deltaTime);
        uiSettings.textBox.color = uiSettings.textBoxColor;


        // TODO BUG FIX this thing dosnt close properly if you hover and then  move out out of range then hover again then stuff gets bugged
        if(uiSettings.textBoxColor.a < 0.01f) // anim is done and need to restart to start anim a new
        {
            uiSettings.opening = false;
            animSettings.Initialize();
            uiSettings.Initialize();
            lifeTimer = 0;
        }
    }


}
