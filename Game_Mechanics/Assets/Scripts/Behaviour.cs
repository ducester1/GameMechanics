using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour {

    public string state;
    public bool infection = false;
	// Use this for initialization
	void Start () {
        state = "human";

	}
	
	// Update is called once per frame
	void Update () {

        if(infection == true) state = "zombie";
        switch (state)
        {
            case "human":
                HumanBehabiour();
                break;

            case "zombie":
                ZombieBehaviour();
                break;

            default: break;
        }
		if(state == "human")
        {

        }
	}

    void HumanBehabiour()
    {

    }

    void ZombieBehaviour()
    {

    }
}
