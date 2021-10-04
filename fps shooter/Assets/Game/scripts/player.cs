using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _gravity = 9.81f;
    public GameObject MuzzleFlash;
    public GameObject HitMarker;
    public AudioSource ShootSound;
    public int CurrentAmmo;
    public int MaxAmmo = 50;
    private bool isReloading = false;
    private uimanager uimanager;
    public bool HasCoin=false;
    public GameObject Weapon;
    // Start is called before the first frame update
    void Start()
    {
        uimanager = GameObject.Find("Canvas").GetComponent<uimanager>();
        _controller = GetComponent<CharacterController>();
        CurrentAmmo = MaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        if(Weapon.activeInHierarchy && Input.GetMouseButton(0) && CurrentAmmo > 0)
        {
            Shoot();
        }
        else
        {
            MuzzleFlash.SetActive(false);
            ShootSound.Stop();
        }
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }
    void Shoot()
    {
        MuzzleFlash.SetActive(true);
        CurrentAmmo--;
        uimanager.UpdateAmmoCount(CurrentAmmo);
        if (ShootSound.isPlaying == false)
        {
            ShootSound.Play();
        }
        Ray rayorigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitinfo;
        if (Physics.Raycast(rayorigin, out hitinfo))
        {
            Debug.Log("hit something" + hitinfo.transform.name);
            GameObject hitmarker = Instantiate(HitMarker, hitinfo.point, Quaternion.identity) as GameObject;
            Destroy(hitmarker, 1f);

            CrateBehaviour Crate = hitinfo.transform.GetComponent<CrateBehaviour>();
            if (Crate != null)
            {
                Crate.DestroyCrate();
            }
        }
    }
    void movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = direction * _speed;
        velocity = transform.transform.TransformDirection(velocity);
        velocity.y -= _gravity;
        _controller.Move(velocity * Time.deltaTime);
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        CurrentAmmo = MaxAmmo;
        uimanager.UpdateAmmoCount(CurrentAmmo);
        isReloading = false;
    }
    public void EnableWeapon()
    {
        Weapon.SetActive(true);
    }
}
