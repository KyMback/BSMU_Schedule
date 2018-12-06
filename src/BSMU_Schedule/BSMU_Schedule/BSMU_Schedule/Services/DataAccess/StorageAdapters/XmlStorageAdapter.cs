using System.IO;
using System.Threading.Tasks;
using BSMU_Schedule.Interfaces;
using BSMU_Schedule.Interfaces.DataAccess.StorageAdapters;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BSMU_Schedule.Services.DataAccess.StorageAdapters
{
    public class XmlStorageAdapter<T>: IStorageAdapter<T>
    {
        private readonly string fullPath;
        private readonly string fileName;

        public XmlStorageAdapter(IHasConnectionConfiguration configuration)
        {
            fileName = configuration.ConnectionConfiguration;
            fullPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                fileName);
        }

        public Task<T> Get()
        {
            if (!File.Exists(fullPath))
            {
                return Task.Run(() => default(T));
            }
            XDocument xdoc = XDocument.Load(fullPath);
            T obj;
            var xmlSerializer = new XmlSerializer(typeof(T));
            using(var reader = xdoc.CreateReader())
            {
                obj = (T)xmlSerializer.Deserialize(reader);
            }
            return Task.Run(() => obj);
        }

        public Task<T> InsertOrUpdate(T value)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                serializer.Serialize(writer, value);
            }

            return Task.Run(() => value);
        }

        public void Commit()
        {
        }

        public void Dispose()
        {
        }
    }
}
