﻿using UnityEngine;
using System.Collections;

public class SMBEnemy : SMBCharacter {

	private SMBConstants.EnemyState _state;

	void Start() {

		_state = SMBConstants.EnemyState.Move;
	}

	// Update is called once per frame
	void Update () {

		if (_state == SMBConstants.EnemyState.Dead)
			return;

		SMBConstants.MoveDirection side = SMBConstants.MoveDirection.Forward;
		if(isFlipped())
			side = SMBConstants.MoveDirection.Backward;
		
		Move (xSpeed * (float)side);
	}

	void Die() {

		_body.velocity = Vector2.zero;
		_collider.applyHorizCollision = false;

		gameObject.layer = LayerMask.NameToLayer ("Ignore Raycast");

		_state = SMBConstants.EnemyState.Dead;

		_animator.SetTrigger ("triggerDie");
		Invoke ("DestroyEnemy", 0.5f);
	}

	void DestroyEnemy() {

		Destroy (gameObject);
	}

	override protected void OnHalfVerticalCollisionEnter(Collider2D collider) {
				
		_body.velocity.x = 0f;

		float side = (float)SMBConstants.MoveDirection.Forward;
		if(isFlipped())
			side = (float)SMBConstants.MoveDirection.Backward;

		transform.position = transform.position - Vector3.right * side * 0.01f;

		_renderer.flipX = !_renderer.flipX;

		base.OnHalfVerticalCollisionEnter (collider);
	}

	void OnHorizontalCollisionEnter(Collider2D collider) {

		if(collider.tag != "Player")
			_renderer.flipX = !_renderer.flipX;
	}
}
