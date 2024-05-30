using Photon.Pun;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float bulletsCount = 30;
    [Space]
    [SerializeField] private Camera myCam;
    [Header("VFX")]
    [SerializeField] private GameObject fireVFX;

    private float _nextFire;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //bulletsCount--;

            Fire();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            bulletsCount = 30;
        }
    }
    private void Fire()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

        Debug.Log("Fire");
        Ray ray = myCam.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit rayHit, 1000f))
        {
            Debug.Log(rayHit.collider.transform.position + " hit collider position" +
                "/n" + rayHit.collider.name + " hit collider name");

            PhotonNetwork.Instantiate(fireVFX.name, rayHit.collider.transform.position, Quaternion.identity);

            if (rayHit.collider.gameObject.CompareTag("Player"))
            {
                rayHit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }

    }
}
