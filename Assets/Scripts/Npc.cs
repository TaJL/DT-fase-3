using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Npc : MonoBehaviour {
  public event System.Action onTalkingStarted;
  public event System.Action onTalkingFinished;
  public static event System.Action<Npc> onFightTriggered;
  public static event System.Action<Npc> onFightStarted;
  public event System.Action<Decision> onDecisionGiven;
  public static int counter = 0;

  public int fontSize = -1;
  public Font font;
  public bool requiresDecision = true;
  public bool HasBeenRead { get => PlayerPrefs.HasKey("npc" + _id); }
  public int FirstMessage { get => HasBeenRead? message.Length-1: 0; }
  public bool WillTalk { get => decision == Decision.None; }
  public DialogueEntry[] message;
  public DialogueEntry madMessage;
  public const float LECTURE_TIME_PER_WORD = 0.5f;
  public int current = 0;
  [HideInInspector]
  public Decision decision = Decision.None;
  public GameObject actionIndicator;
  public Decision requiredDecision;

  public AttackableNpc attackable;
  public RuntimeAnimatorController animController;

  bool _stop = false;
  Coroutine _speak;
  int _id = -1;

  void OnEnable () {
    _id = ++counter;
    current = FirstMessage;
  }

  public void Speak () {
    if (_speak == null) {
      if (current == 0 && onTalkingStarted != null) onTalkingStarted();
      _speak = StartCoroutine(_Speak());
    }
  }

  public void Stop () {
    if (decision != Decision.None && decision != requiredDecision) {
      _stop = true;
    } else {
      StopAllCoroutines();
    }
    _speak = null;
    NpcDialoguePlaceholder.Instance.SetVisibility(false);
    current = FirstMessage;
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
      current = FirstMessage;
      yield break;
    }

    if (!WillTalk) yield break;

    if (current == FirstMessage) {
      NpcDialoguePlaceholder.Instance.SetVisibility(true);
    }

    NpcDialoguePlaceholder.Instance.animator.runtimeAnimatorController = animController;
    NpcDialoguePlaceholder.Instance.dialogue.font = font;
    if (fontSize > 0) {
      NpcDialoguePlaceholder.Instance.dialogue.fontSize = fontSize;
    }
    yield return StartCoroutine(NpcDialoguePlaceholder.Instance.
                                _DisplayMessageLetterByLetter(message[current]));

    current++;
    _speak = null;
    if (current >= message.Length) {
      if (requiresDecision) {
        PlayerDecisions.Instance.Activate();
        PlayerDecisions.onDecisionMade += HandleDecision;
      }
      if (onTalkingFinished != null) onTalkingFinished();
    }
  }

  public void HandleDecision (Decision decision, Npc npc) {
    if (requiredDecision != decision) {
      StartCoroutine(_EventuallyStartFighting());
    } else {
      if (Events.OnBossDeath != null) Events.OnBossDeath(this);
    }
    actionIndicator.SetActive(false);
    PlayerDecisions.onDecisionMade -= HandleDecision;
    this.decision = decision;
    if (onDecisionGiven != null) onDecisionGiven(decision);
    Speak();
    // PlayerPrefs.SetString("npc" + _id, "DONE");
  }

  IEnumerator _EventuallyStartFighting () {
    yield return null;
    if (onFightTriggered != null) onFightTriggered(this);
    yield return StartCoroutine(NpcDialoguePlaceholder.Instance._Say(madMessage));
    yield return new WaitUntil(() => {
        if (Input.GetButtonDown("Fire1")) Stop();
        return _stop;
      });
    if (onFightStarted != null) onFightStarted(this);
    yield return new WaitForSeconds(0.25f);
    attackable.gameObject.SetActive(true);
  }
}
