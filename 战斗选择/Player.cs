using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void movePlayer(Transform target,EventType eventType)
    {
        StartCoroutine(move(target,eventType));  
    }
    IEnumerator move(Transform target,EventType eventType)
    {
        while(target.position!=transform.position)
        {
            Debug.Log(1);
            transform.position=Vector3.MoveTowards(transform.position,target.position, 5f*Time.deltaTime);
            yield return 0;
        }
        switch(eventType)
        {
            case EventType.战斗:
            FightChooseManager.Instance.enterFight();
            ++FightChooseManager.Instance.fightLayerNum;
            break;
            case EventType.回复:
            FightChooseManager.Instance.healAll();
            break;
            case EventType.事件:
            FightChooseManager.Instance.randomEvent();
            break;
            case EventType.BOSS:
            FightChooseManager.Instance.enterFight();
            ++FightChooseManager.Instance.fightLayerNum;
            break;
        }
        
    }
}
