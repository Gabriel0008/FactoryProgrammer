using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using CodeMonkey.Utils;

public class GridBuildingSystem3D : MonoBehaviour {

    public static GridBuildingSystem3D Instance;

    public event EventHandler OnSelectedChanged;
    public event EventHandler OnObjectPlaced;

    [HideInInspector]public bool canSpawn = false;


    private int gridWidth = 1;
    private int gridHeight = 1;
    [SerializeField] private Transform ground;
    [SerializeField] private Transform ground2;

    [SerializeField] PlacedObjectTypeSO starter;
    [SerializeField] PlacedObjectTypeSO end;


    private GridXZ<GridObject> grid;
    private List<PlacedObjectTypeSO> placedObjectTypeSOList = new List<PlacedObjectTypeSO>();
    private List<Materials> endMaterials = new List<Materials>();
    private PlacedObjectTypeSO placedObjectTypeSO;
    private PlacedObjectTypeSO.Dir dir;

    private Renderer renderer;

    private bool _building = false;
    [HideInInspector]public Vector2Int placedObjectOrigin;

    private Vector2Int posBeforeMoving;

    [SerializeField] MenusController menusController;
    [SerializeField] UI_Inventory uI_Inventory;

    private void Awake() { //Create the Grid
        Instance = this;
        gridWidth = InitialValues.Instance.gridWidth;
        gridHeight = InitialValues.Instance.gridHeight;
        float cellSize = 10f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (GridXZ<GridObject> g, int x, int y) => new GridObject(g, x, y));
        ground.transform.localScale = new Vector3(gridWidth,1,gridHeight);
        Transform TextureObj = ground.GetChild(0);
        renderer = TextureObj.gameObject.GetComponent<Renderer>();
        renderer.material.SetTextureScale("_MainTex", new Vector2(gridWidth, gridHeight));
        ground2.transform.localScale = new Vector3(gridWidth*10, 1, gridHeight*10);
        Transform TextureObj2 = ground2.GetChild(0);
        renderer = TextureObj2.gameObject.GetComponent<Renderer>();
        renderer.material.SetTextureScale("_MainTex", new Vector2(gridWidth*10, gridHeight*10));

        placedObjectTypeSO = null;
        
    }




    public class GridObject {

        private GridXZ<GridObject> grid;
        private int x;
        private int y;
        public PlacedObject_Done placedObject;

        public GridObject(GridXZ<GridObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            placedObject = null;
        }

        public override string ToString() { //To be able do see the Grid
            return x + ", " + y + "\n" + placedObject; // x,y and object transform
        }

        public void SetPlacedObject(PlacedObject_Done placedObject) { //Set object on the array
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x, y);
        }

        public void ClearPlacedObject() {// clear object on the array 
            placedObject = null;
            grid.TriggerGridObjectChanged(x, y);
        }

        public PlacedObject_Done GetPlacedObject() {
            return placedObject;
        }


        public bool CanBuild() { //To see if theres something already built 
            return placedObject == null;
        }



    }

    private void Start()
    {
        
        placedObjectTypeSOList = InitialValues.Instance.GetMachines();
        endMaterials = InitialValues.Instance.GetEndMaterials();
        placedObjectTypeSO = starter;// placedObjectTypeSOList[0];
                                                      //Instantiate a building

        Vector2Int placedObjectOrigin = new Vector2Int(gridWidth, (gridHeight / 2) - 1);
        placedObjectOrigin = grid.ValidateGridPosition(placedObjectOrigin);
        // Test Can Build
        List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(placedObjectOrigin, dir);

        bool canBuild = true;

        if (canBuild)
        { //Confirm if you can build 
            Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
            Vector2Int dirForwardVector = placedObjectTypeSO.GetDirForwardVector(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(placedObjectOrigin.x, placedObjectOrigin.y) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

            PlacedObject_Done placedObject = PlacedObject_Done.Create(placedObjectWorldPosition, placedObjectOrigin, dir, placedObjectTypeSO, dirForwardVector);

            foreach (Vector2Int gridPosition in gridPositionList)
            {
                grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
            }

            OnObjectPlaced?.Invoke(this, EventArgs.Empty);

            DeselectObjectType();
        }

        placedObjectTypeSO = end;
        int spaceBetweenEnd = gridWidth / endMaterials.Count;
        for(int i = 1 ; i<= endMaterials.Count; i++)
        {
            Vector2Int endPlace = new Vector2Int(0, i * spaceBetweenEnd - 1);
            if(endMaterials.Count == 1)
            {
                endPlace = new Vector2Int(0, (gridHeight / 2) - 1);
            }else if(endMaterials.Count == 2)
            {
                endPlace = new Vector2Int(0, i * spaceBetweenEnd - 3);
            }
            placedObjectOrigin = grid.ValidateGridPosition(endPlace);

            // Test Can Build
            gridPositionList = placedObjectTypeSO.GetGridPositionList(endPlace, dir);

            bool canBuildEnd = true;

            if (canBuildEnd)
            { //Confirm if you can build 
                Vector2Int EndrotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector2Int EnddirForwardVector = placedObjectTypeSO.GetDirForwardVector(dir);
                Vector3 EndplacedObjectWorldPosition = grid.GetWorldPosition(placedObjectOrigin.x, placedObjectOrigin.y) + new Vector3(EndrotationOffset.x, 0, EndrotationOffset.y) * grid.GetCellSize();

                PlacedObject_Done placedObject = PlacedObject_Done.Create(EndplacedObjectWorldPosition, placedObjectOrigin, dir, placedObjectTypeSO, EnddirForwardVector);
                placedObject.gameObject.GetComponent<EndMachineController>().SetEndMaterial(endMaterials[(i-1)]);

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                }

                OnObjectPlaced?.Invoke(this, EventArgs.Empty);
                //DeselectObjectType();
            }
        }


    }

    private void Update() {


        if (_building == true)
        { //Instantiate a building

                placedObjectOrigin = grid.ValidateGridPosition(placedObjectOrigin);
                List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(placedObjectOrigin, dir);
                bool canBuild = true;
                foreach (Vector2Int gridPosition in gridPositionList)
                { //Check if theres something constructed already 
                    if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                    {
                        canBuild = false;
                        break;
                    }
                }

                if (canBuild)
                {
                
                //Confirm if you can build 
                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector2Int dirForwardVector = placedObjectTypeSO.GetDirForwardVector(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(placedObjectOrigin.x, placedObjectOrigin.y) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

                PlacedObject_Done placedObject = PlacedObject_Done.Create(placedObjectWorldPosition, placedObjectOrigin, dir, placedObjectTypeSO, dirForwardVector);

                    foreach (Vector2Int gridPosition in gridPositionList)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                    }

                OnObjectPlaced?.Invoke(this, EventArgs.Empty);


                    if (placedObjectTypeSO.nameString != "Esteira")
                    {
                    DeselectObjectType();
                    CameraMotion.canMove = true;
                    }
                }
                else
                {
                // Cannot build here
                Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
                UtilsClass.CreateWorldTextPopup("Cannot Build Here!", mousePosition); // Show a pop up message saying that you cant build
                OpenPlacingButtons();


                }


                
            _building = false;
        }

        



        if (CameraMotion.canMove == false)
        {
            

            if (Input.GetMouseButtonDown(0) && placedObjectTypeSO != null)
            { //Instantiate a building


                Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
                if (mousePosition != new Vector3(0, 0, 0))
                {
                    grid.GetXZ(mousePosition, out int x, out int z);
                    placedObjectOrigin = new Vector2Int(x, z);

                }

            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
                {
                    if (raycastHit.transform.gameObject.layer == 8)
                    {
                        if (raycastHit.transform.gameObject.GetComponent<PlacedObject_Done>().configPanel == true)
                        {
                            
                            raycastHit.transform.gameObject.GetComponent<PanelManager>().OpenPanel();
                        }
                        else
                        {
                            if (canSpawn == false)
                            {
                                MenusController uI_MaterialPanel = GameObject.Find("UI_Player").GetComponent<MenusController>();
                                grid.GetXZ(raycastHit.point, out int newx, out int newz);
                                posBeforeMoving = new Vector2Int(newx, newz);
                                placedObjectTypeSO = raycastHit.transform.gameObject.GetComponent<PlacedObject_Done>().GetPlacedObjectTypeSO();
                                uI_MaterialPanel.OpenOptionsMoveButtons();
                            }
                        }
                        

                    }
                    else if (raycastHit.transform.gameObject.layer == 9)
                    {
                        raycastHit.transform.gameObject.GetComponent<MaterialMovment>().OpenMaterialPanelThroughClick();
                    }
                }
            }
        }

        

        


    }

    public bool CheckCanBuild(Vector2Int pos)
    {
        if (grid.GetGridObject(pos.x, pos.y).CanBuild())
        {
            return true;

        }else{
            return false;
        }
    }

    public void PlaceMachineInGrid(PlacedObjectTypeSO PMmachine, Vector2Int PMposition)
    {
        placedObjectTypeSO = PMmachine;
        placedObjectOrigin = PMposition;
        _building = true;
    }

    public void RemoveMachineFromGrid(Vector2Int machinePositon)
    {
        PlacedObject_Done placedObject = grid.GetGridObject(machinePositon.x, machinePositon.y).GetPlacedObject();
        List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();

        placedObject.DestroySelf();


        foreach (Vector2Int gridPosition in gridPositionList)
        {
            grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
        }




    }


public void DeselectObjectType() {
        placedObjectTypeSO = null; RefreshSelectedObjectType();
    }

    private void RefreshSelectedObjectType() {
        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition) {
        grid.GetXZ(worldPosition, out int x, out int z);
        return new Vector2Int(x, z);
    }

    public Vector3 GetMouseWorldSnappedPosition()
    {
        Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
        if (mousePosition != new Vector3(0, 0, 0)) { 
        grid.GetXZ(mousePosition, out int x, out int z);

        if (placedObjectTypeSO != null)
        {
            Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        }
        else
        {
            return mousePosition;
        }

    }
        else
        {
            return (new Vector3(0, 0, 0));
        }
    }

    public Quaternion GetPlacedObjectRotation() {
        if (placedObjectTypeSO != null) {
            return Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0);
        } else {
            return Quaternion.identity;
        }
    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO() {
        return placedObjectTypeSO;
    }

    public void ClickPlaceMachine()
    {
        
        if (placedObjectTypeSO.nameString != "Esteira")
        {
            _building = true;
            ClosePlacingButtons();


        }
        else
        {
            if (!CheckForLoop(placedObjectOrigin + placedObjectTypeSO.GetDirForwardVector(dir)))
            {

                _building = true;
            }
            else
            {
                UtilsClass.CreateWorldTextPopup("Loop Without a While Machine!", GridToWorldPosition(placedObjectOrigin));
            }
        }


    }

    public void ClickRotateMachine()
    {
        dir = PlacedObjectTypeSO.GetNextDir(dir);
    }

    public void ClickCancelPlaceMachine()
    {
        UI_Inventory uI_Inventory = GameObject.Find("UI_Player").GetComponent<UI_Inventory>();
        uI_Inventory.AddIten(placedObjectTypeSO.nameString);
        uI_Inventory.RefreshInventory();
        ClosePlacingButtons();
        CameraMotion.canMove = true;
        DeselectObjectType();
    }

    public void ClickMoveMachine()
    {


            // Valid Grid Position
            PlacedObject_Done placedObject = grid.GetGridObject(posBeforeMoving.x,posBeforeMoving.y).GetPlacedObject();
            
            if (placedObject != null)
            {
                // Demolish
                placedObject.DestroySelf();
                List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
                }
            placedObjectOrigin = posBeforeMoving;
            RefreshSelectedObjectType();
            OpenPlacingButtons();
            CameraMotion.canMove = false;
            GameObject.Find("UI_Player").GetComponent<MenusController>().CloseOptionsMoveButtons();

        }
        

        

    }

    public void ClickRemoveMachine()
    {

        // Valid Grid Position
        PlacedObject_Done placedObject = grid.GetGridObject(posBeforeMoving.x, posBeforeMoving.y).GetPlacedObject();



        if (placedObject != null)
        {
            List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
            
            if (placedObject.GetPlacedObjectTypeSO().nameString == "Switch")
            {

                if (placedObject.gameObject.GetComponent<SwitchController>().GetNumCases() > 2)
                {
                    for (int i = 2; i < placedObject.gameObject.GetComponent<SwitchController>().GetNumCases(); i++)
                    {

                        GetPlacedObjectOnGrid(placedObject.gameObject.GetComponent<SwitchMachine>().GetCaseInGrid(i)).DestroySelf();

                        List<Vector2Int> igridPositionList = GetPlacedObjectOnGrid(placedObject.gameObject.GetComponent<SwitchMachine>().GetCaseInGrid(i)).GetGridPositionList();
                        foreach (Vector2Int gridPosition in igridPositionList)
                        {
                            grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
                        }



                    }
                }

            }



            // Demolish
            UI_Inventory uI_Inventory = GameObject.Find("UI_Player").GetComponent<UI_Inventory>();
            PlacedObjectTypeSO machine = placedObject.GetPlacedObjectTypeSO();
            uI_Inventory.AddIten(machine.nameString);
            uI_Inventory.RefreshInventory();
            Debug.Log("salve");

            placedObject.DestroySelf();

            
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
            }

            GameObject.Find("UI_Player").GetComponent<MenusController>().CloseOptionsMoveButtons();

            


        }

        


    }


    public void InstantiateMachineByDrag(string nameString){ //

        for (int i = 0; i < placedObjectTypeSOList.Count; i++)
        {
            if (nameString == placedObjectTypeSOList[i].nameString)
            {
                
                placedObjectTypeSO = placedObjectTypeSOList[i];
                RefreshSelectedObjectType();

                return;
            }
        }
        DeselectObjectType();

    }

    public Vector3 GridToWorldPosition(Vector2Int gridPosition){//
        Vector3 worldPosition =  grid.GetWorldPosition(gridPosition.x, gridPosition.y) + new Vector3(+ (grid.GetCellSize()/2),6.5f,(grid.GetCellSize()/2)) ;
        
        return worldPosition;

    }

    

    public PlacedObjectTypeSO GetPlacedObjectTypeSO(Vector2Int gridPosition){//
            PlacedObject_Done placedObject = grid.GetGridObject(gridPosition.x, gridPosition.y).GetPlacedObject();
            PlacedObjectTypeSO placedObjectTypeSO = placedObject.GetPlacedObjectTypeSO();
            return placedObjectTypeSO;
        


    }

    public PlacedObject_Done GetPlacedObjectOnGrid(Vector2Int gridPosition){//
        PlacedObject_Done placedObject = grid.GetGridObject(gridPosition.x,gridPosition.y).GetPlacedObject();
        return placedObject;
    }

    public void SetposBeforeMoving(Vector2Int newPos)
    {
        posBeforeMoving = newPos;
    }



    public bool CheckPosition(Vector2Int gridPosition){//
                if (grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) {
                    return true;
                }
                return false;
                
    }

    private bool CheckForLoop(Vector2Int nextPos)
    {
        int stopFlag = 0;
        while (stopFlag < 1000)
        {
            if(nextPos == placedObjectOrigin)
            {
                return true;
            }
            else
            {
                if (CheckCanBuild(nextPos))
                {
                    return false;
                }
                else
                {
                    switch(GetPlacedObjectTypeSO(nextPos).nameString)
                    {
                        case "IFMachine":
                            if (CheckForLoop(GetPlacedObjectOnGrid(nextPos).gameObject.GetComponent<IFMachine>().outElse)) { return true; }
                            if (CheckForLoop(GetPlacedObjectOnGrid(nextPos).gameObject.GetComponent<IFMachine>().outElse2)) { return true; }
                            if (CheckForLoop(GetPlacedObjectOnGrid(nextPos).gameObject.GetComponent<IFMachine>().outIf)) { return true; }
                            return false;
                        case "Switch":
                            int cases = GetPlacedObjectOnGrid(nextPos).gameObject.GetComponent<SwitchController>().GetNumCases();
                            for(int i = 0; i< cases; i++)
                            {
                                if (CheckForLoop(GetPlacedObjectOnGrid(nextPos).gameObject.GetComponent<SwitchMachine>().GetOutCase(i))) { return true; } 

                            }
                            return false;
                        case "While":return false;
                    }
                }
                nextPos = GetPlacedObjectOnGrid(nextPos).GetOrigin() + GetPlacedObjectOnGrid(nextPos).GetForwardVector();
            }

            stopFlag++;
        }
        return false;
    }

    public void OpenPlacingButtons()//
    {
        menusController.OpenPlacingButtons();
    }

    public void ClosePlacingButtons()//
    {
        menusController.ClosePlacingButtons();
    }

    public void SetPlacedObjectTypeSO(PlacedObjectTypeSO newPlacedObjectTypeSO)
    {
        placedObjectTypeSO = newPlacedObjectTypeSO;
    }







}




/*
 * if (_building == true)
        { //Instantiate a building
            Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
            if (mousePosition != new Vector3(0, 0, 0))
            {
                grid.GetXZ(mousePosition, out int x, out int z);

                Vector2Int placedObjectOrigin = new Vector2Int(x, z);
                placedObjectOrigin = grid.ValidateGridPosition(placedObjectOrigin);

                // Test Can Build
                List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(placedObjectOrigin, dir);
                bool canBuild = true;
                foreach (Vector2Int gridPosition in gridPositionList)
                { //Check if theres something constructed already 
                    if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                    {
                        canBuild = false;
                        break;
                    }
                }




                if (canBuild)
                { //Confirm if you can build 
                    Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                    Vector2Int dirForwardVector = placedObjectTypeSO.GetDirForwardVector(dir);
                    Vector3 placedObjectWorldPosition = grid.GetWorldPosition(placedObjectOrigin.x, placedObjectOrigin.y) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

                    PlacedObject_Done placedObject = PlacedObject_Done.Create(placedObjectWorldPosition, placedObjectOrigin, dir, placedObjectTypeSO, dirForwardVector);

                    foreach (Vector2Int gridPosition in gridPositionList)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                    }

                    OnObjectPlaced?.Invoke(this, EventArgs.Empty);

                    //DeselectObjectType();
                }
                else
                {
                    // Cannot build here
                    UtilsClass.CreateWorldTextPopup("Cannot Build Here!", mousePosition); // Show a pop up message saying that you cant built 
                }
            }

if (Input.GetMouseButtonDown(1)) {
            Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();

            if (grid.GetGridObject(mousePosition) != null) {
                // Valid Grid Position
                PlacedObject_Done placedObject = grid.GetGridObject(mousePosition).GetPlacedObject();
                if (placedObject != null) {
                    // Demolish
                    placedObject.DestroySelf();

                    List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
                    foreach (Vector2Int gridPosition in gridPositionList) {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
                    }
                }
            }
        }
        }*/