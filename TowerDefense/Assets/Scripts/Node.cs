using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown() // it will be called when we press mouse button down
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (turret != null)
        {
            Debug.Log("cant build there");
            return;
        }

        buildManager.BuildTurretOn(this);
        
    } 

     private void OnMouseEnter() // it will be called everytime mouse passes collider
    {
        if (!buildManager.HasMoney)
        {
            rend.material.color = notEnoughMoneyColor;
        }
        else
        {          
            rend.material.color = hoverColor;
        }

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;   

    }
    private void OnMouseExit() // call after mouse will exit collider
    {
        rend.material.color = startColor;
    }
}

