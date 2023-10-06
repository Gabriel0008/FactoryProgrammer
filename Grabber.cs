using UnityEngine;

public class Grabber : MonoBehaviour
{

    [SerializeField] private PlacedObject_Done placedObject;

    private Vector2Int grabPosition;
    private Vector2Int dropPosition;
    [SerializeField] private MaterialsSO waste;

    private bool gotWaste = false;
    private bool instantiated = false;
    private float cooldown ;
    private bool canInstantiate = true;
    private float time =0f;


    private UI_Info uI_Info;


    // Start is called before the first frame update
    void Awake()
    {
        cooldown = InitialValues.Instance.materialInstanceCooldown;
    }



    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time >= 0) {
            canInstantiate = false;
        }else{
            canInstantiate = true;
        }




        if(placedObject.placed == true && instantiated == false){
            grabPosition = placedObject.GetOrigin() + placedObject.GetForwardVector() * -1;
            dropPosition = placedObject.GetOrigin() + placedObject.GetForwardVector();

            instantiated = true;
            GridBuildingSystem3D.Instance.DeselectObjectType();
        }


        if(!GridBuildingSystem3D.Instance.CheckPosition(grabPosition)){
            PlacedObjectTypeSO placedObjectTypeSOgrab = GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(grabPosition);

            if(!GridBuildingSystem3D.Instance.CheckPosition(dropPosition)){
                PlacedObjectTypeSO placedObjectTypeSOdrop = GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(dropPosition);

                if (GridBuildingSystem3D.Instance.canSpawn == true)
                {

                    if (placedObjectTypeSOgrab.nameString == "iniciador" && checkEsteira(placedObjectTypeSOdrop, dropPosition))
                    {
                        StarterMaterials starterMaterials = placedObjectTypeSOgrab.prefab.GetComponent(typeof(StarterMaterials)) as StarterMaterials;
                        Vector3 materialPosition = GridBuildingSystem3D.Instance.GridToWorldPosition(dropPosition);
                        Vector3 worldPosition = GridBuildingSystem3D.Instance.GridToWorldPosition(placedObject.GetOrigin());
                        if (canInstantiate == true)
                        {

                            starterMaterials.InstantiateMaterials(materialPosition, worldPosition);
                            time = cooldown;
                        }
                    }
                    if (placedObjectTypeSOgrab.nameString == "Smelter" && checkEsteira(placedObjectTypeSOdrop, dropPosition))
                    {
                        SmelterController smelterController = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(grabPosition).gameObject.GetComponent<SmelterController>();
                        if (smelterController.result.quantidade > 1)
                        {
                            Transform material = Instantiate(smelterController.result.material.prefab, GridBuildingSystem3D.Instance.GridToWorldPosition(this.placedObject.GetOrigin()), Quaternion.identity);
                            material.position = Vector3.MoveTowards(GridBuildingSystem3D.Instance.GridToWorldPosition(this.placedObject.GetOrigin()), GridBuildingSystem3D.Instance.GridToWorldPosition(dropPosition), 5f * Time.deltaTime);
                            material.GetComponent<MaterialMovment>().initialPosition = GridBuildingSystem3D.Instance.GridToWorldPosition(grabPosition);
                            smelterController.result.quantidade--;
                        }
                    }
                    if (placedObjectTypeSOgrab.nameString == "Sorter" && checkEsteira(placedObjectTypeSOdrop, dropPosition))
                    {
                        if (gotWaste) {
                            if(checkEsteira(placedObjectTypeSOdrop, this.placedObject.GetOrigin()))
                            {
                                InstantiateWaste();
                            }
                            
                        }
                        else {
                            SorterController sorterController = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(grabPosition).gameObject.GetComponent<SorterController>();
                            if (!sorterController.isSorting())
                            {
                                if (sorterController.newMaterial == true)
                                {
                                    sorterController.gameObject.GetComponent<PlacedObject_Done>().occupied = false;
                                    Transform material = Instantiate(sorterController.transformMaterial(), GridBuildingSystem3D.Instance.GridToWorldPosition(this.placedObject.GetOrigin()), Quaternion.identity);
                                    material.position = Vector3.MoveTowards(GridBuildingSystem3D.Instance.GridToWorldPosition(this.placedObject.GetOrigin()), GridBuildingSystem3D.Instance.GridToWorldPosition(dropPosition), 5f * Time.deltaTime);
                                    material.GetComponent<MaterialMovment>().initialPosition = GridBuildingSystem3D.Instance.GridToWorldPosition(grabPosition);
                                    sorterController.newMaterial = false;
                                    gotWaste = true;


                                }


                            }
                        }
                            
                        
                    
                    }




                }
            }
            
        }
        

        
        
    }

   
    private void InstantiateWaste( )
    {
        PlacedObjectTypeSO placedObjectTypeSOdrop = GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(dropPosition);
        if (checkEsteira(placedObjectTypeSOdrop, dropPosition)) {
            Transform material = Instantiate(waste.prefab, GridBuildingSystem3D.Instance.GridToWorldPosition(this.placedObject.GetOrigin()), Quaternion.identity);
            material.position = Vector3.MoveTowards(GridBuildingSystem3D.Instance.GridToWorldPosition(this.placedObject.GetOrigin()), GridBuildingSystem3D.Instance.GridToWorldPosition(dropPosition), 5f * Time.deltaTime);
            material.GetComponent<MaterialMovment>().initialPosition = GridBuildingSystem3D.Instance.GridToWorldPosition(grabPosition);
            gotWaste = false;
                }
    }



    private bool checkEsteira(PlacedObjectTypeSO esteira ,Vector2Int dropPosition){
        PlacedObject_Done placedObjectDone = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(dropPosition);

        Vector2Int nextPosition;
        nextPosition = placedObjectDone.GetOrigin() + placedObjectDone.GetForwardVector();


        if((esteira.nameString == "Esteira")&&(nextPosition != placedObject.GetOrigin())&&(placedObjectDone.occupied == false)){
            return true;
        }
        else{
            return false;
        }

    }
}
