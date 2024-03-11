using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleControl : MonoBehaviour,IPointerClickHandler
{
    public BaseRole role;
    public Text text;
    public Image roleImage;
    public GameObject background;//选中后active
    bool isChoosed;
    public List<Sprite> roleImages;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < ApartmentUI.Instance.roleSet.transform.childCount; i++)
        {
            ApartmentUI.Instance.roleSet.transform.GetChild(i).GetComponent<RoleControl>().isChoosed=false;
        }
        isChoosed=true;
        ApartmentUI.Instance.chosenRole=role;
    }
    public void updateUI()
    {
        if(role!=null)
        {
            text.text=role.Name;
            roleImage.sprite=roleImages[role.Id];
            background.SetActive(isChoosed);
        }
    }
    
}
