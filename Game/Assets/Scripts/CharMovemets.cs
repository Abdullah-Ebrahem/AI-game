using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Profiling;
using UnityEngine;

public class CharMovemets : MonoBehaviour
{
    public GameObject gameOver,win;
    CharStates stats;
    Animator anim;
    public float speed = 5;
    CharacterController Controller;
    Transform cam;
    float gravity = 10;
    float verticalVelocity = 0;
    public float jumpValue = 10;
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharStates>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isSprint = Input.GetKey(KeyCode.LeftShift);
        float sprint = isSprint ? 2.7f : 1;
        if (stats.currentHealth <= 0)
        {
            anim.SetTrigger("die");
            StartCoroutine(GameOver());
        }
        if (LevelManager.instance.score >= LevelManager.instance.scoreLimit)
        {
            win.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        anim.SetFloat("speed", Mathf.Clamp(moveDirection.magnitude, 0, 0.5f) + (isSprint ? 0.5f : 0)) ;
        if (Controller.isGrounded)
        {
            if (Input.GetAxis("Jump") > 0)
                verticalVelocity = jumpValue;
        }
        else
            verticalVelocity -= gravity * Time.deltaTime;
        
        if(moveDirection.magnitude>0.1f)
            {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(horizontal, angle, vertical);
        }
        moveDirection = cam.TransformDirection(moveDirection);
        moveDirection = new Vector3(moveDirection.x * speed*sprint, verticalVelocity, moveDirection.z * speed * sprint);
        Controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            GetComponent<CharStates>().ChangeHealth(20);
            Destroy(other.gameObject);
        }else if (other.CompareTag("Power"))
        {
            StartCoroutine(Powerup());
            Destroy(other.gameObject);
        }
    }
    public void DoAttack() {
        transform.Find("collider").GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(HideCollider());
            } 
    IEnumerator HideCollider()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Find("collider").GetComponent<BoxCollider>().enabled = false;
    }
    IEnumerator GameOver() 
    {
        gameOver.SetActive(false);
        yield return new WaitForSeconds(1f);
        gameOver.SetActive(true);
        Destroy(anim);
        Time.timeScale= 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    IEnumerator Powerup()
    {
        stats.power += 20;
        yield return new WaitForSeconds(10f);
        stats.power -= 20;
    }
}
