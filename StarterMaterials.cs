using UnityEngine;

public class StarterMaterials : MonoBehaviour
{
    private GameObject uI_Handler;




    public void InstantiateMaterials(Vector3 worldPosition, Vector3 myPosition)
    {
        if (InitialValues.Instance.totalQuantity > 0)
        {
            uI_Handler = GameObject.Find("UI_Player");
            int breakingPoint = 0;
            bool searching = true;
            int i =0;
            while ((searching == true) && (breakingPoint < 100)&&( i<1000))
            {
                int opt = UnityEngine.Random.Range(0, InitialValues.Instance.materials.Count);

                if(InitialValues.Instance.materials[opt].quantidade > 0){
                    Transform material = Instantiate(InitialValues.Instance.materials[opt].material.prefab, worldPosition, Quaternion.identity);
                    uI_Handler.GetComponent<UI_Info>().RefreshInventory();
                    material.position = Vector3.MoveTowards(myPosition, worldPosition, 5f * Time.deltaTime);
                    Materials newValue = new Materials(InitialValues.Instance.materials[opt].material,InitialValues.Instance.materials[opt].quantidade - 1);
                    InitialValues.Instance.materials[opt] = newValue;
                    InitialValues.Instance.totalQuantity --;
                    searching = false;

                }
                else{
                    breakingPoint ++;
                }

                if(breakingPoint > 98){
                    Debug.Log("Exit by break");
                }
                if(i>900){
                    Debug.Log("Exit by i-break");
                }
                i++;


            }
        }
        else
        {
            Debug.Log("MaterialsSO não encontrado");
        }
    }


}
