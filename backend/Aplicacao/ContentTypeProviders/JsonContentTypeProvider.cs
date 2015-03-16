using Microsoft.Owin.StaticFiles.ContentTypes;

namespace Aplicacao.ContentTypeProviders
{
    public class JsonContentTypeProvider : FileExtensionContentTypeProvider
    {
        public JsonContentTypeProvider()
        {
            Mappings.Add(".json", "application/json");
        }
    }
}