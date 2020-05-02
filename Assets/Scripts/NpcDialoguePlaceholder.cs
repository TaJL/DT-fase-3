using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NpcDialoguePlaceholder : NonPersistantSingleton<NpcDialoguePlaceholder> {
  public Text dialogue;
  public Animator animator;

  public void SetTalking(bool value) {
    if (animator) {
      animator.SetBool("is talking", value);
    }
  }
}
