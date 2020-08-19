using System;
using System.Diagnostics;

namespace TakuLib.StateMachine {
	public class StateMachine<T> {
		
		public State<T> currentState { get; private set; }
		public T Owner;
		public StateMachine(T _owner) {
			Owner = _owner;
			
			currentState = null;
		}

		public void ChangeState(State<T> _newState) {
			if (currentState != null)
				currentState.ExitState(Owner);
			currentState = _newState;
			currentState.EnterState(Owner);
		}

		public void Update() {
			if (currentState != null)
				currentState.UpdateState(Owner);
		}

		public void FixedUpdate() {
			if (currentState != null)
				currentState.FixedUpdate(Owner);
		}
	}

	[Serializable]
	public abstract class State<T>
	{
		protected string stateName;
		public virtual State<T> createInstance() {return null; }
		public virtual string GetStateName() => this.GetType().Name;
		public abstract void EnterState(T _owner);
		public abstract void ExitState(T _owner);
		public abstract void UpdateState(T _owner);
		public abstract void FixedUpdate(T _owner);
	}
}
