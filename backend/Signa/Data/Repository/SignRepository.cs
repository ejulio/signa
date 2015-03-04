using Newtonsoft.Json;
using Signa.Domain.Signs.Dynamic;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Signa.Data.Repository
{
    public class SignRepository : IRepository<Sign>
    {
        private IList<Sign> signsByIndex;
        private IDictionary<string, Sign> signsById;

        private string dataFilePath;

        public int Count
        {
            get { return signsByIndex.Count; }
        }

        public SignRepository(string dataFilePath)
        {
            this.dataFilePath = dataFilePath;
            signsByIndex = new List<Sign>();
            signsById = new Dictionary<string, Sign>();
        }

        public void Add(Sign entity)
        {
            signsById.Add(entity.Description, entity);
            signsByIndex.Add(entity);
        }

        public Sign GetByIndex(int index)
        {
            if (index == Count)
                return null;

            return signsByIndex[index];
        }

        public Sign GetById(string id)
        {
            Sign sign;
            if (signsById.TryGetValue(id, out sign))
                return sign;

            return null;
        }

        public void Load()
        {
            if (!File.Exists(dataFilePath))
                return;

            using (var reader = new StreamReader(dataFilePath))
            {
                var jsonSignSamples = reader.ReadToEnd();
                var fileSamples = JsonConvert.DeserializeObject<List<Sign>>(jsonSignSamples);
                signsByIndex = fileSamples ?? signsByIndex;
                LoadDictionary();
            }
        }

        private void LoadDictionary()
        {
            foreach (var sign in signsByIndex)
            {
                signsById.Add(sign.Description, sign);
            }
        }

        public void SaveChanges()
        {
            using (StreamWriter writer = new StreamWriter(dataFilePath))
            {
                var json = JsonConvert.SerializeObject(signsById.Values);
                writer.Write(json);
            }
        }

        public IEnumerator<Sign> GetEnumerator()
        {
            return signsByIndex.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return signsByIndex.GetEnumerator();
        }
    }
}