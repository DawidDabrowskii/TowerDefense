using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    [SerializeField] private Vector3 positionOffset;

    private GameObject turret;

    private Renderer rend;
    private Color startColor;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown() // it will be called when we press mouse button down
    {
        if (turret != null)
        {
            Debug.Log("cant build there");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    private void OnMouseEnter() // it will be called everytime mouse passes collider
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit() // call after mouse will exit collider
    {
        rend.material.color = startColor;
    }
}

