using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JsonConverters.Collections.UnitTests.Models
{
    [JsonConverter(typeof(SingleOrArrayJsonConverter))]
    public class NestedValueOrList : IEnumerable<int>
    {
        public List<int> Items { get; } = new List<int>();

        public IEnumerator<int> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public void Add(int value)
        {
            Items.Add(value);
        }
    }
}
