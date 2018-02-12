using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

    public GameObject panelPreferences, panelMain, panelTimes, canvas;
    public Toggle sound;
    public Dropdown color, camera;
    public Text record;

    public void ShowPreferences(bool enable)
    {
        
        panelPreferences.SetActive(enable);


        if (enable)
        {
            ShowMainMenu(false);
        }
        else
        {
            ShowMainMenu(true);
        }

    }

    public void ShowTimes(bool enable)
    {

        panelTimes.SetActive(enable);


        if (enable)
        {
            ShowMainMenu(false);
        }
        else
        {
            ShowMainMenu(true);
        }

    }
  

    public void ShowMainMenu(bool enable)
    {
        panelMain.SetActive(enable);
        record.text = SettingsSingleton.Instance.tempo;
    }

    public void ApplyChanges() 
    {
        SetColor();
        SetSound();
        SetCamera();
        ShowPreferences(false);
        ShowTimes(false);
        ShowMainMenu(true);               
    }

    public void PlayGame()
    {
        canvas.SetActive(false);
        SceneManager.LoadScene("track");
        SetSound();
    }

    public void SetColor()
    {
        SettingsSingleton.Instance.carColor = color.options[color.value].text;
    }
    public void SetSound()
    {
        SettingsSingleton.Instance.sound = sound.isOn;
    }
    public void SetCamera()
    {
        SettingsSingleton.Instance.typeOfCamera = camera.options[camera.value].text;
    }

    public void exit()
    {
        Application.Quit();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
