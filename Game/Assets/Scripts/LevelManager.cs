using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform player;
    public NavMeshAgent agent;
    public int score = 0, levelItems, scoreLimit;
    public Transform[] Particles;
    public Transform MainCanvas;



    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    

}


