using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound instance { get; private set; }

    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource effect;

    [SerializeField] AudioClip[] bgms;

    private void Awake()
    {
        instance= this;
    }
    public void PlayBgm(int index)
    {
        bgm.clip = bgms[index];
        bgm.Play();
    }
    public void PlayJump()
    {
        effect.Play();
    }
}
