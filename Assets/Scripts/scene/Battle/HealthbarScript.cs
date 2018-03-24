using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthbarScript : MonoBehaviour {

    Image hpBar;
    public float hp;
    public float maxHp;
    public float decreaseHpSpeed = 0.5f;
	// Use this for initialization
	void Start () {

        hpBar = GetComponent<Image>();

       
	}
	
	// Update is called once per frame
	void Update () {
        
        

        if (hpBar.fillAmount > ((float)hp / (float)maxHp) && hpBar.fillAmount != 0)
        {
            hpBar.fillAmount -= Time.deltaTime * decreaseHpSpeed;
        }

	}

    void damage()
    {

    }
}
