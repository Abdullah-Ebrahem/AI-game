using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections;

public class CharStates : MonoBehaviour
{
    [SerializeField]
    float maxHealth = 100;
    public float power = 10;
    int killScore=200;

    public float currentHealth { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void ChangeHealth(float value)
    {
        //currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        Debug.Log("Current Health" + currentHealth + "/" + maxHealth);
        if (transform.CompareTag("Enemy"))
        
            transform.Find("Canvas").GetChild(1).GetComponentInChildren<UnityEngine.UI.Image>().fillAmount = currentHealth / maxHealth;
        else if (transform.CompareTag("Player"))
            {
                 LevelManager.instance.MainCanvas.Find("PnlStats").Find("ImageHealthBar").GetComponent<UnityEngine.UI.Image>().fillAmount = currentHealth / maxHealth;
                LevelManager.instance.MainCanvas.Find("PnlStats").Find("TxtHealth").GetComponent<TextMeshProUGUI>().text =
                    string.Format("{0:0.##} %", (currentHealth / maxHealth) * 100);
           
        }


        if (currentHealth <= 0)
            Die();
    }
    void Die()
    {
        if (transform.CompareTag("Player"))
        {
            //game over
        }
        else if (transform.CompareTag("Enemy"))
        {
            //destroy enemy
            // GetComponent<EnemyController>().EnemyDie();
            // StartCoroutine(EnemyDestroy());
            Destroy(gameObject);
            LevelManager.instance.score += killScore;
            Debug.Log(LevelManager.instance.score);
            LevelManager.instance.MainCanvas.Find("PnlStats").Find("ScoreTxt").GetComponent<TextMeshProUGUI>().text =
                    string.Format("{0:0.##}", (LevelManager.instance.score));
        }
    }
   /* IEnumerator EnemyDestroy()
    {
        yield return new WaitForSeconds(2f);
       Destroy(gameObject);
        
    }*/
}
