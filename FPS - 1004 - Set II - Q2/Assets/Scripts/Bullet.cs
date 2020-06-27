using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    #region Variables
    public int barrelHealth = 2;

    //public GameObject barrel1;
    //public GameObject barrel2;
    //public GameObject barrel3;
    //public GameObject barrel4;

    public int score;
    public GameObject scoreText;

    public GameObject overText;
    #endregion
    private void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score");
    }
    private void Update()
    {
        scoreText.GetComponent<Text>().text = "Score: " + score;
        if(score == 4)
        {
            overText.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Barrel")
        {
            //TakeDamage();
            //if (barrelHealth >= 0)
            //    barrelHealth--;
            //else
            //{
                Destroy(collision.gameObject);
                score++;
            //}
        }
    }

    //void TakeDamage()
    //{
    //    barrelHealth--;
    //}
}
