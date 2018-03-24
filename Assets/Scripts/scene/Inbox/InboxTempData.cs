using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InboxTempData : MonoBehaviour
{
    public int tempI;

    public void ViewMail(){
        GameObject.Find("InboxScript").GetComponent<Inbox>().OpenMailDetailPopup(tempI);
    }
}
