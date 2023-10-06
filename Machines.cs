
[System.Serializable] public class Machines 
{
    public PlacedObjectTypeSO machine;
    public int quantidade;

    public Machines(PlacedObjectTypeSO material, int quantidade, bool endless)
    {
        this.machine = material;
        this.quantidade = quantidade;


    }

    public void RemoveIten()
    {
        if(this.quantidade > 0)
        this.quantidade--;
    }

    public void AddIten()
    {
            this.quantidade++;
    }
}
