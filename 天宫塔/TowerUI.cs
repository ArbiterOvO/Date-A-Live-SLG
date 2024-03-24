using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerUI : Singleton<TowerUI>
{
    public void returnMain()
    {
        SceneManager.LoadScene("Main");
    }
}
