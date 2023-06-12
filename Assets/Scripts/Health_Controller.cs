using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Controller : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float damage = 10f;
    // [SerializeField] private float heal = 10f;
    // [SerializeField] private float healRate = 1f;
    private Image healthBar;


    // Start is called before the first frame update
    void Awake()
    {
     Transform healthBarTransform = transform.Find("background/InnerBar");
        if (healthBarTransform != null)
        {
            Debug.Log("Found health bar");
            healthBar = healthBarTransform.GetComponent<Image>();
            healthBar.fillAmount = health / 100f;
        }

        if (healthBar == null)
        {
            Debug.Log("Could not find health bar");
        }


    }

    // Update is called once per frame
    void Update()
    {
        takeDamage(damage * Time.deltaTime);
    }

    void takeDamage(float damage)
    {
            health -= damage;
            healthBar.fillAmount = health / 100f;
            Debug.Log("Health: " + health);
    }
}
