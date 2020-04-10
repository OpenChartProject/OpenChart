# File Formats

To help keep things consistent and make file formats easier to test, there is a standardized way that all file formats are handled.

In order to create a new file format, you will need to:

1. Create a class which represents the data stored by the file format.
2. Create an implementation of [IFormatConverter](IFormatConverter.cs).
3. Create an implementation of [IFormatSerializer](IFormatSerializer.cs).
4. Create an implementation of [IFormatHandler](IFormatHandler.cs).

This diagram shows how these classes fit together. The direction the data is moving is dependent on whether a chart is being imported (loaded) or exported (saved).

![](img/file_format_class_diagram.png)

## IFormatConverter

The converter is responsible for converting between the native `Chart` object and an object used by an `IFormatSerializer`.

**Importing**: The converter accepts a **file-format object (FFO)** as input and converts it to a `Chart` object.

**Exporting**: The converter accepts a `Chart` object and converts it to a FFO.

## IFormatSerializer

The serializer is responsible for reading and writing the FFO to a stream.

**Importing**: The serializer accepts a byte stream and unserializes it into a FFO.

**Exporting**: The serializer accepts a FFO from the converter, serializes it, and writes it to the byte stream.

## IFormatHandler

The format handler's main purpose is to call the converter and serializer and pass the necessary data between the two. While most format handlers will look nearly identical, the format handler is a great place to do some inspection on the file, such as checking the file's version to know which converter/serializer to use.