using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1:BaseLevel
{
    public Level1()
    {
        id=1;
        enemies.Add(new List<BaseEnemy>{new Normal1()});
        enemies.Add(new List<BaseEnemy>{new Normal1(),new Normal1()});
    }
}
