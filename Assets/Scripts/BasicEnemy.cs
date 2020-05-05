using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Tools;

namespace Game {

public class BasicEnemy : MonoBehaviour {
	
// 	public StateMachine stateMachine = new StateMachine();
// 	public PlayerControl player;
//     public int hp;
//     public bool vulnerable;
	
//     public event System.Action EnterStateIdle;
//     public event System.Action EnterStateChase;
//     public event System.Action EnterStateHitted;
//     public event System.Action EnterStateDeath;

// 	void Awake()
//     {
// 		stateMachine.AddState("Idle", enter: Idle_Enter, update: Idle_Update, exit: Idle_Exit)
// 			.To("Chase", () => EnterStateChase, enter: Chase_Enter, update: Chase_Update, exit: Chase_Exit)
// 		.To("Idle", () => EnterStateChasing)
// 			.To("Crouching", () => Input.GetKey(KeyCode.DownArrow), enter: Crouching_Enter, update: Crouching_Update, exit: Crouching_Exit)
// 		.To("Idle", () => !Input.GetKey(KeyCode.DownArrow))
// 		.AnyState("Shooting", () => Input.GetKeyDown(KeyCode.A), enter: Enter_Shooting, update: Update_Shooting, exit: Exit_Shooting).Exit(() => Input.GetKeyUp(KeyCode.A))
// 		;
// 	}
	
// 	void Update() {
// 		stateMachine.Update();
// 	}
	
// 	void FixedUpdate() {
// 		stateMachine.FixedUpdate();
// 	}
	
// 	// -- //
	
// 	void Idle_Enter() {
// 		Debug.Log("Enter to Idle");
// 	}
	
// 	void Idle_Update() {
// 		Debug.Log("I'm idling :)");
// 	}
	
// 	void Idle_FixedUpdate() {
// 		Debug.Log("Fixed idling");
// 	}
	
// 	void Idle_Exit() {
// 		Debug.Log("Exit from Idle");
// 	}
	
 }

}
