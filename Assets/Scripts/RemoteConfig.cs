using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using Firebase.Extensions;

public class RemoteConfig : MonoBehaviour
{
    public static RemoteConfig instance;
    

    public bool isBlue;
    
    void Awake()
    {
        
        //Fetch();
        //FetchDataAsync();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    void SetRemoteConfigDefaults()
    {
       
    }

    

    //public Task FetchDataAsync()
    //{
        
    //}

    

    void Start()
    {
        
    }

    void Update()
    {

    }
}
