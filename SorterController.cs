using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorterController : MonoBehaviour
{

    [SerializeField] private float sortingTime = 3f;
    private float _timer;

    public MaterialsSO sortingMaterial;
    [HideInInspector]public bool sorting = false;
    public bool newMaterial = false;


    private void Update()
    {
        if(_timer >= 0)
        {
            _timer = _timer - Time.deltaTime;
        }
        if(_timer < 0)
        {
            sorting = false;
        }

    }

    public Transform transformMaterial()
    {
        switch (sortingMaterial.name)
        {
            case "SOMinCarvao": return InitialValues.Instance.getMaterialSObyName("SOCarvao").prefab;
            case "SOMinOuro": return InitialValues.Instance.getMaterialSObyName("SOOuro").prefab;
            case "SOMinFerro": return InitialValues.Instance.getMaterialSObyName("SOFerro").prefab;
            case "SOMinPrata": return InitialValues.Instance.getMaterialSObyName("SOPrata").prefab;
            default:return null;

        }
    }

    public void SetMaterial(MaterialsSO material)
    {
        sortingMaterial = material;
        _timer = sortingTime;
        sorting = true;
        newMaterial = true;
    }

    public bool isSorting()
    {
        return sorting;
    }



}
