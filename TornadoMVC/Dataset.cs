namespace TornadoMVC
{
    public class Dataset
    {
        readonly public string name;
        readonly public List<Item> data;

        public Dataset()
        {
            this.name = "";
            this.data = new List<Item>();
        }

        public Dataset(string name, List<Item> data)
        {
            this.name = name;
            this.data = data;
        }
        public Dataset(string name, Item data)
        {
            this.name = name;
            this.data = new List<Item>() { data };
        }
    }
}
