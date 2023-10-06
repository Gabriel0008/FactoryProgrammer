using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMovment : MonoBehaviour
{

    private Vector2Int nextPosition;
    PlacedObject_Done myPlacedObject;
    PlacedObject_Done myNextPlacedObject;
    PlacedObjectTypeSO myTypeSOInArray;
    PlacedObjectTypeSO myNextTypeSOInArray;

    [SerializeField] public MaterialsSO material;

    private Vector3 NextWorldPosition;
    private bool imMoving = false;
    private bool illDie = false;

    public bool leftLeaning = false;
    public bool rightLeaning = false;
    public bool backLeanning = false;
    public bool newWay = false;
    public Vector2Int rightNextPosition;
    public Vector3 initialPosition;
    public void OpenMaterialPanelThroughClick()
    {
        UI_MaterialPanel uI_MaterialPanel = GameObject.Find("UI_Player").GetComponent<UI_MaterialPanel>();
        if (UI_MaterialPanel.isOpen == false)
        {
            uI_MaterialPanel.OpenMaterialPanel(material);
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        if (newWay == true)
        {
            imMoving = true;
            myTypeSOInArray = GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(GridBuildingSystem3D.Instance.GetGridPosition(initialPosition));
            myPlacedObject = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(GridBuildingSystem3D.Instance.GetGridPosition(initialPosition));
            nextPosition = myPlacedObject.GetOrigin() + myPlacedObject.GetForwardVector();
            NextWorldPosition = transform.position;
            myPlacedObject.occupied = true;

        }
        else
        {

            if (leftLeaning == false && rightLeaning == false && backLeanning == false)
            {
                myTypeSOInArray = GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(GridBuildingSystem3D.Instance.GetGridPosition(transform.position));
                myPlacedObject = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(GridBuildingSystem3D.Instance.GetGridPosition(transform.position));
                nextPosition = myPlacedObject.GetOrigin() + myPlacedObject.GetForwardVector();
                NextWorldPosition = transform.position;
                myPlacedObject.occupied = true;
            }
            else if (leftLeaning == true)
            {
                myTypeSOInArray = GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(GridBuildingSystem3D.Instance.GetGridPosition(transform.position));
                myPlacedObject = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(GridBuildingSystem3D.Instance.GetGridPosition(transform.position));
                string direcao = myPlacedObject.DirToString();
                nextPosition = myPlacedObject.GetOrigin() + getLeft(direcao);
                myPlacedObject.occupied = true;
            }
            else if (rightLeaning == true)
            {
                myTypeSOInArray = GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(GridBuildingSystem3D.Instance.GetGridPosition(transform.position));
                myPlacedObject = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(GridBuildingSystem3D.Instance.GetGridPosition(transform.position));
                string direcao = myPlacedObject.DirToString();
                nextPosition = rightNextPosition;
                myPlacedObject.occupied = true;
            }
            else if (backLeanning == true)
            {
                myTypeSOInArray = GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(GridBuildingSystem3D.Instance.GetGridPosition(transform.position));
                myPlacedObject = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(GridBuildingSystem3D.Instance.GetGridPosition(transform.position));
                string direcao = myPlacedObject.DirToString();
                nextPosition = rightNextPosition;
                myPlacedObject.occupied = true;
            }
        }


    }
    

    // Update is called once per frame
    void Update()
    {

        FinalValues.Instance.materialActive();

        if (!GridBuildingSystem3D.Instance.CheckPosition(nextPosition))
        {


            myNextTypeSOInArray = GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(nextPosition);
            if (CheckEsteira(myNextTypeSOInArray, nextPosition, myPlacedObject.GetOrigin()) && imMoving == false)
            {
                myNextPlacedObject = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(nextPosition);
                myNextPlacedObject.occupied = true;
                myPlacedObject.occupied = false;
                imMoving = true;
                illDie = false;
            }

            if (CheckNextMachine(myNextTypeSOInArray, nextPosition, "IFMachine")|| CheckNextMachine(myNextTypeSOInArray, nextPosition, "While") && imMoving == false)
            {
                myNextPlacedObject = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(nextPosition);
                myNextPlacedObject.occupied = true;
                myPlacedObject.occupied = false;
                imMoving = true;
                illDie = true;

            }

            if (CheckNextMachine(myNextTypeSOInArray, nextPosition, "End") && imMoving == false)
            {
                myNextPlacedObject = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(nextPosition);
                myNextPlacedObject.occupied = true;
                myPlacedObject.occupied = false;
                imMoving = true;
                illDie = true;

            }
            if (CheckNextMachine(myNextTypeSOInArray, nextPosition, "Switch") && imMoving == false)
            {
                myNextPlacedObject = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(nextPosition);
                myNextPlacedObject.occupied = true;
                myPlacedObject.occupied = false;
                imMoving = true;
                illDie = true;

            }
            if (CheckNextMachine(myNextTypeSOInArray, nextPosition, "Sorter") && imMoving == false)
            {

                if (this.gameObject.GetComponent<MaterialMovment>().material.name == "SOMinCarvao" ||
                   this.gameObject.GetComponent<MaterialMovment>().material.name == "SOMinOuro" ||
                   this.gameObject.GetComponent<MaterialMovment>().material.name == "SOMinFerro" ||
                   this.gameObject.GetComponent<MaterialMovment>().material.name == "SOMinPrata")
                {

                    if (!GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(nextPosition).gameObject.GetComponent<SorterController>().isSorting())
                    {
                        myNextPlacedObject = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(nextPosition);
                        myNextPlacedObject.occupied = true;
                        myPlacedObject.occupied = false;
                        imMoving = true;
                        illDie = true;
                    }

                }
                else
                {
                    Debug.Log("Material nao classificavel!");
                }

            }
            if (CheckNextMachine(myNextTypeSOInArray, nextPosition, "Smelter") && imMoving == false)
            {

                if (this.gameObject.GetComponent<MaterialMovment>().material.name == "SOFerro" ||
                   this.gameObject.GetComponent<MaterialMovment>().material.name == "SOCarvao" ||
                   this.gameObject.GetComponent<MaterialMovment>().material.name == "SOOuro" ||
                   this.gameObject.GetComponent<MaterialMovment>().material.name == "SOPrata")
                {
                myNextPlacedObject = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(nextPosition);
                myNextPlacedObject.occupied = false;
                myPlacedObject.occupied = false;
                imMoving = true;
                illDie = true;
                }
                else
                {
                    Debug.Log("Material Incorreto!");
                }

            }





        }
        if (imMoving == true)
        {


            NextWorldPosition = GridBuildingSystem3D.Instance.GridToWorldPosition(nextPosition);
            transform.position = Vector3.MoveTowards(transform.position, NextWorldPosition, InitialValues.Instance.materialMovSpeed * Time.deltaTime);

            if (Vector3.Distance(NextWorldPosition, transform.position) <= 5f && illDie == true)
            {
                if (myNextTypeSOInArray.nameString == "Sorter")
                {
                    imMoving = false;
                    myNextPlacedObject.gameObject.GetComponent<SorterController>().SetMaterial(material);
                    Destroy(gameObject);
                }

                if (myNextTypeSOInArray.nameString == "Smelter")
                {
                    imMoving = false;
                    myNextPlacedObject.gameObject.GetComponent<SmelterController>().SetMaterial(material);
                    Destroy(gameObject);
                }
            }
            if (Vector3.Distance(NextWorldPosition, transform.position) <= 0.2f && illDie == false)
            {
                //myPlacedObject.occupied = false;
                myTypeSOInArray = myNextTypeSOInArray;
                myPlacedObject = myNextPlacedObject;
                nextPosition = myPlacedObject.GetOrigin() + myPlacedObject.GetForwardVector();
                imMoving = false;


            }
            if (Vector3.Distance(NextWorldPosition, transform.position) <= 0.2f && illDie == true)
            {
                imMoving = false;
                if (myNextTypeSOInArray.nameString == "IFMachine" || myNextTypeSOInArray.nameString == "While")
                {
                    myNextPlacedObject.GetIFMachine().getMaterialSO(material);
                    Destroy(gameObject);
                }
                if (myNextTypeSOInArray.nameString == "End")
                {
                    myNextPlacedObject.occupied = false;
                    myNextPlacedObject.gameObject.GetComponent<EndMachineController>().SetNewMaterial(material);
                    Destroy(gameObject);
                }
                if (myNextTypeSOInArray.nameString == "Switch")
                {
                    myNextPlacedObject.occupied = false;
                    myNextPlacedObject.gameObject.GetComponent<SwitchMachine>().SetMaterialSO(material);
                    Destroy(gameObject);
                }

                
            }

        }




    }

    private bool CheckNextMachine(PlacedObjectTypeSO myTypeSOInArray, Vector2Int myPosInGrid, string name)
    {

        PlacedObject_Done placedObjectDone = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(myPosInGrid);

        if (myTypeSOInArray.nameString == name && placedObjectDone.occupied == false)
        {

            return true;
        }
        else
        {
            return false;
        }
    }


    private bool CheckEsteira(PlacedObjectTypeSO myTypeSOInArray, Vector2Int myPosInGrid, Vector2Int myOrigin)
    {

        PlacedObject_Done placedObjectDone = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(myPosInGrid);
        Vector2Int nextPosition;
        nextPosition = placedObjectDone.GetOrigin() + placedObjectDone.GetForwardVector();


        if (myTypeSOInArray.nameString == "Esteira" && nextPosition != myOrigin && placedObjectDone.occupied == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private Vector2Int getLeft(string direcao)
    {
        switch (direcao)
        {
            default:
            case "Up": return new Vector2Int(-1, 0);
            case "Down": return new Vector2Int(1, 0);
            case "Left": return new Vector2Int(0, -1);
            case "Right": return new Vector2Int(0, 1);

        }
    }




}
