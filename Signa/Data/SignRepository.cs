using Newtonsoft.Json;
using Signa.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Signa.Data
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
            signsByIndex = new Sign[0];
            signsById = new Dictionary<string, Sign>();
        }

        public void Add(Sign entity)
        {
            signsById.Add(entity.Description, entity);
            signsByIndex.Add(entity);
        }

        public Sign GetByIndex(int index)
        {
            return signsByIndex[index];
        }

        public Sign GetById(string id)
        {
            return signsById[id];
        }

        public void Load()
        {
            using (var reader = new StreamReader(dataFilePath))
            {
                var jsonSignSamples = reader.ReadToEnd();
                signsByIndex = JsonConvert.DeserializeObject<List<Sign>>(jsonSignSamples);
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
    }
}