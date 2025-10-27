using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health = 100;
    public Animator anim;

    public bool isDead = false;

    [Header("UI")]
    public TextMeshProUGUI healthText;

    [Header("Damage Popup")]
    public GameObject damagePopupPrefab;        // Префаб тексту урону
    public Transform popupSpawnPoint;           // Точка появи
    public Canvas canvas;                       // Canvas, до якого додається popup

    void Start()
    {
        anim = GetComponent<Animator>();
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        // Віднімаємо HP
        health -= damage;

        // Обмеження
        if (health <= 0) health = 0;
        if (health > maxHealth) health = maxHealth;

        UpdateHealthUI();

        // Спливаючий текст урону
        if (damagePopupPrefab != null && popupSpawnPoint != null && canvas != null)
        {
            GameObject popup = Instantiate(damagePopupPrefab, popupSpawnPoint.position, Quaternion.identity, canvas.transform);
            popup.GetComponent<DamagePopup>().Setup((int)damage); // або Mathf.RoundToInt(damage)
        }

        // Смерть
        if (health == 0)
        {
            isDead = true;
            anim.SetTrigger("IsDead");
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + Mathf.RoundToInt(health).ToString();
        }
    }
}