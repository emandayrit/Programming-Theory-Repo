#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;

public class OfflineRewards : MonoBehaviour
{
    [SerializeField] double pointsMultiplier = 1.5f;

    private void Awake() => SavePrefs();

    private void Update() => ExitKeyPress();

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LAST_LOGIN",DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    //ENCAPSULATION
    void ExitKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            #if UNITY_EDITOR
            Debug.Log("Call this for editor.");
            UnityEditor.EditorApplication.ExitPlaymode();
            #else
            Debug.Log("Game was quitted.");
            Application.Quit();
            #endif
        }
    }
    
    //ENCAPSULATION
    void SavePrefs()
    {
        if (PlayerPrefs.HasKey("LAST_LOGIN"))
        {
            GameManager.manager.hasSavePref = true;
            DateTime lastLogin = DateTime.Parse(PlayerPrefs.GetString("LAST_LOGIN"));
            TimeSpan span = DateTime.Now - lastLogin;

            GameManager.manager.offlinePoints = span.TotalSeconds * pointsMultiplier;
            Debug.Log($"{GameManager.manager.SetNotate(GameManager.manager.offlinePoints)} Points");
        }
        else
        {
            GameManager.manager.hasSavePref = false;
        }
    }
}
