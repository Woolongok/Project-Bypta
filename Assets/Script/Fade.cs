using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private float duration = 1f; // Durasi fade
    private SpriteRenderer sr;

    // Start is called before the first frame update
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        // Mulai dengan transparansi (alpha) 0 untuk fade in
        Color initialColor = sr.color;
        initialColor.a = 0f;
        sr.color = initialColor;
    }

    private void Update()
    {
        // Mulai fade in saat tombol panah kanan ditekan
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(FadeInPanel());
        }
    }

    IEnumerator FadeInPanel()
    {
        float time = 0.0f;

        // Mengubah alpha dari 0 ke 1 selama durasi yang ditentukan
        while (time < duration)
        {
            float alpha = Mathf.Lerp(0.0f, 1.0f, time / duration);
            Color newColor = sr.color;
            newColor.a = alpha;
            sr.color = newColor;

            time += Time.deltaTime; // Increment waktu dengan deltaTime
            yield return null; // Tunggu hingga frame berikutnya
        }

        // Pastikan alpha diatur ke 1 setelah selesai
        Color finalColor = sr.color;
        finalColor.a = 1f;
        sr.color = finalColor;
    }
}
