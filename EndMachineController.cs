using System;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;


public class EndMachineController : MonoBehaviour
{
    private Materials endMaterial;
    private MaterialsSO newMaterial;
    private int _flag = 0;
    [SerializeField] PlacedObject_Done placedObject_Done;

    [SerializeField] private Image imagem;

    private void Start()
    {
        imagem.sprite = ItemAssets.Instance.GetSpriteMaterial(endMaterial.material.name);
        FinalValues.Instance.OnFinishGame += FinishingGame;
    }


    private void FinishingGame(object sender, EventArgs e)
    {
        if( _flag < endMaterial.quantidade / 2)
        {
            FinalValues.Instance.failure = true;
        }
    }


    public void SetEndMaterial (Materials end)
    {
        endMaterial = end;
    }

    public void SetNewMaterial (MaterialsSO material)
    {
        newMaterial = material;
        CompareMaterials();
    }

    private void CompareMaterials()
    {
        if(newMaterial.nameString == endMaterial.material.nameString && endMaterial.material.defective == false)
        {
            FinalValues.Instance.SetCorrectEndValue();
            
            UtilsClass.CreateWorldTextPopup("Nice!", GridBuildingSystem3D.Instance.GridToWorldPosition(placedObject_Done.GetOrigin()));
            _flag++;
        }
        else
        {
            FinalValues.Instance.SetWrongEndValue();
            UtilsClass.CreateWorldTextPopup("Wrong!", GridBuildingSystem3D.Instance.GridToWorldPosition(placedObject_Done.GetOrigin()));
        }

    }


}
