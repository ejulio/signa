using Newtonsoft.Json;
using Signa.Domain.Signs.Dynamic;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Signa.Data.Repository
{
    public class SignRepository : IRepository<SinalDinamico>
    {
        private IList<SinalDinamico> signsByIndex;
        private IDictionary<string, SinalDinamico> signsById;

        private string dataFilePath;

        public int Count
        {
            get { return signsByIndex.Count; }
        }

        public SignRepository(string dataFilePath)
        {
            this.dataFilePath = dataFilePath;
            signsByIndex = new List<SinalDinamico>();
            signsById = new Dictionary<string, SinalDinamico>();
        }

        public void Add(SinalDinamico entity)
        {
            signsById.Add(entity.Descricao, entity);
            signsByIndex.Add(entity);
        }

        public SinalDinamico GetByIndex(int index)
        {
            if (index == Count)
                return null;

            return signsByIndex[index];
        }

        public SinalDinamico GetById(string id)
        {
            SinalDinamico sinalDinamico;
            if (signsById.TryGetValue(id, out sinalDinamico))
                return sinalDinamico;

            return null;
        }

        public void Load()
        {
            if (!File.Exists(dataFilePath))
                return;

            using (var reader = new StreamReader(dataFilePath))
            {
                var jsonSignSamples = reader.ReadToEnd();
                var fileSamples = JsonConvert.DeserializeObject<List<SinalDinamico>>(jsonSignSamples);
                signsByIndex = fileSamples ?? signsByIndex;
                LoadDictionary();
            }
        }

        private void LoadDictionary()
        {
            foreach (var sign in signsByIndex)
            {
                signsById.Add(sign.Descricao, sign);
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

        public IEnumerator<SinalDinamico> GetEnumerator()
        {
            return signsByIndex.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return signsByIndex.GetEnumerator();
        }
    }
}