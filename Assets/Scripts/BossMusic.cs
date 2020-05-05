using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossMusic : MonoBehaviour {
  public static event System.Action onMusicOver;
  public AudioSource music;
  public float timeToDecrease = 3;

  void OnEnable () {
    Npc.onFightStarted += HandleFightStarted;
    Events.OnBossDeath += HandleBossDeath;
  }

  void OnDisable () {
    Npc.onFightStarted -= HandleFightStarted;
    Events.OnBossDeath -= HandleBossDeath;
  }

  public void HandleFightStarted (Npc npc) { music.Play(); }
  public void HandleBossDeath (Npc npc) { StartCoroutine(_SmoothStop()); }

  IEnumerator _SmoothStop () {
    float elapsed = 0;
    float initial = music.volume;

    while (elapsed < timeToDecrease) {
      elapsed += Time.deltaTime;
      music.volume = Mathf.Lerp(initial, 0, elapsed / timeToDecrease);
      yield return null;
    }

    music.Stop();
    if (onMusicOver != null) onMusicOver();
  }
}
