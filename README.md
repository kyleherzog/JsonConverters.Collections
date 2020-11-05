# JsonConverters.Collections

[![Build Status](https://kyleherzog.visualstudio.com/JsonConverters.Collections/_apis/build/status/JsonConverters.Collections?branchName=master)](https://kyleherzog.visualstudio.com/JsonConverters.Collections/_build/latest?definitionId=3&branchName=master)

This library is available from [NuGet.org](https://www.nuget.org/packages/JsonConverters.Collections/).

--------------------------

A .NET Standard class library with JsonConverters to assist with deserializing collections.

## SingleOrArrayJsonConverter
This converter supports deserializing `IEnumerable<>` properties that are serialized as an array when there are multiple values, but when there is only one value, just the value itself is serialized.

The following example shows a property the leverages the `JsonConverterAttribute` to specify the `SingleOrArrayJsonConverter<>`.  With this specification, both serialization examples shown will successfully deserialize.

**Example Class**
```c#
public class DemoUser
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("aliases")]
    [JsonConverter(typeof(SingleOrArrayJsonConverter<string>))]
    public List<string> Aliases { get; set; }
}
```

**Example JSON with Array**
```
{
    "name": "John",
    "aliases": [
        "Johnny",
        "J."
    ]
}
```

**Example JSON with Single Value**
```
{
    "name": "Johnathan",
    "aliases": "John"
}
```

## EnumerableJsonConverter
This converter will deserialize a serialized array to any type that implements `IEnumerable<>`. Just construct the `EnumerableJsonConverter` by passing the type of the objects in the serialized array.  Then add the converter to the converters array that is passed to `JsonConvert.DeserializeObject<>`, specifying any type that implements `IEnumerable<>` or just an `IEnumerable<>` itself, without the need for any interface name to have been serialized into the data first. 

```c#
var serialized = "[true, false, true]";
var converters = new JsonConverter[] { new EnumerableJsonConverter(typeof(bool)) };
var deserialized = JsonConvert.DeserializeObject<IEnumerable<bool>>(
    serialized, 
    converters);
```

## License
[MIT](LICENSE)