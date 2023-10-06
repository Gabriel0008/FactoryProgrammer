using System.Collections.Generic;
using UnityEngine;

public class InitialValues : MonoBehaviour
{
    public static InitialValues Instance;
    [SerializeField] public List<Materials> materials = null;
    [SerializeField] public List<MaterialsSO> allmaterials = null;
    List<string> strings = new List<string>();

    [SerializeField] public List<Machines> machines = null;

    [SerializeField] public List<Materials> endMaterials = null;

    public int gridWidth = 10;
    public int gridHeight = 10;

    public float materialInstanceCooldown;
    [HideInInspector]public int totalQuantity;
    public float materialMovSpeed = 5f;

    public List<Materials> GetListMaterials()
    {
        return materials;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        for (int i = 0; i < materials.Count; i++)
        {
            if (materials[i].material.nameString != null && materials[i].quantidade > 0)
            {
                strings.Add("'" + materials[i].material.nameString + "'");
            }
            if(materials != null){
                totalQuantity = totalQuantity + materials[i].quantidade;

            }
        }

        for (int i = 0; i < materials.Count; i++)
        {
            if (materials[i].material.nameString != null && materials[i].quantidade > 0)
            {
                foreach (string name in strings)
                {
                    if (name == materials[i].material.nameString)
                    {
                        break;
                    }
                    strings.Add("'" + materials[i].material.type + "'");
                }

            }
        }


    }

    public List<Materials> GetEndMaterials()
    {
        return endMaterials;
    }

    public int GetTotalEndMaterials()
    {
        int total = 0;
        for(int i = 0; i < endMaterials.Count; i++)
        {
            total += endMaterials[i].quantidade;
        }
        return total;
        
    }

    public List<PlacedObjectTypeSO> GetMachines()
    {
        List<PlacedObjectTypeSO> machineObject = new List<PlacedObjectTypeSO>();
        for(int i = 0; i < machines.Count; i++)
        {
            machineObject.Add(machines[i].machine);
        }
        return machineObject;

    }
    
    public MaterialsSO getMaterialSObyName(string soName)
    {
        for(int i = 0; i< allmaterials.Count; i++)
        {
            if(allmaterials[i].name == soName)
            {
                return allmaterials[i];
            }
        }
        return null;
    }

    public List<string> MaterialsStrings()
    {
        return strings;
    }

}
