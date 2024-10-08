using UnityEngine;
using UnityEngine.UI;

public class ImageStoryManager : MonoBehaviour
{
    public Image storyImage;
    public Sprite[] storySprites;
    private int currentIndex = 0;

    void Start()
    {
        if (storySprites.Length > 0)
        {
            storyImage.sprite = storySprites[currentIndex];
        }
    }

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
        if (currentIndex < storySprites.Length - 1)
        {
            currentIndex++;
            storyImage.sprite = storySprites[currentIndex];
        }
    }

    public void PreviousImage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            storyImage.sprite = storySprites[currentIndex];
        }
    }
}
