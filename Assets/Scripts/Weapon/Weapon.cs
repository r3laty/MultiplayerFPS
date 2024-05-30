using Photon.Pun;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float bulletsCount = 30;
    [SerializeField] private float fireRate = 10;
    [Space]
    [SerializeField] private Camera myCam;
    [Header("VFX")]
    [SerializeField] private GameObject fireVFX;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI bulletCounter;
    [Header("Animation")]
    [SerializeField] private Animation reloadAnimation;
    [SerializeField] private AnimationClip reloadAnimationClip;
    [Header("Recoil")]
    [SerializeField] private float recoverPersent = 0.7f;
    [Space]
    [SerializeField] private float recoilUp = 1;
    [SerializeField] private float recoilBack = 0;

    private Vector3 _originalPosition;
    private Vector3 _recoilVelocity = Vector3.zero;

    private float _recoilLenght;
    private float _recoverLenght;

    private bool _recoiling;
    private bool _recovering;

    private float _nextFire;
    private void Awake()
    {
        _originalPosition = transform.localPosition;
    }

    private void Start()
    {
        _recoilLenght = 0;
        _recoverLenght = 1 / fireRate * recoverPersent;
    }

    private void Update()
    {
        if (_nextFire > 0)
        {
            _nextFire -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && _nextFire <= 0 && bulletsCount <= 30 && bulletsCount > 0 && !reloadAnimation.isPlaying)
        {
            _nextFire = 1 / fireRate;

            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (_recoiling)
        {
            Recoil();
        }

        if (_recovering)
        {
            Recover();
        }
    }
    private void Reload()
    {
        bulletsCount = 30;
        reloadAnimation.Play(reloadAnimationClip.name);
    }
    private void Fire()
    {
        bulletsCount--;
        UpdateAmmoText();

        _recoiling = true;
        _recovering = false;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = myCam.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit rayHit, 1000f))
        {
            Debug.Log(rayHit.collider.name + " hit collider name");

            PhotonNetwork.Instantiate(fireVFX.name, rayHit.collider.transform.position, Quaternion.identity);

            if (rayHit.collider.gameObject.CompareTag("Player"))
            {
                rayHit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }
    private void Recoil()
    {
        Vector3 finalPosition = new Vector3(_originalPosition.x, _originalPosition.y + recoilBack, _originalPosition.z - recoilBack);

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref _recoilVelocity, _recoilLenght);

        if (transform.localPosition != finalPosition)
        {
            _recoiling = false;
            _recovering = true;
        }
    }
    private void Recover()
    {
        Vector3 finalPosition = _originalPosition;

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref _recoilVelocity, _recoverLenght);

        if (transform.localPosition != finalPosition)
        {
            _recoiling = false;
            _recovering = false;
        }
    }
    public void UpdateAmmoText()
    {
        bulletCounter.text = bulletsCount.ToString() + "/30";
    }
}