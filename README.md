# JsonConverters.Collections

[![Build Status](https://kyleherzog.visualstudio.com/JsonConverters.Collections/_apis/build/status/JsonConverters.Collections?branchName=develop)](https://kyleherzog.visualstudio.com/JsonConverters.Collections/_build/latest?definitionId=3&branchName=develop)

This library is available from [NuGet.org](https://www.nuget.org/packages/JsonConverters.Collections/).

--------------------------

A .NET Standard class library with JsonConverters to assist with deserializing collections.

## EnumerableJsonConverter
This converter will deserialize a serialized array to any class that implements `IEnumerable<>`. Just construct the `EnumerableJsonConverter` by passing the type of the objects in the serialized array.  Then add the converter to the converters array that is passed to `JsonConvert.DeserializeObject<>`, specifying any type that implements 'IEnumerable<>' or just an `IEnumerable<>` itself, without the need for any interface name to have been serialized into the data first. 

```c#
var serialized = "[true, false, true]";
var converters = new JsonConverter[] { new EnumerableJsonConverter(typeof(bool)) };
var deserialized = JsonConvert.DeserializeObject<IEnumerable<bool>>(
    serialized, 
    converters);
```

## License
[MIT](LICENSE)