using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] effectSound;
    public AudioClip[] backgroundSound;
    public AudioSource[] sources;

    public void SoundPlay(int source,int clip)
    {
        sources[source].clip = effectSound[clip];
        sources[source].Play();
    }

}
