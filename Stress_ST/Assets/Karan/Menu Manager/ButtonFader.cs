using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonFader : MonoBehaviour {

    public bool faded = false;

    Image btnImg;
    Text txt;
    Color btnColor;
    Color txtColor;
    bool startFade = false;
    float smooth = 0;
    bool initialized = false;

	// Use this for initialization
	void Start ()
    {
        Initialize();
	}
	
    void Update()
    {
        if(startFade)
        {
            Fade(smooth);
            if(btnColor.a > 0.9)
            {
                faded = true;
            }
        }
    }

    void Initialize()
    {
        startFade = false;
        faded = false;
        btnImg = GetComponent<Image>();
        btnColor = btnImg.color;
        if(GetComponentInChildren<Text>())
        {
            txt = GetComponentInChildren<Text>();
            txtColor = txt.color;
        }
        initialized = true;
    }
    public void Fade(float rate)
    {
        // make sure the prorper values have been init
        if(!initialized)
        {
            Initialize();
        }

        smooth = rate;
        startFade = true;

        btnColor.a += rate;
        btnImg.color = btnColor;

        if(txt)
        {
            txtColor.a += rate;
            txt.color = txtColor;
        }
    }
}
