using UnityEngine;
using UnityEngine.UI;

public class ImageStoryManager : MonoBehaviour
{
    public Image storyImage;  // Reference to the UI Image component
    public Sprite[] storySprites;  // Array to hold all story images
    private int currentIndex = 0;  // Index to track the current image

    void Start()
    {
        if (storySprites.Length > 0)
        {
            storyImage.sprite = storySprites[currentIndex];  // Set the first image
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))  // Press right arrow to go to the next image
        {
            NextImage();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))  // Press left arrow to go to the previous image
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
