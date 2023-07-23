#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;

public class OfflineRewards : MonoBehaviour
{
    private string noSave = "You have no save yet. But Welcome!";
    private string withSave = "Welcome back! Your points earned: ";
    private double pointsMultiplier = 1f;

    private void Start()
    {
        SavePrefs();
    }

    private void Update()
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

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LAST_LOGIN",DateTime.Now.ToString());
        Debug.Log($"Closing the program {Time.time}");
    }

    void SavePrefs()
    {
        if (PlayerPrefs.HasKey("LAST_LOGIN"))
        {
            DateTime lastLogin = DateTime.Parse(PlayerPrefs.GetString("LAST_LOGIN"));
            TimeSpan span = DateTime.Now - lastLogin;

            Debug.Log(withSave + $"{span.TotalSeconds * pointsMultiplier}/Points");
        }
        else
        {
            Debug.Log(noSave);
        }
    }
}
