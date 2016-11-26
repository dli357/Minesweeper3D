using UnityEngine;
using System.Collections;
using System;

public class changeAnimCubeMat : StateMachineBehaviour {
    GameObject animCube;
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    public Material mat5;
    public Material mat6;
    public Material mat7;
    public Material mat8;
    public Material mat9;
    public Material mat10;
    private int currentMatIndex = 0;

    void Awake() {
        animCube = GameObject.Find("AnimationCube");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Material[] mats = { mat1, mat2, mat3, mat4, mat5, mat6, mat7, mat8, mat9, mat10};
        System.Random rand = new System.Random();
        int randomInt = rand.Next(10);
        while (randomInt == currentMatIndex) {
            randomInt = rand.Next(10);
        }
        currentMatIndex = randomInt;
        animCube.GetComponent<Renderer>().material = mats[randomInt];
    }
}
