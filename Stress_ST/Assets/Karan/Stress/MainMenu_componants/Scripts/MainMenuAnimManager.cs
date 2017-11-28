using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimManager : MonoBehaviour {

	
	public GameObject MainMenuCanvasRoot;

	[Space(5f)]
	public GameObject UI_ExitPower;
	public GameObject UI_EndlessModePhone;
	public GameObject UI_PCMonitor;
	public GameObject UI_OptionsTable;

	[Space(5f)]
	public GameObject Anim_MenuCharactor;

	[Header("Names of GameObjects we need in this script")] // Copy and past stopped working Buggy shitt
	[Space(10)]
	public string MainMenuCanvasName;
	public string ExitPowerName = "Exit PowerButton";
	public string EndlessModePhoneName = "EndlessMode Phone";
	public string PCMonitorName = "PC Monitor";
	public string OptionsTableName = "OptionsTable";

	private Animator animatorExitPower;
	private Animator animatorEndlessModePhone;
	private Animator animatorPCMonitor;
	private Animator animatorOptionsTable;

	private Button btn_ExitPower;
	private Button btn_EndlessModePhone;
	private Button btn_PCMonitor;
	private Button btn_OptionsTable;

	private Text txt_ExitPower;
	private Text txt_EndlessModePhone;
	private Text txt_PCMonitor;
	private Text txt_OptionsTable;
	// Use this for initialization
	void Awake () 
	{
		NullCheckGameObject.NullCheckFindWithTag(ref MainMenuCanvasRoot, "MainCanvas");
		NullCheckGameObject.NullCheckFindWithName(ref UI_ExitPower , ExitPowerName);
		NullCheckGameObject.NullCheckFindWithName(ref UI_EndlessModePhone , EndlessModePhoneName);
		NullCheckGameObject.NullCheckFindWithName(ref UI_PCMonitor , PCMonitorName);
		NullCheckGameObject.NullCheckFindWithName(ref UI_OptionsTable , OptionsTableName);

		animatorExitPower = UI_ExitPower.GetComponent<Animator>();
		animatorEndlessModePhone = UI_EndlessModePhone.GetComponent<Animator>();
		animatorPCMonitor = UI_PCMonitor.GetComponent<Animator>();
		animatorOptionsTable = UI_OptionsTable.GetComponent<Animator>();

		btn_ExitPower = UI_ExitPower.GetComponentInChildren<Button>();
		btn_EndlessModePhone = UI_EndlessModePhone.GetComponentInChildren<Button>();
		btn_PCMonitor = UI_PCMonitor.GetComponentInChildren<Button>();
		btn_OptionsTable = UI_OptionsTable.GetComponentInChildren<Button>();

		txt_ExitPower = UI_ExitPower.GetComponentInChildren<Text>();
		txt_EndlessModePhone = UI_EndlessModePhone.GetComponentInChildren<Text>();
		txt_PCMonitor = UI_PCMonitor.GetComponentInChildren<Text>();
		txt_OptionsTable = UI_OptionsTable.GetComponentInChildren<Text>();
	}


	public void LoadeStoryModeScene()
	{
		// Needs to be playerd after Animation. 
		Debug.Log("Loade StoryMode scene!");
	}

	public void OnClickStoryMode()
	{
		IsAllButtonsInteractable(false);
		DisableAllText();
	}



	void IsAllButtonsInteractable(bool interactable)
	{
		btn_ExitPower.interactable = interactable;
		btn_EndlessModePhone.interactable = interactable;
		btn_PCMonitor.interactable = interactable;
		btn_OptionsTable.interactable = interactable;
	}

	void DisableAllText()
	{
		txt_ExitPower.enabled = false;
		txt_EndlessModePhone.enabled = false;
		txt_PCMonitor.enabled = false;
		txt_OptionsTable.enabled = false;
	}
}
