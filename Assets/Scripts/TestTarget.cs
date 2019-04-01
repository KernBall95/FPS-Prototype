using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour {

    public int hitPoints = 3;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = Color.green;
    }

    void ApplyDamage(int damage)
    {
        if (hitPoints <= 0)
            return;

        hitPoints -= damage;

        if (hitPoints == 2)
            rend.material.color = Color.yellow;
        else if (hitPoints == 1)
            rend.material.color = Color.red;

        if (hitPoints <= 0) 
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
