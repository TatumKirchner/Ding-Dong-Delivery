using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private float timeToRespawn = 30f;

    private Vector3 posOffset;
    private Vector3 tempPos;

    private GameObject cone;
    private MeshRenderer mesh;
    private AudioSource source;
    private Weapons Weapons;

    [SerializeField] private bool isCone = false;
    [SerializeField] private bool isBrick = false;
    [SerializeField] private bool isAnvil = false;
    public bool pickedUp = false;

    private int weaponIndex;

    private void Start()
    {
        if (isCone)
        {
            cone = gameObject.transform.GetChild(0).gameObject;
            weaponIndex = 1;
        }
        if (isBrick)
            weaponIndex = 2;
        if (isAnvil)
            weaponIndex = 3;

        posOffset = transform.position;        
        source = GetComponent<AudioSource>();
        if (!isCone)
            mesh = GetComponent<MeshRenderer>();
    }

    //Move the pickup
    private void Update()
    {
       transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * movementSpeed) * 0.5f;
        transform.position = tempPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the player drives into the pickup play a sound and add it to the players inventory
        if (other.gameObject.CompareTag("Player") && !pickedUp)
        {
            source.Play();
            Weapons = other.GetComponent<Weapons>();
            Weapons.AddWeapon(1, weaponIndex);
            StartCoroutine(Respawn());
            pickedUp = true;
        }
    }

    //Once the item has been picked up turn it off. After timeToRespawn has elapsed turn it back on.
    IEnumerator Respawn()
    {
        if (isCone)
            cone.SetActive(false);
        if (!isCone)
            mesh.enabled = false;

        yield return new WaitForSeconds(timeToRespawn);
        
        if (isCone)
            cone.SetActive(true);
        if (!isCone)
            mesh.enabled = true;

        pickedUp = false;
    }
}
