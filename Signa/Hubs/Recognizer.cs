using Microsoft.AspNet.SignalR;
using Signa.Model;
using Signa.Recognizer;

namespace Signa.Hubs
{
    public class Recognizer : Hub
    {
        public int Recognize(SignSample data)
        {
            return Svm.Instance.Recognize(data);
        }

        //public void SaveSignSample(string name, string exampleFileContent, SignSample data)
        //{
        //    if (!Directory.Exists("samples"))
        //    {
        //        Directory.CreateDirectory("samples");
        //    }

        //    var fileName = "samples/" + name + ".json";
        //    using (StreamWriter writer = new StreamWriter(fileName))
        //    {
        //        writer.Write(exampleFileContent);
        //    }

        //    SignController.Instance.Add(new Sign
        //    {
        //        Description= name,
        //        ExampleFilePath = fileName,
        //        Samples = new SignSample[] { data }
        //    });

        //    SignController.Instance.Save();
        //}

        //public SignInfo GetNextSign(int previousSignId)
        //{
        //    var signId = GetRandomIndex(previousSignId);
        //    var sign = SignController.Instance.GetByIndex(signId);

        //    var signInfo = new SignInfo
        //    {
        //        Id = signId,
        //        Description = sign.Description,
        //        ExampleFilePath = sign.ExampleFilePath
        //    };

        //    return signInfo;
        //}
    }
}