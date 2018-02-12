using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSingleton
{
    private static SettingsSingleton instance;

    private SettingsSingleton() { }

    public static SettingsSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SettingsSingleton();
            }
            return instance;
        }
    }


    public string carColor { get; set; }

    public string typeOfCamera { get; set; }

    public bool sound { get; set; }

    public string tempo { get; set; }


}