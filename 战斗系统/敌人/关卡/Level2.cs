using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2:BaseLevel
{
    public Level2()
    {
        id=2;
        enemies.Add(new List<BaseEnemy>{new Normal1(),new Normal1(),new Normal1()});
        enemies.Add(new List<BaseEnemy>{new Normal1(),new Normal1()});
    }
}