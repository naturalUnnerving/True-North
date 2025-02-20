//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AudioRefSO", menuName = "AudioRefSO", order = 0)]
public class AudioRefSO : ScriptableObject 
{
    [Header("Battle Sounds")]
    public AudioClip bearRoar;

    [Header("Menu Sounds")]
    public AudioClip mouseHover;

    public AudioClip mouseClick;

    public AudioClip startClick;
}
