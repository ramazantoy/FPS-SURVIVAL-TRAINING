using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public FixedJoystick kontroller;
    public bool isMove = false;
    public float speed = 5f;
    public float Camera_speed = 3f;
    Touch finger;
    float camera_x = 0.0f;
    [SerializeField]
    Transform camera;

    void Start()
    {
        
    }

 
    void Update()
    {
        float x = kontroller.Horizontal * Time.deltaTime; ;
        float z = kontroller.Vertical*Time.deltaTime;
        transform.Translate(x*speed, 0, z*speed);
        if(x==0.0f && z == 0.0f)
        {
            isMove = false;
        }
        else
        {
            isMove = true;
        }
        look();
    }
    void look()
    {
        for(int i = 0; i < Input.touchCount; i++)
        {
            finger = Input.GetTouch(i);
            if (finger.phase == TouchPhase.Moved)
            {
                if (isMove == true && finger.fingerId == 1)//ekrana dokunma var hareket ediyorsa ise ikinci parmağı elde etmek için
                {
                    rotate();
                }
                else if (isMove == false && finger.fingerId == 0)//hareket yok ise ilk parmağı alalım
                {
                    rotate();
                }
                    
            }
        }
    }
    void rotate()
    {
        float left_right = finger.deltaPosition.x * Camera_speed * Time.deltaTime;
        float up_down = finger.deltaPosition.y * Camera_speed * Time.deltaTime;
        camera_x -= up_down;
        camera_x = Mathf.Clamp(camera_x, -90.0f, 90.0f);//maksimum dönme miktarı
        camera.localRotation = Quaternion.Euler(camera_x, 0, 0);//cameranın dönmesi
        transform.Rotate(0, left_right, 0);//karakterin dönmesi
    }
}
