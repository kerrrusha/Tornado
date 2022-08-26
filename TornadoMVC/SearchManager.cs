using TornadoMVC.Data;
using TornadoMVC.Models;

namespace TornadoMVC
{
    public class SearchManager
    {
        private List<Dataset> data;
        private readonly TornadoMVCContext _context;

        public SearchManager(TornadoMVCContext context)
        {
            _context = context;
            data = GetDataSources();
        }

        private List<Dataset> GetDataSources()
        {
            var sources = new List<Dataset>();

            var data = new List<Item>();
            _context.Category.ToList().ForEach(delegate (Category item)
            {
                data.Add(new Item(item.Id, item.Name));
            });
            sources.Add(new Dataset("Категорії", data.ToList()));

            data.Clear();
            _context.Product.ToList().ForEach(delegate (Product item)
            {
                data.Add(new Item(item.Id, item.Name));
            });
            sources.Add(new Dataset("Товари", data.ToList()));

            data.Clear();
            _context.Product.ToList().ForEach(delegate (Product item)
            {
                data.Add(new Item(item.Id, item.description));
            });
            sources.Add(new Dataset("Описи товарів", data.ToList()));

            return sources;
        }
        public IEnumerable<Dataset> Search(string query)
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
