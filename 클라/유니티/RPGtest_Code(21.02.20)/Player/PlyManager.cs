using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Equip = null;
    [SerializeField] private bool onWeapon = false;

    private Rigidbody plyRigi;
    private Animator plyAni;

    private void Start()
    {
        plyRigi = GetComponent<Rigidbody>();
        plyAni = GetComponent<Animator>();
    }

    public Rigidbody returnPlyRigi()
    {
        return plyRigi;
    }

    public Animator returnPlyAni()
    {
        return plyAni;
    }

    public GameObject returnEquip(int i)
    {
        return Equip[i];
    }

    public bool returnWeaponBool()
    {
        return onWeapon;
    }

    public void setWeapon(bool b)
    {
        onWeapon = b;
    }
}
