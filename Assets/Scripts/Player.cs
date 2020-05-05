using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : NonPersistantSingleton<Player> {
  public TalkativePlayer dialogues;
  public AttackablePlayer attackable;
}
