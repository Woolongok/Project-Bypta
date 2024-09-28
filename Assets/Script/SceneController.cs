using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator transitionAnim;
    public GameObject quitMenu;
    public void LoadChapterSelect()
    {
        StartCoroutine(LoadChapterSelectAnimation());
    }
    
    IEnumerator LoadChapterSelectAnimation()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("ChapterSelect");
    }

    public void LoadChapterOne()
    {
        StartCoroutine(LoadChapterOneAnimation());
    }

    IEnumerator LoadChapterOneAnimation()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Chapter 1");
    }

    public void LoadMainStartMenu()
    {
        StartCoroutine(LoadMainStartMenuAnimation());
    }

    IEnumerator LoadMainStartMenuAnimation()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("StartMenu");
    }

    public void CancelQuit()
    {
        if(quitMenu != null) {
            quitMenu.SetActive(false);
        }
    }

    public void Quit()
    {
        StartCoroutine(QuitAnimation());
    }

    IEnumerator QuitAnimation()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }

}
