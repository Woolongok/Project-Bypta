using System.Collections;
using UnityEngine;

[System.Serializable] // Agar bisa terlihat di Inspector Unity
public class Page
{
    public GameObject[] panels; // Panel-panel dalam satu halaman
}

public class MultiplePanelScene : MonoBehaviour
{
    public Page[] scenePages; 
    private int currentPage = 0;
    private int currentPanel = -1;

    private float fadeDuration = 0.5f;

    private bool allPanelsCompleted = false;
    private void Start()
    {
        foreach (var page in scenePages)
        {
            foreach (var panel in page.panels)
            {
                panel.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (allPanelsCompleted)
        {
            return;
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

        // Jika masih ada panel di halaman ini
        if (currentPanel < scenePages[currentPage].panels.Length - 1)
        {
            currentPanel++;
            scenePages[currentPage].panels[currentPanel].SetActive(true);
            StartCoroutine(FadeIn(scenePages[currentPage].panels[currentPanel].GetComponent<SpriteRenderer>()));
        }
        else if (currentPage < scenePages.Length - 1) // Pindah ke halaman berikutnya
        {
            StartCoroutine(TransitionToNextPage());
        }
    }

    private IEnumerator TransitionToNextPage()
    {
        // Fade out semua panel di halaman saat ini
        foreach (var panel in scenePages[currentPage].panels)
        {
            StartCoroutine(FadeOut(panel.GetComponent<SpriteRenderer>()));
        }

        yield return new WaitForSeconds(fadeDuration);

        // Nonaktifkan semua panel setelah fade out
        foreach (var panel in scenePages[currentPage].panels)
        {
            panel.SetActive(false);
        }

        // Pindah ke halaman berikutnya
        currentPage++;
        currentPanel = 0;
        scenePages[currentPage].panels[currentPanel].SetActive(true);
        StartCoroutine(FadeIn(scenePages[currentPage].panels[currentPanel].GetComponent<SpriteRenderer>()));
    }

    private IEnumerator FadeIn(SpriteRenderer sr)
    {
        float time = 0.0f;
        Color initialColor = sr.color;

        // Ambil posisi awal dan target
        Vector3 initialPosition = sr.transform.position;
        initialPosition.y += 1.0f;
        Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y - 1.0f, initialPosition.z);

        // Set alpha ke 0 di awal
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

    private IEnumerator FadeOut(SpriteRenderer sr)
    {
        float time = 0.0f;
        Color initialColor = sr.color;

        // Ambil posisi awal dan target untuk fade out
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
}
