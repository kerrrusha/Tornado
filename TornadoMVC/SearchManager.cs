namespace TornadoMVC
{
    public class SearchManager
    {
        private List<Dataset> data;

        public SearchManager()
        {
            data = new List<Dataset>();
        }
        public SearchManager(List<Dataset> data)
        {
            this.data = data;
        }

        public void add(Dataset newSource)
        {
            foreach (Dataset source in data)
            {
                if (source.name == newSource.name)
                {
                    source.data.AddRange(newSource.data);
                    return;
                }
            }
            data.Add(newSource);
        }
        public IEnumerable<Dataset> search(string query)
        {
            var result = new List<Dataset>();

            if (query is null)
                return result;

            query = query.ToLower().Trim();
            foreach (Dataset source in data)
            {
                foreach (Item dataElement in source.data)
                {
                    int id = dataElement.id;
                    string name = dataElement.name;
                    if (name is null)
                        continue;
                    if (name.ToLower().Trim().Contains(query))
                    {
                        bool appended = false;
                        foreach (Dataset match in result)
                        {
                            if (match.name == source.name)
                            {
                                match.data.Add(dataElement);
                                appended = true;
                            }
                        }
                        if (!appended)
                        {
                            result.Add(new Dataset(source.name, dataElement));
                        }
                    }
                }
            }

            return result;
        }
    }
}
