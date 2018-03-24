using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;

public class WarriorStageScript : MonoBehaviour
{

    public Vector2 startPosition;
    public Transform endPosition;
    public float castingGagaue = 0f;
    public float startCastTime = 0f;
    public float castTimeStop = 0f;
    public float castingTime = 0f;
    public float timegaugeSpeed = 1.0f;
    public int mode = 0;
    public int charType = 0;

    // Use this for initialization
    void Start()
    {
        startPosition = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);
    }

    int CheckMode()
    {
        if (startCastTime < 1)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if(startCastTime <= 1)
            MovementAtk(charType, mode);
        
        

       


    }

    public void MovementAtk(int charType ,int mode)
    {
        int shift = 0;

        if (charType == 1)
            shift = -230;
        else
            shift = 230;

        if (mode == 1)
        {
            startCastTime += Time.deltaTime;
            transform.localPosition = Vector2.Lerp(this.transform.localPosition, new Vector2(endPosition.localPosition.x + shift, endPosition.localPosition.y), startCastTime * 5);
        }
        else if (mode == 2)
        {
            startCastTime += Time.deltaTime;
            transform.localPosition = Vector2.Lerp(this.transform.localPosition, startPosition, startCastTime);
        }
        else
        {
            //stop
        }
    }

}
