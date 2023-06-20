using Autofac;
using MediatR;

namespace DesignPatterns.Mediator.Examples
{
    public class PongResponse
    {
        public DateTime Timestamp;

        public PongResponse(DateTime timestamp)
        {
            Timestamp = timestamp;
        }
    }

    public class PingCommand : IRequest<PongResponse>
    {

    }

    // [UsedImplicitly]
    public class PingCommandHandler : IRequestHandler<PingCommand, PongResponse>
    {
        public async Task<PongResponse> Handle(PingCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new PongResponse(DateTime.Now))
                .ConfigureAwait(false);
        }
    }

    public class MediatRExample
    {
        public static async Task StartAsync(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(MediatRExample).Assembly)
                .AsImplementedInterfaces();

            builder.Register<Func<Type, object>>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return type => componentContext.Resolve(type);
            });

            builder.RegisterType<MediatR.Mediator>()
                .As<IMediator>().InstancePerLifetimeScope();

            using var container = builder.Build();
            var mediator = container.Resolve<IMediator>();

            var response = await mediator.Send(new PingCommand());
            await Console.Out.WriteLineAsync($"We got a response at {response.Timestamp}");
        }
    }
}
