using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamagePopupScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float alphatext = this.GetComponent<Text>().canvasRenderer.GetAlpha();
        if (alphatext <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    public void FadeOut()
    {
        this.GetComponent<Text>().CrossFadeAlpha(0, 1, false);

    }

    public void FadeOut(float duration)
    {
        this.GetComponent<Text>().CrossFadeAlpha(0, duration, false);
      
    }
}
