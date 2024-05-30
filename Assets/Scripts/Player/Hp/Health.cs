using UnityEngine;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviour
{
    [HideInInspector] public bool IsLocalPlayer;

    [SerializeField] private int health;
    [Header("UI")]
    [SerializeField] private TextMeshPro gameHealthbarText;
    [SerializeField] private TextMeshProUGUI menuHealthbarText;

    [PunRPC]
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Good shot");
        gameHealthbarText.text = health.ToString() + " Hp";
        menuHealthbarText.text = health.ToString() + "/100";

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
