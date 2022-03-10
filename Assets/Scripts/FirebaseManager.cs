using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;

    private FirebaseApp app;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            CheckForUpdatedPlayServices();
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void CheckForUpdatedPlayServices()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                app = FirebaseApp.DefaultInstance;

                Debug.Log("Firebase is ready!!!");

            }
            else
            {
                Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }
}
