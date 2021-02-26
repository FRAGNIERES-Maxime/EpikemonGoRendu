using System.Collections;
using System.Collections.Generic;
using Assets.Classes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    /// <summary>
    /// Property to store player life points
    /// </summary>
    public int initialLife = 150;
    /// <summary>
    /// Property to store player life points
    /// </summary>
    private int currentLife;

    /// <summary>
    /// Method called when Player object is loaded
    /// </summary>
    void Start()
    {
        currentLife = initialLife;
    }

    /// <summary>
    /// Method to trigger when enemy hit
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mob"))
        {
            var mob = other.GetComponent<MobBehaviour>();
            if (mob != null)
            {
                LoseLife(mob.GetDamage());
            }
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// Method to lose life of a value
    /// </summary>
    /// <param name="value">Value to lose</param>
    public void LoseLife(int value)
    {
        currentLife -= value;
        if (currentLife <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
