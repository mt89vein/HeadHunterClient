using System.Collections.Generic;
using System.Linq;
using Domain.SearchModels;
using HeadHunter.Integration.Abstractions;
using Newtonsoft.Json;

namespace HeadHunter.Integration.Models
{
    public class HeadHunterDataPage<TEntity, THeadHunterJsonModel> : IHeadHunterJsonModel<DataPage<TEntity>>
        where THeadHunterJsonModel : IHeadHunterJsonModel<TEntity>
    {
        [JsonProperty("found")]
        public int Found { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("items")]
        public List<THeadHunterJsonModel> Items { get; set; }

        public DataPage<TEntity> GetModel()
        {
            return new DataPage<TEntity> {
                Count = Found,
                Objects = Items.Select(i => i.GetModel()).ToList()
            };
        }
    }
}
