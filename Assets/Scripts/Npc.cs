using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Npc : MonoBehaviour {
  public event System.Action<Decision> onDecisionGiven;

  public bool WillTalk { get => decision == Decision.None; }
  public DialogueEntry[] message;
  public const float LECTURE_TIME_PER_WORD = 0.5f;
  public int current = 0;
  [HideInInspector]
  public Decision decision = Decision.None;
  public GameObject actionIndicator;

  Coroutine _speak;

  public void Speak () {
    if (_speak == null) {
      _speak = StartCoroutine(_Speak());
    }
  }

  public void Stop () {
    StopAllCoroutines();
    _speak = null;
    NpcDialoguePlaceholder.Instance.SetVisibility(false);
    current = 0;
    PlayerDecisions.Instance.Hide();
    PlayerDecisions.onDecisionMade -= HandleDecision;
    actionIndicator.SetActive(false);
  }

  public void IndicateActiveForTalk () {
    actionIndicator.SetActive(WillTalk);
  }

  IEnumerator _Speak () {
    if (NpcDialoguePlaceholder.Instance.IsVisible && current >= message.Length) {
      NpcDialoguePlaceholder.Instance.SetVisibility(false);
      current = 0;
      yield break;
    }

    if (!WillTalk) yield break;

    if (current == 0) {
      NpcDialoguePlaceholder.Instance.SetVisibility(true);
    }

    yield return StartCoroutine(_DisplayMessageLetterByLetter());

    NpcDialoguePlaceholder.Instance.dialogue.text = message[current].message;
    NpcDialoguePlaceholder.Instance.SetTalking(false);

    current++;
    _speak = null;
    if (current >= message.Length) {
      PlayerDecisions.Instance.Activate();
      PlayerDecisions.onDecisionMade += HandleDecision;
    }
  }

  public void HandleDecision (Decision decision, Npc npc) {
    PlayerDecisions.onDecisionMade -= HandleDecision;
    this.decision = decision;
    if (onDecisionGiven != null) onDecisionGiven(decision);
    Speak();
  }

  IEnumerator _DisplayMessageLetterByLetter () {
    bool jump = false;
    Text t = NpcDialoguePlaceholder.Instance.dialogue;
    t.text = "";
    NpcDialoguePlaceholder.Instance.SetTalking(true);
    float elapsed = 0;

    for (int i=0;
         i<message[current].message.Length && !jump;
         i++, jump = Input.GetButtonDown("Fire1")) {

      float x = 0;
      while (x < 0.05f) {
        yield return null;
        x += Time.deltaTime;
        if (Input.GetButtonDown("Fire1")) break;
      }

      t.text += message[current].message[i];
      elapsed += Time.deltaTime;
    }
  }
}
