using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float lifeTime = 1f;
    private float timer;
    private TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        timer = lifeTime;
    }

    public void Setup(int damage)
    {
        text.text = "-" + damage.ToString();
    }

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}