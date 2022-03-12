using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.RemoteConfig;


public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager _instance;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;

    private FirebaseApp m_app;
    public bool isBlue;

    Single _time;
    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames;
    private float fps;

    private void Awake()
    {
        _time = Time.time;
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
        if (_instance == null)
        {
            _instance = this;
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
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                m_app = FirebaseApp.DefaultInstance;
                InitializeRemoteConfigAndSetDefaultValues();
                FetchDataAsync();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });

        
    }

    void InitializeRemoteConfigAndSetDefaultValues()
    {
        Dictionary<string, object> defaults = new Dictionary<string, object>();

        defaults.Add("isBlue", false);

        FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults).ContinueWithOnMainThread(task => 
        {
            Debug.Log("RemoteConfig configured and ready!");
        });
    }

    public Task FetchDataAsync()
    {
        Debug.Log("Fetching data...");
        Task fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
        Debug.LogError(lastInterval);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }


    void FetchComplete(Task fetchTask)
    {
        if (fetchTask.IsCanceled)
        {
            Debug.Log("Fetch canceled.");
        }
        else if (fetchTask.IsFaulted)
        {
            Debug.Log("Fetch encountered an error.");
        }
        else if (fetchTask.IsCompleted)
        {
            Debug.Log("Fetch completed successfully!");
        }

        var info = FirebaseRemoteConfig.DefaultInstance.Info;
        switch (info.LastFetchStatus)
        {
            case LastFetchStatus.Success:
                FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
                .ContinueWithOnMainThread(task => {
                    Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
                                   info.FetchTime));
                });

                break;
            case LastFetchStatus.Failure:
                switch (info.LastFetchFailureReason)
                {
                    case FetchFailureReason.Error:
                        Debug.Log("Fetch failed for unknown reason");
                        break;
                    case FetchFailureReason.Throttled:
                        Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
                        break;
                }
                break;
            case LastFetchStatus.Pending:
                Debug.Log("Latest Fetch call still pending.");
                break;
        }
    }

    private void Start()
    {
        Fetch();
    }

    public void Fetch()
    {
        isBlue = FirebaseRemoteConfig.DefaultInstance.GetValue("isBlue").BooleanValue;
    }

    private void Update()
    {
        //++frames;
        //float timeNow = Time.realtimeSinceStartup;
        //if (timeNow > lastInterval + updateInterval)
        //{
        //    fps = (float)(frames / (timeNow - lastInterval));
        //    frames = 0;
        //    lastInterval = timeNow;
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
