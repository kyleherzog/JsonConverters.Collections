using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace JsonConverters.Collections.UnitTests.Models
{
    [JsonConverter(typeof(SingleOrArrayJsonConverter<string>))]
    public class SingleOrList<T> : List<T>
    {
        public bool HasMultipleValues
        {
            get => this.Count > 1;
        }

        public bool HasValue
        {
            get => this.Any();
        }

        public T Value
        {
            get => this.FirstOrDefault();
        }
    }
}
