1. Difference between using reader/writer in System.IO namespace and using object serialization:
* seialization help reduce process by using serialization (meaning: describes the process of persisting the state of an object in to stream) and using deserialize (reconstruct necessary information about saved object).
* If you use reader or writer in System.IO, you will save each member in the object into file. Assume you need read the object from this file, you must read each member in this file and load them into object.
=> serialization help reduce code and proces => save time.

2. Note about serialization
* .NET object serialization makes it easy to persist objects; however, the processes used behind the scenes are quite sophisticated. Because, an object is persisted to a streamm, all associated data is automatically serialized, as well.
* .NET serialization services also allow you to persist an object graph in a variety ob formats.

3. The role of object graphs
* Graph representing the relationships among your objects is eastablished automatically behind the proccess.
* Using the BinaryFormatter and SoapFormatter is absolutely no difference, regardless of whethere they are public fields, private fields, or private fields exposed through public properties.
* Using XmlSerializer type, this type will only seriablize public data fields or private data exposed by public properties.
* Nonserialize attribute is used to reduce the size of persisted data.

4. Choosing the formatter: binary(binaryformatter), SOAP(SoapFormatter), XML(XmlFormatter).
* binary: serializing your object's state to a stream using a compact binary format.
using System.Runtime.Serialization.Formatters.Binary;
* SOAP: persisting an object's state as a SOAP message(the standard XML format for passing messages to/from a SOAP-based web service).
using System.Runtime.Serialization.Formatters.Soap;
* XML: persisting a tree of object as an XML document.
using System.Xml.Serialization;

	Note: If you want to persist an object’s state in a manner that can be used by any operating system (e.g.,
Windows, macOS, and various Linux distributions), application framework (e.g., .NET, Java Enterprise
Edition, and COM), or programming language, you do not want to maintain full type fidelity because you
cannot assume all possible recipients can understand .NET-specific data types. Given this, SoapFormatter
and XmlSerializer are ideal choices when you need to ensure as broad a reach as possible for the persisted
tree of objects

