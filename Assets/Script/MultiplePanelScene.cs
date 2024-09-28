using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Page
{
    public GameObject[] panels; // Panels within a single page
    public bool isMiniGame = false; // Determines if the page is a mini-game
    public AudioSource pageAudio; // AudioSource assigned to the page
}

public class MultiplePanelScene : MonoBehaviour
{
    public GameObject parentBatteries;
    public GameObject button;
    public Page[] scenePages;
    private int currentPage = 0;
    private int currentPanel = -1;

    private float fadeDuration = 0.5f;

    private bool allPanelsCompleted = false;

    public Animator transitionAnim;
    private AudioSource currentAudioSource; // Keeps track of the currently playing audio

    public AudioSource clickAudioSource; // AudioSource for click sound

    public void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenuDelay());
    }

    IEnumerator LoadMainMenuDelay()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("StartMenu");
    }

    private void Start()
    {
        foreach (var page in scenePages)
        {
            foreach (var panel in page.panels)
            {
                panel.SetActive(false);
            }
        }

        CheckButtonStatus();

        // Start playing the first page's audio at the beginning
        if (scenePages.Length > 0)
        {
            PlayPageAudio(scenePages[currentPage]);
        }
    }

    private void CheckButtonStatus()
    {
        if (scenePages[currentPage].isMiniGame)
        {
            button.SetActive(false); // Disable button if the current page is a mini-game
        }
        else
        {
            button.SetActive(true); // Enable button if the current page is not a mini-game
        }
    }

    void Update()
    {
        if (allPanelsCompleted)
        {
            LoadMainMenu();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPanel();
        }
    }

    public void NextPanel()
    {
        if (allPanelsCompleted)
        {
            return;
        }

        // Play the click audio sound
        if (clickAudioSource != null)
        {
            clickAudioSource.Play();
        }

        // If there are still panels left on the current page
        if (currentPanel < scenePages[currentPage].panels.Length - 1)
        {
            currentPanel++;
            GameObject activePanel = scenePages[currentPage].panels[currentPanel];
            activePanel.SetActive(true);
            StartCoroutine(FadeIn(activePanel.GetComponent<Image>()));
        }
        else if (currentPage < scenePages.Length - 1) // Move to the next page
        {
            StartCoroutine(TransitionToNextPage());
        }
        else
        {
            allPanelsCompleted = true;
        }
    }

    private IEnumerator TransitionToNextPage()
    {
        // Fade out all panels on the current page
        foreach (var panel in scenePages[currentPage].panels)
        {
            Image panelImage = panel.GetComponent<Image>();
            if (panelImage != null)
            {
                StartCoroutine(FadeOut(panelImage));
            }
        }

        yield return new WaitForSeconds(fadeDuration);

        // Disable all panels after fade out
        foreach (var panel in scenePages[currentPage].panels)
        {
            panel.SetActive(false);
        }

        // Move to the next page
        currentPage++;
        currentPanel = 0;
        GameObject firstPanel = scenePages[currentPage].panels[currentPanel];
        firstPanel.SetActive(true);

        Image firstPanelImage = firstPanel.GetComponent<Image>();
        if (firstPanelImage != null)
        {
            StartCoroutine(FadeIn(firstPanelImage));
        }

        // Check and play the next page audio
        PlayNextPageAudio(scenePages[currentPage]);

        CheckButtonStatus();
    }

    private void PlayPageAudio(Page page)
    {
        // Play audio if available; otherwise, retain the current audio
        if (page.pageAudio != null && page.pageAudio.clip != null)
        {
            currentAudioSource = page.pageAudio;
            currentAudioSource.loop = true; // Ensure audio is set to loop
            currentAudioSource.Play();
        }
    }

    private void PlayNextPageAudio(Page nextPage)
    {
        // If the next page has a valid audio clip, stop the current audio and play the new one
        if (nextPage.pageAudio != null && nextPage.pageAudio.clip != null)
        {
            StopCurrentPageAudio(); // Stop previous audio if needed
            PlayPageAudio(nextPage); // Play the audio for the new page
        }
        // If the next page has no audio, the current audio continues playing in loop mode
    }

    private void StopCurrentPageAudio()
    {
        // Stop the current audio source if it exists and is playing
        if (currentAudioSource != null && currentAudioSource.isPlaying)
        {
            currentAudioSource.loop = false; // Set loop to false only when intentionally stopping the audio
            currentAudioSource.Stop();
        }
    }

    private IEnumerator FadeOut(Image sr)
    {
        if (sr == null)
        {
            yield break; // Exit the coroutine if Image is null
        }

        float time = 0.0f;
        Color initialColor = sr.color;
        Vector3 initialPosition = sr.transform.position;
        Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y - 1.0f, initialPosition.z);

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            sr.transform.position = Vector3.Lerp(initialPosition, targetPosition, time / fadeDuration);

            time += Time.deltaTime;
            yield return null;
        }

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        sr.transform.position = targetPosition;
    }

    private IEnumerator FadeIn(Image sr)
    {
        if (sr != null)
        {
            float time = 0.0f;
            Color initialColor = sr.color;
            Vector3 initialPosition = sr.transform.position;
            initialPosition.y += 1.0f;
            Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y - 1.0f, initialPosition.z);

            initialColor.a = 0;
            sr.color = initialColor;

            while (time < fadeDuration)
            {
                float alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
                sr.transform.position = Vector3.Lerp(initialPosition, targetPosition, time / fadeDuration);

                time += Time.deltaTime;
                yield return null;
            }

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            sr.transform.position = targetPosition;
        }
    }
}
