using UnityEngine;

public class CableManager : MonoBehaviour
{
    public GameObject button;
    public DragAndDropLimited[] cables; // Masukkan semua kabel yang perlu dicek
    public bool allCableHasPlugIn = false; // Boolean untuk cek apakah semua kabel sudah terhubung

    void Update()
    {
        CheckAllCables();
    }

    // Metode untuk mengecek apakah semua kabel sudah terhubung
    private void CheckAllCables()
    {
        allCableHasPlugIn = true; // Set awal sebagai true, nanti bisa jadi false jika ada yang belum terhubung

        foreach (var cable in cables)
        {
            if (!cable.isDone) // Jika ada kabel yang belum tercolok, maka set allCableHasPlugIn menjadi false
            {
                allCableHasPlugIn = false;
                break; // Hentikan loop, karena cukup satu kabel yang belum tercolok untuk menyatakan false
            }
        }

        // Debug atau lakukan sesuatu ketika semua kabel sudah terhubung
        if (allCableHasPlugIn)
        {
            Debug.Log("Semua kabel sudah tercolok!");
            button.SetActive(true);
        }
    }
}
