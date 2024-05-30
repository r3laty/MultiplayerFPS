using UnityEngine;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [Header("UI")]
    [SerializeField] private TextMeshPro healthbarText;

    [PunRPC]
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Good shot");
        healthbarText.text = health.ToString() + " Hp";

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
