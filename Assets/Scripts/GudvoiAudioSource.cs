using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GudvoiAudioSource : MonoBehaviour {
  public AudioSource speaker;
  public AudioClip sound;
  public Vector2 volumeIncrementRange;
  public Vector2 pitchIncrementRange;

  float _initialVolume;
  float _initialPitch;

  void Start () {
    _initialVolume = GetComponent<AudioSource>().volume;
    _initialPitch = GetComponent<AudioSource>().pitch;
  }

  public void Play () {
    speaker.pitch = _initialPitch + Random.Range(pitchIncrementRange.x,
                                                 pitchIncrementRange.y);
    speaker.volume = _initialVolume + Random.Range(volumeIncrementRange.x,
                                                   volumeIncrementRange.y);
    speaker.PlayOneShot(sound);
  }
}
