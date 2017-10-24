using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class Behaviour : MonoBehaviour
    {

        public State currentState;
        public Material normal, religious;
        public Renderer rend;

        // Use this for initialization
        void Start()
        {
            currentState = new Normal();
            rend = GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            currentState.Execute(this);
        }

        public void ChangeState(State newState)
        {
            currentState.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }
    }

        public abstract class State
        {
            public abstract void Enter(Behaviour human);

            public abstract void Execute(Behaviour human);

            public abstract void Exit(Behaviour human);
        }

        ////////////////////////////////////////////////States///////////////////////////////////////////

    class Normal : State
    {
        public override void Enter(Behaviour human)
        {
            human.rend.material = human.normal;
        }

        public override void Execute(Behaviour human)
        {
            if (Input.anyKey) human.ChangeState(new Religious());
        }

        public override void Exit(Behaviour human)
        {
            
        }
    }

    class Religious : State
    {
        public override void Enter(Behaviour human)
        {
            human.rend.material = human.religious;
        }

        public override void Execute(Behaviour human)
        {
            if (Input.anyKey) human.ChangeState(new Normal());
        }

        public override void Exit(Behaviour human)
        {
            
        }
    }
}
