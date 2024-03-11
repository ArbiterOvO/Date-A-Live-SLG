using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;

public class Role : MonoBehaviour
{
    //正常状态
    public Image roleImage;
    //表情
    public Image roleEmote;
    public SerializedDictionary<int,SerializedDictionary<string,Sprite>> roleImages;

}
