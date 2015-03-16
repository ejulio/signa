using Microsoft.Owin.StaticFiles.ContentTypes;

namespace Signa.ContentTypeProviders
{
    public class JsonContentTypeProvider : FileExtensionContentTypeProvider
    {
        public JsonContentTypeProvider()
        {
            Mappings.Add(".json", "application/json");
        }
    }
}