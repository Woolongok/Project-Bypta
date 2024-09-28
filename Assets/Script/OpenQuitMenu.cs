using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenQuitMenu : MonoBehaviour
{
    public GameObject quitMenu;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) quitMenu.SetActive(true);
    }
}
