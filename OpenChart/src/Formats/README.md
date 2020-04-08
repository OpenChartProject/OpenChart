# tl;dr

This first section explains the process and rationale around how file formats are handled. Skip to the examples if you just want a quick reference.

# File Format Process

Ultimately, we want to only have a single chart class that we use internally within the app (`OpenChart.Charting.Chart`). This obviously makes our lives way easier since the internal logic doesn't depend on what kind of chart we're working on. The goal is to have a universal file format which can reasonably represent the data for all the other file formats.

For handling different file formats, the most direct approach is to simply have a Converter class that can write `Chart` objects to the file format (exporting), or read the file and return a `Chart` object (importing).

The problem with this approach is that you are stuffing a ton of logic into a single class. Suppose we try exporting a file but it didn't work like we expected it to. Was this error a result of incorrectly interpreting the `Chart` class in the context of a different file format? Or was this an I/O related error, such as bad formatting?

The preferred approach here then is to split up the data conversion and the data representation.

Data conversion refers to the interpretation of our `Chart` data. Every file format likely doesn't support all the data we use internally. Conversely, we likely don't use all of the data internally that a particular file format has to offer, either.

Data representation refers to how that data is stored. This is where the I/O comes in, and knowing how to read/write data from a particular format.

In order to mesh these two processes together, we need an intermediate data class. Essentially, a class we can load the raw file data into that then gets passed to the chart converter.

## Example

Say we want to add support for a file format called `Foo`. We need three things:

- A class that can store the data from a .foo file. (`FooChart`)
- A class that can read and write `FooChart` objects to/from a .foo file. (`FooLoader`)
- A class that can convert a `FooChart` into a `Chart` and vice versa. (`FooConverter`)

### Conversion Pipeline

```
import: .foo file ---> FooLoader    ---> FooChart ---> FooConverter ---> Chart
export: Chart     ---> FooConverter ---> FooChart ---> FooLoader    ---> .foo file
```

Note: for the OpenChart file format, the process is simplified since the conversion step is unnecessary.

# Benefits

Compare the pipeline in the example with the pipeline described in the first section:

```
import: Chart 	  ---> .foo file
export: .foo file ---> Chart
```

Both pipelines produce the same results, but by having more steps, we can distribute the complexity among several classes. This means having classes which are smaller, more concise, easier to read, and easier to maintain.

Additionally, because we can test each individual step, we can now incrementally test four individual steps instead of just one.