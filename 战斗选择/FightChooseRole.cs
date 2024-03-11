using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightChooseRole : MonoBehaviour
{
    public BaseRole role;
    public Image image;
    public Slider bloodSlider;
    public Text bloodText;

    void Update() {
        changeUI();
    }
    void changeUI()
    {
        bloodSlider.value=role.Blood/role.MaxBlood;
        bloodText.text=((int)(role.Blood+0.5)).ToString();
    }
}
