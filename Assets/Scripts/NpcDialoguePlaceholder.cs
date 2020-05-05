using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NpcDialoguePlaceholder : NonPersistantSingleton<NpcDialoguePlaceholder> {
  public Text dialogue;
  public Animator animator;
  public Animator placeholder;
  public bool IsVisible { get => placeholder.GetBool("visible"); }

  public void SetVisibility (bool value) {
    placeholder.SetBool("visible", value);
  }

  public void SetTalking(bool value) {
    if (animator) {
      animator.SetBool("is talking", value);
    }
  }

  public void Say (DialogueEntry entry) {
    StopAllCoroutines();
    StartCoroutine(_Say(entry));
  }

  public IEnumerator _Say (DialogueEntry entry) {
    if (!NpcDialoguePlaceholder.Instance.IsVisible) {
      NpcDialoguePlaceholder.Instance.SetVisibility(true);
    }

    yield return StartCoroutine(_DisplayMessageLetterByLetter(entry));
  }

  public IEnumerator _DisplayMessageLetterByLetter (DialogueEntry entry) {
    bool jump = false;
    dialogue.text = "";
    SetTalking(true);

    for (int i=0; i<entry.message.Length && !jump;
         i++, jump = Input.GetButtonDown("Fire1")) {
      float x = 0;
      while (x < 0.05f) {
        yield return null;
        x += Time.deltaTime;
        if (Input.GetButtonDown("Fire1")) break;
      }

      dialogue.text += entry.message[i];
    }

    dialogue.text = entry.message;
    SetTalking(false);
  }

}
