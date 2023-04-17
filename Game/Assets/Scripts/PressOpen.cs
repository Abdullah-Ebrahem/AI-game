using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressOpen : MonoBehaviour
{
    public GameObject Instruction;
    public GameObject AnimeObject;
    public bool Action = false;
    // Start is called before the first frame update
    void Start()
    {
        Instruction.SetActive(false);
    }
     void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag=="Player")
        {
            Instruction.SetActive(true);
            Action = true;
        }
    }
     void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false);
        Action = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                Instruction.SetActive(true);
                AnimeObject.GetComponent<Animator>().Play("OpenDoor");
                Action = false;
            }
        }
    }
}
