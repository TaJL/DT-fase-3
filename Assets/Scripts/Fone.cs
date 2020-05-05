using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fone : MonoBehaviour {
  public Animator animator;
  public Npc talking;
  public Door door;

  IEnumerator Start () {
    talking.onTalkingStarted += () => { animator.SetBool("is ringing", false); };
    talking.onTalkingFinished += () => { door.Open(); };
    yield return new WaitForSeconds(1);
    animator.SetBool("is ringing", true);
    talking.gameObject.SetActive(true);
  }
}
