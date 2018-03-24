using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.Linq;

public class MainBattleScript : MonoBehaviour
{

    public ReadData readData;
    int amountWarriorInTeam;
    int warriorLeader;
    int amountMonsterEnemy;
    public int[] teamId;
    public int[] enemyId;
    public List<BattleLog> battleLog;
    public Transform[] warriorLocation = new Transform[6];
    public Transform[] enemyLocation = new Transform[6];
    public List<Warrior> team;
    public List<string> enemyIdWave1;
    public List<string> enemyIdWave2;
    public List<string> enemyIdWave3;
    public List<BattleWarrior> allWar;
    public List<BattleWarrior> allWarOrigin;
    public List<BattleWarrior> newList;
    public GameObject soundManager;

    bool once = true;
    public bool result = true;
    public int battleStatus = 0;
    public int endofreplay = 0;

    void Awake()
    {
        if (GameObject.Find("BgMusic") != null)
        {
            Destroy(GameObject.Find("BgMusic"));
        }
    }

    void Start()
    {
        // GameObject.Find("entity_01").transform.Translate(new Vector3(0, 0, 0));

        SetCharactersPosition();

        battleLog = new List<BattleLog>();
        int count = readData.dataUserModel.warrior.Count;
        JsonReader test = new LitJson.JsonReader("test");

        team = readData.dataUserModel.warrior.FindAll(x => x.position > 0);


        string world = "1";
        string area = "1";
        string level = "1";
        string ordinal = "1";


        enemyIdWave1 = GetEnemyWave(world, area, level, ordinal, "enemyId_wave1");

        enemyIdWave2 = GetEnemyWave(world, area, level, ordinal, "enemyId_wave2");

        enemyIdWave3 = GetEnemyWave(world, area, level, ordinal, "enemyId_wave3");

        allWar = new List<BattleWarrior>();
        allWarOrigin = new List<BattleWarrior>();

        SetEnemyToBattleList(enemyIdWave1);
        //random position enemy
        RandomPosition();

        SetWarriorToBattleList();

        

        GenerateSprite();



    }

    void SetEnemyToBattleList(List<string> enemyList)
    {
        BattleWarrior temp = null;
        BattleWarrior temp2 = null;
        for (int i = 0; i < enemyList.Count; i++)
        {
            temp = new BattleWarrior();
            temp2 = new BattleWarrior();
            if (enemyIdWave1[i] != "0")
            {
                temp.setDataEnemy(enemyList[i], readData.jsonEnemyData, readData.jsonEnemySkillData, readData.languageId);
                temp2.setDataEnemy(enemyList[i], readData.jsonEnemyData, readData.jsonEnemySkillData, readData.languageId);



                allWar.Add(temp);
                allWarOrigin.Add(temp2);
            }
        }
    }

    void SetWarriorToBattleList()
    {
        for (int i = 0; i < team.Count; i++)
        {
            BattleWarrior temp = null;
            BattleWarrior temp2 = null;
            temp = new BattleWarrior();
            temp.setDataWarrior(team[i], readData.languageId);
            allWar.Add(temp);

            temp2 = new BattleWarrior();
            temp2.setDataWarrior(team[i], readData.languageId);
            allWarOrigin.Add(temp2);
        }

    }

    private void RandomPosition()
    {
        //random position enemy
        List<int> randomEnemyPosition = new List<int> { 1, 2, 3 };

        for (int i = 0; i < 6; i = i + 2)
        {
            int test2 = randomEnemyPosition[UnityEngine.Random.Range(0, randomEnemyPosition.Count)];

            if (test2 == 1)
            {
                if (allWar.ElementAtOrDefault(i) != null)
                {
                    allWar[i].position = 1;
                    allWarOrigin[i].position = 1;

                }
                if (allWar.ElementAtOrDefault(i + 1) != null)
                {
                    allWar[i + 1].position = 2;
                    allWarOrigin[i + 1].position = 2;
                }
            }
            else if (test2 == 2)
            {
                if (allWar.ElementAtOrDefault(i) != null)
                {
                    allWar[i].position = 3;
                    allWarOrigin[i].position = 3;
                }
                if (allWar.ElementAtOrDefault(i + 1) != null)
                {
                    allWar[i + 1].position = 4;
                    allWarOrigin[i + 1].position = 4;
                }
            }
            else
            {
                if (allWar.ElementAtOrDefault(i) != null)
                {
                    allWar[i].position = 5;
                    allWarOrigin[i].position = 5;
                }
                if (allWar.ElementAtOrDefault(i + 1) != null)
                {
                    allWar[i + 1].position = 6;
                    allWarOrigin[i + 1].position = 6;
                }


            }

            randomEnemyPosition.Remove(test2);

        }
    }

    private List<string> GetEnemyWave(string world, string area, string level, string ordinal, string wave)
    {
        return readData.jsonStageData[area][level][ordinal][wave].ToString().Split(new string[] { "," }, StringSplitOptions.None).ToList();
    }

    private void SetCharactersPosition()
    {
        for (int i = 0; i < warriorLocation.Length; i++)
        {
            string locationName = "Canvas/Warrior" + (i + 1).ToString();
            warriorLocation[i] = GameObject.Find(locationName).GetComponent<Transform>();
        }

        for (int i = 0; i < enemyLocation.Length; i++)
        {
            string locationName = "Canvas/Enemy" + (i + 1).ToString();
            enemyLocation[i] = GameObject.Find(locationName).GetComponent<Transform>();
        }
    }


    private IEnumerator WaitForS()
    {

        //foreach (BattleLog log in battleLog)
        for (int i = 0; i < battleLog.Count; i++)
        {
            yield return new WaitForSeconds(2);
            string tempname = "";
            int position = 0;
            Vector3 position2;
            Animator animator;

            float[] animatetimeAtk = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
            float[] animatetime1 = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
            float[] animatetime2 = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
            float[] animatetime3 = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };

            List<float[]> animateSkillTime = new List<float[]>();
            animateSkillTime.Add(animatetimeAtk);
            animateSkillTime.Add(animatetime1);
            animateSkillTime.Add(animatetime2);
            animateSkillTime.Add(animatetime3);

            int countCombo = 0;

            GameObject monsterBody = null;
            GameObject monsterFile = null;
            BattleWarrior attacker = null;
            Skill skillTemp = null;
            float[] tempAnimateTime = animatetimeAtk;
            int hitAmount = 1;
            string skillName = "Attack!";

            if (battleLog[i].skillId != 0)
            {
                if(battleLog[i].typeOfCharAtk == 1)
                    attacker = allWarOrigin.Find(x => battleLog[i].attackerUserId == x.warriorUserId);
                else
                    attacker = allWarOrigin.Find(x => (battleLog[i].attackerId == x.characterId) && (battleLog[i].positionAtk == x.position));

                skillTemp = attacker.skillList.Find(x => x.skillId == battleLog[i].skillId);

                tempAnimateTime = animateSkillTime[battleLog[i].ordinal];
                skillName = skillTemp.name[0].ToString() + "!";
                hitAmount = skillTemp.hitAmount;

            }
            else
            {

            }

            if (battleLog[i].typeOfCharAtk == 1)
            {
                tempname = "Enemy" + battleLog[i].positionDef;

                position2 = new Vector3(enemyLocation[battleLog[i].positionDef - 1].position.x, enemyLocation[battleLog[i].positionDef - 1].position.y, enemyLocation[battleLog[i].positionDef - 1].position.z);
                Vector3 attackerPosition = new Vector3(warriorLocation[battleLog[i].positionAtk - 1].position.x, warriorLocation[battleLog[i].positionAtk - 1].position.y + 200, warriorLocation[battleLog[i].positionAtk - 1].position.z);

                animator = GameObject.Find("Canvas/Warrior" + battleLog[i].positionAtk + "/entity_01(Clone)").GetComponent<Animator>();
                GameObject.Find("Canvas/Warrior" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().endPosition = GameObject.Find("Canvas/Enemy" + battleLog[i].positionDef).transform;
                GameObject.Find("Canvas/Warrior" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().charType = 1;
                GameObject.Find("Canvas/Warrior" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().mode = 1;
                GameObject.Find("Canvas/Warrior" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().startCastTime = 0;
                monsterBody = GameObject.Find("Canvas");


                GameObject skillNamePrefab = (GameObject)Resources.Load("Prefab/SkillName", typeof(GameObject));
                GameObject tempSkillName = Instantiate(skillNamePrefab, attackerPosition, Quaternion.identity) as GameObject;
                tempSkillName.GetComponent<Text>().text = skillName;
                tempSkillName.transform.parent = monsterBody.transform;
                tempSkillName.transform.localScale = new Vector3(1, 1, 1);
                tempSkillName.transform.localPosition = attackerPosition;
                tempSkillName.GetComponent<DamagePopupScript>().FadeOut();

                yield return new WaitForSeconds(0.25f);

                if (battleLog[i].skillId == 0)
                {
                    animator.Play("Atk");

                }
                else
                {
                    animator.Play("Skill" + battleLog[i].ordinal);
              
                }

                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

                

                if ((i + 1) == battleLog.Count || battleLog[i + 1].attackerUserId != battleLog[i].attackerUserId)
                {
                    //GameObject.Find("Canvas/Warrior" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().startCastTime = 0;
                    GameObject.Find("Canvas/Warrior" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().charType = 1;
                    GameObject.Find("Canvas/Warrior" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().mode = 2;
                    GameObject.Find("Canvas/Warrior" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().startCastTime = 0;
                    //yield return new WaitForSeconds(0.25f);
                }
                animator = GameObject.Find("Canvas/" + tempname + "/entity_01(Clone)").GetComponent<Animator>();
                animator.Play("Hit");

            }
            else
            {
                tempname = "Warrior" + battleLog[i].positionDef;

                position2 = new Vector3(warriorLocation[battleLog[i].positionDef - 1].position.x, warriorLocation[battleLog[i].positionDef - 1].position.y, warriorLocation[battleLog[i].positionDef - 1].position.z);
                Vector3 attackerPosition = new Vector3(enemyLocation[battleLog[i].positionAtk - 1].position.x, enemyLocation[battleLog[i].positionAtk - 1].position.y + 200, enemyLocation[battleLog[i].positionAtk - 1].position.z);
                //animator = GameObject.Find("Canvas/" + tempname + "/entity_01(Clone)").GetComponent<Animator>();
                //animator.Play("Hit");
                //***********
                animator = GameObject.Find("Canvas/Enemy" + battleLog[i].positionAtk + "/entity_01(Clone)").GetComponent<Animator>();
                GameObject.Find("Canvas/Enemy" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().endPosition = GameObject.Find("Canvas/Warrior" + battleLog[i].positionDef).transform;
                GameObject.Find("Canvas/Enemy" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().charType = 2;
                GameObject.Find("Canvas/Enemy" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().mode = 1;
                GameObject.Find("Canvas/Enemy" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().startCastTime = 0;

                monsterBody = GameObject.Find("Canvas");
                GameObject skillNamePrefab = (GameObject)Resources.Load("Prefab/SkillName", typeof(GameObject));
                GameObject tempSkillName = Instantiate(skillNamePrefab, attackerPosition, Quaternion.identity) as GameObject;
                tempSkillName.GetComponent<Text>().text = skillName;
                tempSkillName.transform.parent = monsterBody.transform;
                tempSkillName.transform.localScale = new Vector3(1, 1, 1);
                tempSkillName.transform.localPosition = attackerPosition;
                tempSkillName.GetComponent<DamagePopupScript>().FadeOut();

                yield return new WaitForSeconds(0.25f);

                if (battleLog[i].skillId == 0)
                {
                    animator.Play("Atk");
                }
                else
                {
                    animator.Play("Skill" + battleLog[i].ordinal);
                    if(battleLog[i].ordinal == 3)
                    {
                        Animator animatorEffect = GameObject.Find("Canvas/Warrior1/entity_01(Clone)").GetComponent<Animator>();
                        animatorEffect.SetFloat("light", 1f);
                    }

                }

                yield return new WaitForSeconds(0.25f);

                animator = GameObject.Find("Canvas/" + tempname + "/entity_01(Clone)").GetComponent<Animator>();
                animator.Play("Hit");

                if ((i + 1) == battleLog.Count || battleLog[i + 1].positionAtk != battleLog[i].positionAtk)
                {
                    //GameObject.Find("Canvas/Enemy" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().startCastTime = 0;
                    GameObject.Find("Canvas/Enemy" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().charType = 2;
                    GameObject.Find("Canvas/Enemy" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().mode = 2;
                    GameObject.Find("Canvas/Enemy" + battleLog[i].positionAtk).GetComponent<WarriorStageScript>().startCastTime = 0;
                  
                    //yield return new WaitForSeconds(0.25f);
                }

                
                //yield return new WaitForSeconds(0.25f);
                
            }


            //
            //   float length = animator.GetCurrentAnimatorStateInfo(0).length;

          
             monsterFile = (GameObject)Resources.Load("Prefab/Damage", typeof(GameObject));

            

            

            for (int j = 0; j < hitAmount; j++)
            {


                GameObject tempDmg = Instantiate(monsterFile, position2, Quaternion.identity) as GameObject;
                int realDamage = 0;
                if (battleLog[i].skillId != 0)
                {
                    realDamage = battleLog[i].dmg * Convert.ToInt32(skillTemp.hitDmg[j]) / 100;
                }
                else
                {
                    realDamage = battleLog[i].dmg;
                }
                
                tempDmg.GetComponent<Text>().text = realDamage.ToString();
                tempDmg.transform.parent = monsterBody.transform;

                tempDmg.transform.localScale = new Vector3(1, 1, 1);
                //if (animation["x"].enabled && animation["x"].time == 0)
                //audio.clip = xsound;
                soundManager.GetComponent<SoundManagerScript>().PlayEffect();
                //atk1.Play();
                //tempDmg.transform.localPosition = new Vector3(1, 1, 1);
                tempDmg.GetComponent<DamagePopupScript>().FadeOut();

                HealthbarScript health = GameObject.Find(tempname + "/Healthbar").GetComponent<HealthbarScript>();
                health.hp -= battleLog[i].dmg;
                animator.SetFloat("hp", health.hp);

                yield return new WaitForSeconds(tempAnimateTime[j]);
                //GameObject.Find("Canvas/" + tempname).GetComponent<WarriorStageScript>().mode = 0;
                //Destroy(tempDmg);
                Debug.Log("wait");
            }

            if ((i + 1) == battleLog.Count || battleLog[i + 1].positionAtk != battleLog[i].positionAtk)
            {
                animator.Play("Dead");
            }
        }

        endofreplay = 1;
    }

    private BattleWarrior PickTarget(BattleWarrior character, List<BattleWarrior> listAll)
    {

        BattleWarrior temp = null;
        List<BattleWarrior> listAllTemp;
        int[] setOfposition;

        if (character.typeOfChar == 1)
        {
            listAllTemp = listAll.FindAll(x => (x.hp > 0) && (x.typeOfChar == 2));
        }
        else
        {
            listAllTemp = listAll.FindAll(x => (x.hp > 0) && (x.typeOfChar == 1));
        }


        if (character.position == 1 || character.position == 2)
        {
            setOfposition = new int[] { 1, 2, 3, 4, 5, 6 };
            for (int i = 0; (i < setOfposition.Length) && (temp == null); i++)
            {
                temp = listAllTemp.Find(x => x.position == setOfposition[i]);
            }
        }
        else if (character.position == 5 || character.position == 6)
        {
            setOfposition = new int[] { 5, 6, 3, 4, 1, 2 };

            for (int i = 0; (i < setOfposition.Length) && (temp == null); i++)
            {
                temp = listAllTemp.Find(x => x.position == setOfposition[i]);
            }
        }
        else
        {
            setOfposition = new int[] { 3, 4, 1, 2, 5, 6 };

            for (int i = 0; (i < setOfposition.Length) && (temp == null); i++)
            {
                temp = listAllTemp.Find(x => x.position == setOfposition[i]);
            }
        }

        return temp;
    }

    // Update is called once per frame
    void Update()
    {

        if (once)
        {
            once = false;
            CalculatePhase();
            //SetStageForReplay(1);
            ReplayFirstWave();
            //wait for action
            //ReplaySecondWave();
            //wait for action
            //ReplayThirdthWave();
            //wait for action and result and reward

        }

        if (endofreplay == 1)
        {
            if (battleStatus == 2)
            {
                //Victory
                GameObject.Find("Result").GetComponent<CanvasGroup>().alpha = 1;
                GameObject.Find("Result").GetComponent<CanvasGroup>().interactable = true;
                GameObject.Find("Result").GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else if (battleStatus == 3)
            {
                GameObject.Find("Result").GetComponent<CanvasGroup>().alpha = 1;
                GameObject.Find("Result").GetComponent<CanvasGroup>().interactable = true;
                GameObject.Find("Result").GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else
            {
                //Defeated
                //GameObject.Find("Screen/Defeat").GetComponent<CanvasGroup>().alpha = 1;

            }
        }
       
    }

    public void SkipReplay()
    {
        endofreplay = 1;
    }

    void CalculatePhase()
    {
        List<BattleWarrior> tempDie = new List<BattleWarrior>();
        battleStatus = 0;

        int round = 0;
        for (int i = 1; i <= 1; i++)
        {
            allWar = allWar.OrderByDescending(x => x.spd).ToList();

            while (result)
            {
                //start calulate result
                battleStatus = 1;
                round++;
                Debug.Log("start round " + round);

                foreach (BattleWarrior character in allWar)
                {
                    if (character.hp > 0)
                    {
                        BattleWarrior target = PickTarget(character, allWar);
                        int resultBattle = character.ActionSkill(target, battleLog);

                        if (resultBattle == 1)
                        {
                            tempDie.Add(target);

                        }
                        else if (resultBattle == 2)
                        {
                            tempDie.Add(character);
                        }
                        else if (resultBattle == 4)
                        {

                        }
                        else
                        {
                            tempDie.Add(target);
                            tempDie.Add(character);

                        }

                        //check result
                        if (allWar.FindAll(x => (x.typeOfChar == 1) && (x.hp > 0)).Count == 0)
                        {
                            //lose
                            result = false;
                            battleStatus = 3;
                            break;
                        }
                        else if (allWar.FindAll(x => (x.typeOfChar == 2) && (x.hp > 0)).Count == 0)
                        {
                            //win
                            result = false;
                            battleStatus = 2;
                            break;
                        }
                        else
                        {
                            //continute
                            result = true;
                        }
                    }

                }

                //remove all character for next round
                foreach (BattleWarrior temp3 in tempDie)
                {
                    allWar.Remove(temp3);
                }



                //Limit round
                if (round >= 30)
                {
                    //draw
                    result = false;
                    Debug.Log("draw");
                    battleStatus = 4;
                }
                else if (battleStatus == 1)
                {
                    Debug.Log("continute");
                    Debug.Log("end round " + round);
                }
                else if (battleStatus == 2)
                {
                    Debug.Log("win");

                }
                else if (battleStatus == 3)
                {
                    Debug.Log("lose");
                }



            }

            //clear origin enemy for next wave
            //allWarOrigin.RemoveAll(x => x.typeOfChar == 2);
        }
    }

    public void ReplayFirstWave()
    {
        endofreplay = 0;
        GameObject.Find("Result").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Result").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("Result").GetComponent<CanvasGroup>().blocksRaycasts = false;
        SetStageForReplay(1);
        StartCoroutine(WaitForS());

    }

    void ReplaySecondWave()
    {
        SetStageForReplay(2);
        StartCoroutine(WaitForS());
    }

    void ReplayThirdthWave()
    {
        SetStageForReplay(3);
        StartCoroutine(WaitForS());
    }

    void SetStageForReplay(int waveNo)
    {
        foreach (BattleWarrior war in allWarOrigin)
        {
            string nameobject = "";
            if (war.typeOfChar == 1)
            {
                nameobject = "Canvas/Warrior" + war.position + "/Healthbar";

                Animator animator = GameObject.Find("Canvas/Warrior" + war.position + "/entity_01(Clone)").GetComponent<Animator>();
                animator.Play("Idle");


                if (waveNo == 1)
                {
                    HealthbarScript health = GameObject.Find(nameobject).GetComponent<HealthbarScript>();
                    GameObject.Find(nameobject).GetComponent<Image>().fillAmount = 1;
                    health.hp = war.hp;
                    health.maxHp = war.hp;
                }

            }
            else
            {
                nameobject = "Canvas/Enemy" + war.position + "/Healthbar";

                Animator animator = GameObject.Find("Canvas/Enemy" + war.position + "/entity_01(Clone)").GetComponent<Animator>();
                animator.Play("Idle");

                HealthbarScript health = GameObject.Find(nameobject).GetComponent<HealthbarScript>();
                GameObject.Find(nameobject).GetComponent<Image>().fillAmount = 1;
                health.hp = war.hp;
                health.maxHp = war.hp;
            }

        }
    }

    void GenerateSprite()
    {
        team = team.OrderBy(x => x.position).ToList();
        //foreach(BattleWarrior war in allWar)
        for (int i = 0; i < team.Count; i++)
        {
            string folderName = "Warriors/Yukimura Sanada";
            if(i == 1)
                folderName = "Warriors/Guan Yu";
            //generate monster sprite
            GameObject monsterBody = GameObject.Find("Canvas/Warrior" + team[i].position);
            GameObject monsterFile = (GameObject)Resources.Load(folderName + "/AnimateCharacter/entity_01", typeof(GameObject));

            GameObject monsterEntity;
            if (i == 1)
                monsterEntity = Instantiate(monsterFile, new Vector3(warriorLocation[team[i].position - 1].position.x, warriorLocation[team[i].position - 1].position.y + 90, warriorLocation[team[i].position - 1].position.z), Quaternion.identity) as GameObject;
            else
                monsterEntity = Instantiate(monsterFile, new Vector3(warriorLocation[team[i].position - 1].position.x, warriorLocation[team[i].position - 1].position.y, warriorLocation[team[i].position - 1].position.z), Quaternion.identity) as GameObject;

            GameObject.Find("Canvas/Warrior" + team[i].position + "/stageShadow").SetActive(true);

            monsterEntity.transform.parent = monsterBody.transform;
            if (i == 1)
                monsterEntity.transform.localScale = new Vector3(200, 200, 1);
            else
                monsterEntity.transform.localScale = new Vector3(70, 70, 1);

            // Assigns a material named "Assets/Resources/DEV_Orange" to the object.
            /* Material newMat = Resources.Load(folderName+"/Sanada_Material", typeof(Material)) as Material;
             monsterEntity.GetComponent<Spriter2UnityDX.EntityRenderer>().Material = newMat; */

            if (i == 1)
                monsterEntity.GetComponent<SpriteRenderer>().sortingOrder = team[i].position;
            else
                monsterEntity.GetComponent<Spriter2UnityDX.EntityRenderer>().SortingOrder = team[i].position;
            
            //ChangeSortingLayersRecursively(monsterEntity.transform, "Warrior" + team[i].position);
            monsterBody = GameObject.Find("Canvas/Warrior" + team[i].position);
            monsterFile = (GameObject)Resources.Load("Prefab/Healthbar", typeof(GameObject));

            monsterEntity = Instantiate(monsterFile, new Vector3(warriorLocation[team[i].position - 1].position.x, warriorLocation[team[i].position - 1].position.y, warriorLocation[team[i].position - 1].position.z), Quaternion.identity) as GameObject;


            monsterEntity.transform.parent = monsterBody.transform;
            monsterEntity.name = "Healthbar";
            monsterEntity.transform.localScale = new Vector3(50, 50, 1);
            monsterEntity.transform.localPosition = new Vector3(0, 250, 0);
        }
        /*
        int row = UnityEngine.Random.Range(1, 3);

        if(row == 1)
        {

        }
        */

        List<BattleWarrior> enemyList1 = allWarOrigin.FindAll(x => x.typeOfChar == 2).ToList();

        for (int i = 0; i < enemyList1.Count; i++)
        {

            string name = "zombie";
            string folderName = "Warriors/Yukimura Sanada";
            GameObject monsterBody = GameObject.Find("Canvas/Enemy" + enemyList1[i].position);
            GameObject monsterFile = (GameObject)Resources.Load(folderName + "/AnimateCharacter/entity_01", typeof(GameObject));

            GameObject monsterEntity;
            int positionTemp = enemyList1[i].position - 1;
            monsterEntity = Instantiate(monsterFile, new Vector3(enemyLocation[positionTemp].position.x, enemyLocation[positionTemp].position.y, enemyLocation[positionTemp].position.z), Quaternion.identity) as GameObject;
            GameObject.Find("Canvas/Enemy" + enemyList1[i].position + "/stageShadow").SetActive(true);
            // monsterEntity.name = "Enemy" + enemyList1[i].position;
            // monsterEntity.tag = "Warrior_INSERT";
            monsterEntity.transform.parent = monsterBody.transform;
            // ChangeSortingLayersRecursively(monsterEntity.transform, "Enemy" + enemyList1[i].position);
            monsterEntity.transform.localScale = new Vector3(-70, 70, 1);

            Material newMat = Resources.Load(folderName + "/Sanada_Material", typeof(Material)) as Material;
            monsterEntity.GetComponent<Spriter2UnityDX.EntityRenderer>().Material = newMat;
            monsterEntity.GetComponent<Spriter2UnityDX.EntityRenderer>().SortingOrder = positionTemp;

            monsterBody = GameObject.Find("Canvas/Enemy" + enemyList1[i].position);
            monsterFile = (GameObject)Resources.Load("Prefab/Healthbar", typeof(GameObject));

            monsterEntity = Instantiate(monsterFile, new Vector3(enemyLocation[positionTemp].position.x, enemyLocation[positionTemp].position.y, enemyLocation[positionTemp].position.z), Quaternion.identity) as GameObject;

            monsterEntity.name = "Healthbar";
            monsterEntity.transform.parent = monsterBody.transform;

            monsterEntity.transform.localScale = new Vector3(50, 50, 1);
            monsterEntity.transform.localPosition = new Vector3(0, 250, 0);

        }

    }

    /**
     * Change sorting layer name all sub gameobject in that Transform
     */
    public void ChangeSortingLayersRecursively(Transform trans, string name)
    {
        if (trans.GetComponent<Renderer>() != null)
        {
            trans.GetComponent<Renderer>().sortingLayerName = name;
            // trans.GetComponent<Renderer>().
        }

        foreach (Transform child in trans)
        {
            ChangeSortingLayersRecursively(child, name);
        }
    }

    public void ChangeGameSpeed(int speed)
    {
        Time.timeScale = speed;
    }

    public void GotoHomeScene()
    {
        Application.LoadLevel("Home");
    }
}
