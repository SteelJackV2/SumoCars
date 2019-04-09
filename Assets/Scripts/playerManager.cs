using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class playerManager : NetworkBehaviour
{
    public float respawntime = 3f;
    public int CarDamage = 10;
    public Text score;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SerializeField]
    private Collider[] collideOnDeath;
    private bool[] collwasEnabled;

    public void Begin()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        collwasEnabled = new bool[collideOnDeath.Length];
        for (int i = 0; i < collwasEnabled.Length; i++)
        {
            collwasEnabled[i] = collideOnDeath[i].enabled;
        }
        setDefaults();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcDie();
        }

    }

    public void setSlider(float val)
    {
        //time.value = val;
    }


    [ClientRpc]
    public void RpcAddDamge()
    {
        CarDamage--;
        if (CarDamage <= 0)
        {
            CarDamage = 10;
            RpcDie();

        }

    }
    public int getDamage()
    {
        return CarDamage;
    }

    [ClientRpc]
    private void RpcDie()
    {
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        for (int i = 0; i < collideOnDeath.Length; i++)
        {
            collideOnDeath[i].enabled = false;
        }

        StartCoroutine(reSpawn());
    }


    private IEnumerator reSpawn()
    {
        yield return new WaitForSeconds(respawntime);
        setDefaults();
        Debug.Log("Respawned");
        Transform startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = startPoint.position;
        transform.rotation = startPoint.rotation;

    }

    public void setDefaults()
    {

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        for (int i = 0; i < collideOnDeath.Length; i++)
        {
            collideOnDeath[i].enabled = collwasEnabled[i];
        }

    }
    
}