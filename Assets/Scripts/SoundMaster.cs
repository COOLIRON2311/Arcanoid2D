using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dolbit normalno
/// </summary>
public class SoundMaster : MonoBehaviour
{
    public static SoundMaster instance { get; private set; }
    public AudioSource sfx;
    public AudioSource bgm;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
