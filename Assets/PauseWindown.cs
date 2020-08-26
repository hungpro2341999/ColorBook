using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseWindown : Windown
{
    public const string Key_Sound = "Sound";
    public const string Key_Music = "Music";
    public List<Transform> ButtonSound = new List<Transform>();
    public List<Transform> ButtonMusic = new List<Transform>();

    public override void Event_Close()
    {
        GameManager.Ins.isGamePause = false;
    }
    public override void Event_Open()
    {
        ApplySetting();
        GameManager.Ins.isGamePause = true;
    

    }


    public void Resume()
    {
        GameManager.Ins.CloseSingleWindown(this.type);
    }
    public void Home()
    {

    }
    public void ApplySetting()
    {
        if (isSound())
        {

            ButtonSound[0].gameObject.SetActive(true);
            ButtonSound[1].gameObject.SetActive(false);
        }
        else
        {
            ButtonSound[0].gameObject.SetActive(false);
            ButtonSound[1].gameObject.SetActive(true);
        }

        if (isMusic())
        {
            AudioCtrl.Ins.Play("BG");
            ButtonMusic[0].gameObject.SetActive(true);
            ButtonMusic[1].gameObject.SetActive(false);
        }
        else
        {
            AudioCtrl.Ins.Mute("BG");
            ButtonMusic[0].gameObject.SetActive(false);
            ButtonMusic[1].gameObject.SetActive(true);
        }
    }
    public void ChangeSound()
    {
        if (isSound())
        {
            PlayerPrefs.SetInt(Key_Sound, 0);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt(Key_Sound, 1);
            PlayerPrefs.Save();
        }

        ApplySetting();
    }
    public void ChangeMucis()
    {
        if (isMusic())
        {

            PlayerPrefs.SetInt(Key_Music, 0);
            PlayerPrefs.Save();
        }
        else
        {

            PlayerPrefs.SetInt(Key_Music, 1);
            PlayerPrefs.Save();

        }
        ApplySetting();
    }
    public bool isSound()
    {
        if (PlayerPrefs.GetInt(Key_Sound) == 1)
        {
            return true;
        }
        return false;
    }

    public bool isMusic()
    {
        if (PlayerPrefs.GetInt(Key_Music) == 1)
        {
            return true;
        }
        return false;
    }

  
}
