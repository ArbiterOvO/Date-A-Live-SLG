using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public abstract class BaseLevel
{
    public int id;
    public List<List<BaseEnemy>> enemies=new List<List<BaseEnemy>>();
    
}
