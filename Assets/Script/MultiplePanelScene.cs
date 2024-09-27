using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplePanelScene : MonoBehaviour
{
    public GameObject[] storySprites;
    private int currentIndex = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextImage();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousImage();
        }
    }

    public void NextImage()
    {
        if (currentIndex <= storySprites.Length - 1)
        {
            storySprites[currentIndex].gameObject.SetActive(true);
            currentIndex++;
        }
    }

    public void PreviousImage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            storySprites[currentIndex].gameObject.SetActive(false);
        }
    }
}
