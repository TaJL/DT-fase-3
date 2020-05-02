using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Npc : MonoBehaviour {
  public string message;
  public const float LECTURE_TIME_PER_WORD = 0.5f;

  public void Say () {
    StopAllCoroutines();
    StartCoroutine(_Say());
  }

  IEnumerator _Say () {
    bool jump = false;
    Text t = NpcDialoguePlaceholder.Instance.dialogue;
    t.text = "";
    NpcDialoguePlaceholder.Instance.SetTalking(true);
    float elapsed = 0;

    for (int i=0; i<message.Length && !jump; i++, jump = Input.GetButtonDown("Fire1")) {
      yield return null;
      t.text += message[i];
      elapsed += Time.deltaTime;
    }

    yield return new
      WaitForSeconds(LECTURE_TIME_PER_WORD * message.Split(' ').Length);
    
    NpcDialoguePlaceholder.Instance.SetTalking(false);
  }
}
