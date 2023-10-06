using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject_Done : MonoBehaviour {

    public static PlacedObject_Done Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectTypeSO, Vector2Int dirForwardVector) {
        Transform placedObjectTransform = Instantiate(placedObjectTypeSO.prefab, worldPosition, Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0));

        PlacedObject_Done placedObject = placedObjectTransform.GetComponent<PlacedObject_Done>();
        placedObject.Setup(placedObjectTypeSO, origin, dir, dirForwardVector);

        return placedObject;
    }




    private PlacedObjectTypeSO placedObjectTypeSO;
    private Vector2Int origin;
    private PlacedObjectTypeSO.Dir dir;

    [SerializeField] private IFMachine ifmachine = null;

     public bool occupied = false;


    private Vector2Int dirForwardVector;

    public bool placed = false;
    public bool destroyable = true;
    public bool configPanel = false;

    private void Setup(PlacedObjectTypeSO placedObjectTypeSO, Vector2Int origin, PlacedObjectTypeSO.Dir dir, Vector2Int dirForwardVector) {
        this.placedObjectTypeSO = placedObjectTypeSO;
        this.origin = origin;
        this.dir = dir;
        this.dirForwardVector = dirForwardVector;

        placed = true;
    }

    public Vector2Int GetOrigin() {
        return this.origin;
    }

    public void SetForwardVector(Vector2Int newValue){
        this.dirForwardVector = newValue;
    }

    public string DirToString(){
        string direcao = placedObjectTypeSO.DirToString(this.dir);
        return direcao;

    }

    public IFMachine GetIFMachine(){
        return ifmachine;
    }
    

    public Vector2Int GetForwardVector(){
        return this.dirForwardVector;
    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO(){
        return this.placedObjectTypeSO;
    }

    public List<Vector2Int> GetGridPositionList() {
        return placedObjectTypeSO.GetGridPositionList(origin, dir);
    }

    public void DestroySelf() {
        if(destroyable){
        Destroy(gameObject);
        placed = false;
        }
    }

    public override string ToString() {
        return placedObjectTypeSO.nameString;
    }

}
