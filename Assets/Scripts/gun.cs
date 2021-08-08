using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gun : MonoBehaviour
{
    public ParticleSystem fire_effect;
    public GameObject ammo;
    Coroutine fire;
    Animator a;
    Transform cam;
    bool isFire = true;
    int magazine = 3;
    int magazine_ammo = 15;
    TextMeshProUGUI ammo_txt;
    List<GameObject> Ammo_pool;

    void Start()
    {
        fire_effect.Stop();
        a = GetComponent<Animator>();
        cam = Camera.main.GetComponent<Transform>();
        ammo_txt = GameObject.Find("ammo").GetComponent<TextMeshProUGUI>();
        Ammo_pool = new List<GameObject>();
        ammo_txt.text = magazine_ammo + " / " + magazine;
        for(int i = 0; i < 20; i++)
        {
            GameObject y_ammo = Instantiate(ammo);
            y_ammo.SetActive(false);
            Ammo_pool.Add(y_ammo);
        }

        
    }
    public void  firee()
    {
        if (magazine_ammo > 0)
        {
            isFire = !isFire;
            //Debug.Log(isFire);
            if (isFire == false)
            {
                fire = StartCoroutine(letsFire());
                fire_effect.Play();
                a.SetBool("fire",true);
                
            }
            else
            {
               
               StopCoroutine(fire);
                fire_effect.Stop();
                a.SetBool("fire", false);
            }
        }

    }
    IEnumerator letsFire()
    {
        if (magazine_ammo > 0 && isFire==false)
        {
            for(int i = 0; i < Ammo_pool.Count; i++)
            {
                if (Ammo_pool[i].activeSelf == false && isFire==false)
                {
                    Ammo_pool[i].SetActive(true);
                    Ammo_pool[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Ammo_pool[i].transform.position = cam.position;
                    Ammo_pool[i].GetComponent<Rigidbody>().AddForce(cam.transform.forward * 20.0f, ForceMode.Impulse);

                    StartCoroutine(ammo_false(Ammo_pool[i]));
                    magazine_ammo--;
                    ammo_txt.text = magazine_ammo + " / " + magazine;
                    break;
                }
                else if(isFire==false)
                {
                    GameObject y_ammo = Instantiate(ammo,cam.position,Quaternion.identity);
                    Ammo_pool.Add(y_ammo);
                    y_ammo.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 20.0f, ForceMode.Impulse);
                    StartCoroutine(ammo_false(y_ammo));
                    magazine_ammo--;
                    ammo_txt.text = magazine_ammo + " / " + magazine;
                    break;

                }
            }
            yield return new WaitForSeconds(0.2f);
            yield return letsFire();
        }
        else
        {
            isFire = false;
          StopCoroutine(fire);
            fire_effect.Stop();
            a.SetBool("fire", false);
        }

    }
    IEnumerator ammo_false(GameObject ammo)
    {
        yield return new WaitForSeconds(3.0f);
        if (ammo.activeSelf == true)
        {
            ammo.SetActive(false);
        }
    }
    public void reload()
    {
        if (magazine > 0)
        {
            a.SetTrigger("reload");
            magazine--;
            magazine_ammo = 15;
            ammo_txt.text = magazine_ammo + " / " + magazine;
        }
    }


  
}
