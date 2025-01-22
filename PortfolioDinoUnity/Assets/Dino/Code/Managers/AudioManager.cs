using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [TabGroup("Audios List")]
    [SerializeField] List<Audio> audioList = new List<Audio>();

    #region Unity Methods
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region public methods

    public void PlayAudio(string name)
    {
        Audio audio = audioList.Find(x => x.name == name);
        if (audio == null)
        {
            Debug.LogError("Audio not found");
            return;
        }
        
        if (audio.sound3D)
        {
            AudioSource.PlayClipAtPoint(audio.clip, CameraController.Instance.transform.position, audio.volume);
        }
        else
        {
        }
        
    }
    

    #endregion
    
}

[Serializable]
public class Audio
{
    public string name;
    public AudioClip clip;
    public bool sound3D;
    [Range(0f, 1f)] public float volume = 1f;
}