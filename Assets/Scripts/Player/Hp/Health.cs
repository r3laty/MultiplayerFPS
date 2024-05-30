using UnityEngine;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviour
{
    [HideInInspector] public bool IsLocalPlayer;

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
            if (IsLocalPlayer)
            {
                NetworkManager.Instance.SpawnPlayer();
            }

            Destroy(gameObject);
        }
    }
}
