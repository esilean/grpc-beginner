// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: greeting.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Greet {
  public static partial class GreetingService
  {
    static readonly string __ServiceName = "greet.GreetingService";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::Greet.GreetingManyTimesRequest> __Marshaller_greet_GreetingManyTimesRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Greet.GreetingManyTimesRequest.Parser));
    static readonly grpc::Marshaller<global::Greet.GreetingManyTimesResponse> __Marshaller_greet_GreetingManyTimesResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Greet.GreetingManyTimesResponse.Parser));

    static readonly grpc::Method<global::Greet.GreetingManyTimesRequest, global::Greet.GreetingManyTimesResponse> __Method_Greet = new grpc::Method<global::Greet.GreetingManyTimesRequest, global::Greet.GreetingManyTimesResponse>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "Greet",
        __Marshaller_greet_GreetingManyTimesRequest,
        __Marshaller_greet_GreetingManyTimesResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Greet.GreetingReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of GreetingService</summary>
    [grpc::BindServiceMethod(typeof(GreetingService), "BindService")]
    public abstract partial class GreetingServiceBase
    {
      public virtual global::System.Threading.Tasks.Task Greet(global::Greet.GreetingManyTimesRequest request, grpc::IServerStreamWriter<global::Greet.GreetingManyTimesResponse> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for GreetingService</summary>
    public partial class GreetingServiceClient : grpc::ClientBase<GreetingServiceClient>
    {
      /// <summary>Creates a new client for GreetingService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public GreetingServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GreetingService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public GreetingServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected GreetingServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected GreetingServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual grpc::AsyncServerStreamingCall<global::Greet.GreetingManyTimesResponse> Greet(global::Greet.GreetingManyTimesRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Greet(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncServerStreamingCall<global::Greet.GreetingManyTimesResponse> Greet(global::Greet.GreetingManyTimesRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_Greet, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override GreetingServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GreetingServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(GreetingServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Greet, serviceImpl.Greet).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GreetingServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Greet, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Greet.GreetingManyTimesRequest, global::Greet.GreetingManyTimesResponse>(serviceImpl.Greet));
    }

  }
}
#endregion
