using System.Collections;
using System.Collections.Generic;
using Assets.Classes;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public Camera camera;
    public GameObject EndPointLightning;
    public GameObject Lightning;
    private bool canDamage = false;
    private bool currentDamage = false;
    private bool colorChange = false;
    private GameObject CurrentMob;

    // Start is called before the first frame update
    void Start()
    {
        Lightning.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward * 100);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Mob")
            {
                Debug.Log("Did Hit");
                CurrentMob = hit.transform.gameObject;
                canDamage = true;
                EndPointLightning.gameObject.transform.position = hit.transform.gameObject.transform.position;
                if (currentDamage == false)
                {
                    StartCoroutine(TouchMob());
                    /*if (colorChange == false)
                        StartCoroutine(ChangeColor(hit.transform.gameObject));*/
                }
                Lightning.gameObject.SetActive(true);
            }
        }
        else
        {
            currentDamage = false;
            canDamage = false;
            CurrentMob = null;
            Lightning.gameObject.SetActive(false);
        }
    }

    IEnumerator TouchMob()
    {
        currentDamage = true;
        GetComponent<AudioSource>().Play();
        while (canDamage)
        {
            CurrentMob.gameObject.GetComponent<MobBehaviour>().LoseLife(10);
            yield return new WaitForSeconds(0.5f);
        }
        GetComponent<AudioSource>().Stop();
        yield return null;
    }

    IEnumerator ChangeColor(GameObject Mob)
    {
        colorChange = true;
        float startTime = Time.time;
        Renderer[] allChildren = Mob.GetComponentsInChildren<Renderer>();
        float T = Time.time * 1.0f;
        foreach(Renderer child in allChildren)
        {
            //T = (startTime - Time.time) * 1.0f;
            child.material.color = Color.Lerp(Color.white, Color.red, 255);
        }
        yield return new WaitForSeconds(0.5f);
        foreach(Renderer child in allChildren)
        {
            //T = (Mathf.Sin(startTime - Time.time) * 1.0f);
            child.material.color = Color.Lerp(Color.red, Color.white, 255);
        }
        yield return new WaitForSeconds(0.5f);
        colorChange = false;
        yield return null;
    }
}

