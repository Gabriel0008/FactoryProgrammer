
[System.Serializable] public class Materials{


        public MaterialsSO material;
        public int quantidade;

        public Materials(MaterialsSO material, int quantidade){
            this.material = material;
            this.quantidade = quantidade;

        }
        public Materials(MaterialsSO material)
        {
        this.material = material;
        this.quantidade ++;

        }

    public Materials()
        {
        this.material = null;
        this.quantidade = 0;
        }

        public void AddQuantidade()
    {
        this.quantidade++;
    }


}