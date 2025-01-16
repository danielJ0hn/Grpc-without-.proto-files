# GRPC instructions for developing server and client

GRPC uses .proto file for communication protocols. It will contain the request type reply type and the procedures which can be called by client to server
## Install nuget packages
1. Google.Protobuf(all projects using grpc): To work with the proto files
1. Grpc.Net.Client(client project): To talk to grpc servers.
1. Grpc.Tools(global): Visual Studio tooling for working with grpc files.

## Proto file description

1. **syntax** mentions the proto schema.
    ```cs
    syntax = "proto3";
    ```
1. **Import** mentions other proto file which is being used in the current file.
    ```cs
    import  "another.proto";
    ```
1. **csharp_namespace** mentions the namespace of the class generated.
1. **package** name of the package.
1. **service** class with procedures in it.
    - Syntax:
    ```cs
    Service ServiceName
    {
        rpc ProcedureName (RequestModelName) returns (ReplyModelName);
    }
    ```

    This is equivalent to
    ```cs
    Class ServiceName
    {
        ReplyModelName ProcedureName(RequestModelName);
    }
    ```
    - ```ServiceName``` is the name of the class.
    - ```ProcedureName``` corresponds to the method name which we will call.
    - ```RequestModelName``` is the ModelName of request.
    - ```ReplyModelName``` is the ModelName of returned object.

1. **message** definition for a model(class) in C#.
    - Syntax: 
    ```cs
    message ModelName
    {
    Type propertyName = 1;
    Type anotherPropertyName = 2;
    }
    ```
    - message has a name ```ModelName```
    - In the body specify the type, name and the order in which the property will appear, for each property in the format ```TypeOfProperty NameOfProperty = OrderOfProperty```.
1. **Stream** to return a stream of items. To stream the the arrray of data, we specify:
```cs
rpc ProcedureName (RequestModelName) returns (stream ReplyModelName);
```

## GRPC Server

### Adding services

1. In ```.csproj``` add the section
    ```cs
    <ItemGroup>
        <Protobuf Include="..\pathToProtoFile\fileName.proto" GrpcServices="Server" />
    </ItemGroup>
    ```
1. Create a class with the name of the endpoint ```EndpointService``` and implement the base class from proto file ```ServiceName.ServiceNameBase```
1. Create constructor and inject dependencies if any.
1. Use below method structure for overriding a method
    ```cs
    public override Task<ReplyModelName> ProcedureName(RequestModelName request, ServerCallContext context)
    {
        return Task.FromResult(response);
    }
    ```
1. If using a stream, write to a stream like this:
    ```cs
    await responseStream.WriteAsync(eachItem);
    ```

1. Add the grpc service to the middleware to the ```program.cs``` file.
    ```cs
    app.MapGrpcService<EndpointService>();
    ```

## GRPC Client
1. In ```.csproj``` add the section
    ```cs
    <ItemGroup>
        <Protobuf Include="..\pathToProtoFile\fileName.proto" GrpcServices="Client" />
    </ItemGroup>
    ```
1. Connect to the grpc channel using the following line
    ```cs
    var channel = GrpcChannel.ForAddress("https://address");
    ```
1. Create client using
    ```cs
    var client = new ServiceName.ServiceNameClient(channel);
    ```
1. Call the remote procedure
    ```cs
    var reply = await client.ProcedureName(request)
    ```
1. For stream of data use
    ```cs
    using (var call = client.ProcedureName(parameter))
    {
        while (await call.ResponseStream.MoveNext())
        {
            var current = call.ResponseStream.Current;
            //do operation on current
        }
    }
    ```