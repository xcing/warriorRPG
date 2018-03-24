using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class Login : MonoBehaviour {
    public string userId;
    public ReadData readData;

	// Use this for initialization
	void Start () {
        userId = "20150504101235123451";
        StartCoroutine(readData.GetDataUserOnService(userId, true));
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("test = " + readData.dataUserModel.mail[0].subject[0] + " , " + readData.dataUserModel.mail[0].detail[0]);
	}

    public void LoginButton()
    {
        
    }

    public void StartGame()
    {
        Application.LoadLevel("Home");
    }
}
