using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkTestingScript : MonoBehaviour {


	public int NumberOfConnections = 5;
	public string IP = ""; // 80.232.86.193
	public int Port = 25001; // tcp only i guess // 25001
	public string TestString = "Dette ble sendt fra client";

	public GameObject MultiplayerObject;
	public GameObject DisconnectPnl;
	public GameObject ConnectedPnl;
	public Text ConnectionLogger;
	public Button ServerSpawnerBtn;

	public InputField IP_Input;
	public InputField Port_Input;

	void Start()
	{

		IP_Input.text = IP;
		Port_Input.text = Port.ToString();

		if(Network.peerType == NetworkPeerType.Disconnected)
		{
			DisconnectPnl.SetActive(true);
	
		}else
		{
			Debug.Log("Start() --> you are still connected ?");
		}
	}

	void Update()
	{
		if (Network.peerType == NetworkPeerType.Server)
		{
			ConnectionLogger.text = "Connections " + Network.connections.Length;
		}
		if(Network.peerType == NetworkPeerType.Client) // Connection needs time so i need to put this in Update
		{
			ConnectionLogger.text = "I am Client"; 
			DisconnectPnl.SetActive(false);
			ConnectedPnl.SetActive(true);
		}
	}

	public void ConnectToHost()
	{
		Debug.Log("IP = " + IP);
		Debug.Log("Port = " + Port);
		Network.Connect(IP,Port);
		//Debug.Log("I AM DISCONNECTED --> CONNECTING TO IP( " + IP + ", " + port + ")");

		/*if(Network.peerType == NetworkPeerType.Disconnected)
		{
			Debug.Log("Failed to connect");
			ConnectionLogger.text = ConnectionLogger.text +  "\n Connection failed";
			return;
		}*/

		if(Network.peerType == NetworkPeerType.Client) // dosent work connection needs time ....
		{
			DisconnectPnl.SetActive(false);
			ConnectedPnl.SetActive(true);
			Debug.Log("I AM CLIENT");
			ConnectionLogger.text = ConnectionLogger.text +  "\n Connection Succses you are client";
		}
	}

	public void InitServer()
	{
		Network.InitializeServer(NumberOfConnections, Port , true);

		if(Network.peerType == NetworkPeerType.Disconnected)
		{
			Debug.Log("Failed to create server");
			ConnectionLogger.text = ConnectionLogger.text +  "\n Failed Host";
			return;
		}
		else if (Network.peerType == NetworkPeerType.Server)
		{
			DisconnectPnl.SetActive(false);
			ConnectedPnl.SetActive(true);
			ServerSpawnerBtn.gameObject.SetActive(true);
			//ConnectionLogger.text = ConnectionLogger.text +  "\n Succses host";
			Debug.Log("I AM SERVER");
		}
	}


	public void LogOut()
	{
		Network.Disconnect(250);
		ConnectedPnl.SetActive(false);
		DisconnectPnl.SetActive(true);
	
		ConnectionLogger.text = ConnectionLogger.text +  "\n Logged out";

	}

	public void ChangeIP(string newIP)
	{
		IP = newIP;
	}

	public void ChangePort(string newPort)
	{
		Port = int.Parse(newPort);
	}


	public void spawnCube()
	{
		Vector3 test = new Vector3( Random.Range(-2, 5) ,Random.Range(-2, 5) ,Random.Range(-2, 5) );

		Network.Instantiate(MultiplayerObject , test  ,Quaternion.identity, 0);
	}

	/*void OnGUI() // this shitt updates more then void Update()
	{
		if(Network.peerType == NetworkPeerType.Disconnected)
		{
			if(GUI.Button(new Rect(100,100,100,25), "Start Client"))
			{
				Debug.Log(" Connecting to = " + IP + " " + port);
				Network.Connect(IP,port);
				Debug.Log(Network.TestConnection());

			}
			if(GUI.Button(new Rect(100,125,100,25), "Start Server"))
			{
				Network.InitializeSecurity();
				Network.InitializeServer(5, port, true);
			}
			Debug.Log("TEST TEST");
		}
		else
		{
			if(Network.peerType == NetworkPeerType.Client)
			{
				GUI.Label(new Rect(100,100,100,25), "client");
				if(GUI.Button(new Rect(100,125,100,25) , "LogOut"))
				{
					Network.Disconnect(250);
				}
			}
			if(Network.peerType == NetworkPeerType.Server)
			{
				GUI.Label(new Rect(100,100,100,25), "server");
				GUI.Label(new Rect(100,125,100,25),"Connections " + Network.connections.Length);

				if(GUI.Button(new Rect(100,150,100,25), "Logout"))
				{
					Network.Disconnect(250);
				}
			}

			Debug.Log(" ELS ELSADLS");
		}
	}*/
}





	