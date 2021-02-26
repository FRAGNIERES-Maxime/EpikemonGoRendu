using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Classes
{
    public class MobBehaviour : MonoBehaviour
    {
        #region Public props

        /// <summary>
        /// Name of mob
        /// </summary>
        public string name = "Undefined";
        /// <summary>
        /// Speed of mob
        /// </summary>
        public float speed = 1;
        /// <summary>
        /// Size of mob
        /// </summary>
        public float size = 0.75f;
        /// <summary>
        /// Level of mob (impact damage and life)
        /// </summary>
        public int level = 1;
        /// <summary>
        /// Set target of mob
        /// </summary>
        public Transform target;
        public GameObject Spawner;

        #endregion

        #region Private props

        /// <summary>
        /// Get initial life
        /// </summary>
        private int initialLife => (int)(level * 10 * size);
        /// <summary>
        /// Get currentLife
        /// </summary>
        private int currentLife { get; set; }
        private bool death = false;

        #endregion

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            Spawner = GameObject.Find("Spawner");
            currentLife = initialLife;
            gameObject.transform.localScale = new Vector3(size, size, size);
        }

        /// <summary>
        /// Update is called once per frame to move mob into player position
        /// </summary>
        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.LookAt(target);
        }

        /// <summary>
        /// Generate damage of 
        /// </summary>
        /// <returns></returns>
        public int GetDamage()
        {
            return (int)(level * size);
        }

        /// <summary>
        /// Method to lose life of a value
        /// </summary>
        /// <param name="value">Value to lose</param>
        public void LoseLife(int value)
        {
            if (currentLife > 0)
                currentLife -= value;
            if (death == false && currentLife <= 0)
            {
                death = true;
                Spawner.GetComponent<WavesBehaviour>().Score += (10 * Spawner.GetComponent<WavesBehaviour>().actualLevel);
                StartCoroutine(deathAnim());
            }
        }

        IEnumerator deathAnim()
        {
            Vector3 size = gameObject.transform.localScale;
            for (float i = size.x; i > 0; i -= 0.05f)
            {
                gameObject.transform.localScale = new Vector3(i, i, i);
                yield return new WaitForEndOfFrame();
            }
            gameObject.Destroy();
            yield return null;
        }
    }
}
