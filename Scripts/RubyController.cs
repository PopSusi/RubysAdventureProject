using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float walkSpeed;
    public float speed;
    public float runSpeed;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float horDir = Input.GetAxis("Horizontal");
        float verDir = Input.GetAxis("Vertical");

        Vector2 position = transform.position;

        position.x = position.x + speed * horDir * Time.deltaTime;
        position.y = position.y + speed * verDir * Time.deltaTime;
        transform.position = position;
    }
}
