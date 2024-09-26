using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator transitionAnim;
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
}
