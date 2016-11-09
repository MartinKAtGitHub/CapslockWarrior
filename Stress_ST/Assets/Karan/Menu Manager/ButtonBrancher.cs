using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ButtonBrancher : MonoBehaviour {

    public class ButtonScaler // Handling resizing buttons if we change the screen size so we can have multi res capability
    {
        enum ScaleMode
        {
            MatchWidthHeight,
            IndependentWidthHeight
        };

        ScaleMode mode;
        Vector2 referenceButtonSize;

        [HideInInspector]
        public Vector2 referanceScreenSize;
        public Vector2 newButtonSize;

        public void Initialize(Vector2 refButtonSize, Vector2 refScreenSize, int scaleMode)
        {
            mode = (ScaleMode)scaleMode;
            referenceButtonSize = refButtonSize;
            referanceScreenSize = refScreenSize;
            SetNewButtonSize();
        }

        void SetNewButtonSize()
        {
            if(mode == ScaleMode.IndependentWidthHeight)
            {
                newButtonSize.x = (referenceButtonSize.x * Screen.width) / referanceScreenSize.x;
                newButtonSize.y = (referenceButtonSize.y * Screen.height) / referanceScreenSize.y;
            }
            else if(mode == ScaleMode.MatchWidthHeight)
            {
                newButtonSize.x = (referenceButtonSize.x * Screen.width) / referanceScreenSize.x;
                newButtonSize.y = newButtonSize.x;
            }
        }
    }

    [System.Serializable]
    public class ButtonSettings
    {
        public enum RevealOptions
        {
            Linear,
            Circular
        };
        public RevealOptions options;
        public float translateSmooth = 5f;  // how fast the buttons move to their positions
        public float fadeSmooth = 0.01f;       // how fast the buttons fade in(if they fade in)
        public bool revealOnStart = false;  // spawns buttons at the start of game.

        [HideInInspector]
        public bool opening = false;
        [HideInInspector]
        public bool spawned = false;

    }
    [System.Serializable]
    public class LinearSpawner
    {
        public enum RevealStyle
        {
            SlideToPosition,
            FadeInAtPosition
        };
        public RevealStyle revealStyle;
        public Vector2 direction = new Vector2(0,1);    // slide down
        public float baseButtonSpacing = 5f;           // how much space between each button
        public int buttonNumOffset = 0;                 // how many button spaces offset? sometims necessary when using multiple button branches

        [HideInInspector]
        public float buttonSpacing = 5f;

        public void FitSpacingToScreenSize(Vector2 refScreenSize)
        {
            float refScreenFloat = (refScreenSize.x + refScreenSize.y) / 2;
            float screenFloat = (Screen.width + Screen.height) / 2;
            buttonSpacing = (baseButtonSpacing * screenFloat) / refScreenFloat;
        }
    }

    [System.Serializable]
    public class CircularSpawner
    {
        public enum RevealStyle
        {
            SlideToPosition,
            FadeInAtPosition
        };
        public RevealStyle revealStyle;
        public Angle angle;
        public float baseDistanceFromBrancher = 20f;

        [HideInInspector]
        public float distanceFromBrancher = 0f;

        [System.Serializable]
        public struct Angle
        {
            public float minAngle;
            public float maxAngle;
        }
        public void FitDistanceToScreenSize(Vector2 refScreenSize)
        {
            float refScreenFloat = (refScreenSize.x + refScreenSize.y) / 2;
            float screenFloat = (Screen.width + Screen.height) / 2;
            distanceFromBrancher = (baseDistanceFromBrancher * screenFloat) / refScreenFloat;
        }
    }


    public GameObject[] buttonRefs; // Prefabs
    [HideInInspector]
    public List<GameObject> buttons;

    public enum ScaleMode
    {
        MatchWidthHeight,
        IndependentWidthHeight
    };
    public ScaleMode mode;
    public Vector2 referenceButtonSize;
    public Vector2 referenceScreenSize;


    public ButtonSettings buttonSettings = new ButtonSettings();
    public LinearSpawner linSpawner = new LinearSpawner();
    public CircularSpawner circSpawner = new CircularSpawner();

    ButtonScaler buttonScaler = new ButtonScaler();
    float lastScreenWidth = 0;
    float lastScreenHeight = 0;


    // Use this for initialization
    void Start ()
    {
        buttons = new List<GameObject>();
        buttonScaler = new ButtonScaler();
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
        buttonScaler.Initialize(referenceButtonSize, referenceScreenSize, (int)mode);

        circSpawner.FitDistanceToScreenSize(buttonScaler.referanceScreenSize);
        linSpawner.FitSpacingToScreenSize(buttonScaler.referanceScreenSize);

        if(buttonSettings.revealOnStart)
        {
            SpawnButtons();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            buttonScaler.Initialize(referenceButtonSize, referenceScreenSize, (int)mode);
            circSpawner.FitDistanceToScreenSize(buttonScaler.referanceScreenSize);
            linSpawner.FitSpacingToScreenSize(buttonScaler.referanceScreenSize);
            SpawnButtons();
        }
        if(buttonSettings.opening)
        {
            if(!buttonSettings.spawned)
            {
                SpawnButtons();
            }

            switch(buttonSettings.options)
            {
                case ButtonSettings.RevealOptions.Linear:
                    switch(linSpawner.revealStyle)
                    {
                        case LinearSpawner.RevealStyle.SlideToPosition:
                            RevealLinearlyNormal();
                            break;
                        case LinearSpawner.RevealStyle.FadeInAtPosition:
                            RevealLinearlyFade();
                            break;
                    }
                    break;

                case ButtonSettings.RevealOptions.Circular:
                    switch(circSpawner.revealStyle)
                    {
                        case CircularSpawner.RevealStyle.SlideToPosition:
                            RevealCircularNormal();
                            break;
                        case CircularSpawner.RevealStyle.FadeInAtPosition:
                            RevealCircularFade();
                            break;
                    }
                    break;
            }
        }
    }

    public void SpawnButtons()
    {
        buttonSettings.opening = true;
        for (int i = buttons.Count - 1; i >= 0; i--)
        {
            Destroy(buttons[i]);
        }
        buttons.Clear();
        ClearCommanButtonsBreancher();

        for(int i = 0; i < buttonRefs.Length; i++)
        {
       
            GameObject b = Instantiate(buttonRefs[i] as GameObject);
            b.transform.SetParent(transform); // make child of button brancher parent
            b.transform.position = transform.position; // zeroing the pos places the button on the button brancher

             //check if the btn fade or not
            if(linSpawner.revealStyle == LinearSpawner.RevealStyle.FadeInAtPosition || circSpawner.revealStyle == CircularSpawner.RevealStyle.FadeInAtPosition)
            {

                Color c = b.GetComponent<Image>().color; // change color apha of button and its text to 0
                c.a = 0;
                b.GetComponent<Image>().color = c;

                if (b.GetComponentInChildren<Text>())
                {
                    c = b.GetComponentInChildren<Text>().color;
                    c.a = 0;
                    b.GetComponentInChildren<Text>().color  = c;
                }
            }
            buttons.Add(b);
			
        }
        buttonSettings.spawned = true;

    }
    private void ClearCommanButtonsBreancher()
    {
  
        GameObject[] branchers = GameObject.FindGameObjectsWithTag("BtnBranchers");
        foreach(GameObject brancher in branchers)
        {
            if(brancher.transform.parent == transform.parent)
            {
                ButtonBrancher bb = brancher.GetComponent<ButtonBrancher>();
                for(int i = bb.buttons.Count -1; i >= 0; i--)
                {
                    Destroy(bb.buttons[i]);
                }
                bb.buttons.Clear();
            }
        }
    }

    private void RevealLinearlyNormal()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            Vector3 targetPos;      //Give the button a position to move towards
            RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();
            //set size
            buttonRect.sizeDelta = new Vector2(buttonScaler.newButtonSize.x, buttonScaler.newButtonSize.y);
            //set pos
            targetPos.x = linSpawner.direction.x * ((i + linSpawner.buttonNumOffset) * (buttonRect.sizeDelta.x + linSpawner.buttonSpacing)) + transform.position.x;
            targetPos.y = linSpawner.direction.y * ((i + linSpawner.buttonNumOffset) * (buttonRect.sizeDelta.y + linSpawner.buttonSpacing)) + transform.position.y;
            targetPos.z = 0;

            buttonRect.position = Vector3.Lerp(buttonRect.position, targetPos, buttonSettings.translateSmooth * Time.deltaTime); // lerp(from, to, how fast)
        }
    }

    private void RevealLinearlyFade()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Vector3 targetPos;      //Give the button a position to move towards
            RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();
            //set size
            buttonRect.sizeDelta = new Vector2(buttonScaler.newButtonSize.x, buttonScaler.newButtonSize.y);
            //set pos
            targetPos.x = linSpawner.direction.x * ((i + linSpawner.buttonNumOffset) * (buttonRect.sizeDelta.x + linSpawner.buttonSpacing)) + transform.position.x;
            targetPos.y = linSpawner.direction.y * ((i + linSpawner.buttonNumOffset) * (buttonRect.sizeDelta.y + linSpawner.buttonSpacing)) + transform.position.y;
            targetPos.z = 0;

            ButtonFader previousButtonFader;
            if(i > 0)
            {
                previousButtonFader = buttons[i - 1].GetComponent<ButtonFader>();
            }
            else
            {
                previousButtonFader = null;
            }

            ButtonFader buttonFader = buttons[i].GetComponent<ButtonFader>();

            if (previousButtonFader) // first button wont have a previous button
            {
                if(previousButtonFader.faded)
                {
                    buttons[i].transform.position = targetPos;

                    if (buttonFader)
                    {
                        buttonFader.Fade(buttonSettings.fadeSmooth);
                    }
                    else
                    {
                        Debug.LogError(" You want to fade buttons, but they need a buttonfader script on them 1");
                    }
                }
            }
            else
            {
                buttons[i].transform.position = targetPos;
                if (buttonFader)
                {
                    buttonFader.Fade(buttonSettings.fadeSmooth);
                }
                else
                {
                    Debug.LogError(" You want to fade buttons, but they need a buttonfader script on them 2");
                }
            }
        }

    }

    private void RevealCircularNormal()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            // find angle
            float angleDist = circSpawner.angle.maxAngle - circSpawner.angle.minAngle;
            float targetAngle = circSpawner.angle.minAngle + (angleDist / buttons.Count) * i;

            //find position
            Vector3 targetPos = transform.position + Vector3.right * circSpawner.distanceFromBrancher;
            targetPos = RotatePointAroundTarget(targetPos, transform.position, targetAngle);
            RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();
           
            //resize button
            buttonRect.sizeDelta = new Vector2(buttonScaler.newButtonSize.x, buttonScaler.newButtonSize.y);
            buttonRect.position = Vector3.Lerp(buttonRect.position, targetPos, buttonSettings.translateSmooth * Time.deltaTime);
        }
    }

    private void RevealCircularFade()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            // find angle
            float angleDist = circSpawner.angle.maxAngle - circSpawner.angle.minAngle;
            float targetAngle = circSpawner.angle.minAngle + (angleDist / buttons.Count) * i;

            //find position
            Vector3 targetPos = transform.position + Vector3.right * circSpawner.distanceFromBrancher;
            targetPos = RotatePointAroundTarget(targetPos, transform.position, targetAngle);
            RectTransform buttonRect = buttons[i].GetComponent<RectTransform>();

            //resize button
            buttonRect.sizeDelta = new Vector2(buttonScaler.newButtonSize.x, buttonScaler.newButtonSize.y);

            ButtonFader previousButtonFader;
            if (i > 0)
            {
                previousButtonFader = buttons[i - 1].GetComponent<ButtonFader>();
            }
            else
            {
                previousButtonFader = null;
            }
            ButtonFader buttonFader = buttons[i].GetComponent<ButtonFader>();

            if (previousButtonFader) // first button wont have a previous button
            {
                if (previousButtonFader.faded)
                {
                    buttons[i].transform.position = targetPos;

                    if (buttonFader)
                    {
                        buttonFader.Fade(buttonSettings.fadeSmooth);
                    }
                    else
                    {
                        Debug.LogError(" You want to fade buttons, but they need a buttonfader script on them 1");
                    }
                }
            }
            else
            {
                buttons[i].transform.position = targetPos;
                if (buttonFader)
                {
                    buttonFader.Fade(buttonSettings.fadeSmooth);
                }
                else
                {
                    Debug.LogError(" You want to fade buttons, but they need a buttonfader script on them 2");
                }
            }
        }
    }
    private Vector3 RotatePointAroundTarget(Vector3 point, Vector3 pivot, float angle)
    {
        Vector3 targetPoint = point - pivot;
        targetPoint = Quaternion.Euler(0, 0, angle) * targetPoint;
        targetPoint += pivot;
        return targetPoint;
    }
}


