using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public BaseLevel getLevel()
    {
        switch (GameManager.instance.currentMap)
        {
            case 1:
            return new Level1();
            case 2:
            return new Level2();
        }
        return null;
    }
    public BaseLevel getSpecialLevel()
    {
        switch(GameManager.instance.specialBattleNum)
        {
            case 1:
            return new SpecialLevel1();
            case 2:
            return new SpecialLevel2();
        }
        return null;
    }
    
    void Awake() {
        if(instance != null)
        Destroy(this.gameObject);
        instance=this;
    }
}
