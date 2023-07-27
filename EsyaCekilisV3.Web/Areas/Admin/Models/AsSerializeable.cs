using Newtonsoft.Json;

namespace EsyaCekilisV3.Web.Areas.Admin.Models
{
    public abstract class AsSerializeable
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
