using Autofac;
using System;
using System.Linq;

namespace DesignPatterns.Decorator.Examples
{
    public interface IReportingService
    {
        void Report();
    }

    public class ReportingService : IReportingService
    {
        public void Report() => Console.WriteLine("Here is your report");
    }

    public class ReportingServiceWithLogging : IReportingService
    {
        private IReportingService _decorated;

        public ReportingServiceWithLogging(IReportingService reportingService)
        {
            _decorated = reportingService;
        }

        public void Report()
        {
            Console.WriteLine("Starting log...");
            _decorated.Report();
            Console.WriteLine("Stoping log...");
        }
    }

    public class DecoratorInDependencyInjection
    {
        public static void Start(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<ReportingService>().Named<IReportingService>("reporting");
            b.RegisterDecorator<IReportingService>(
                (context, service) => new ReportingServiceWithLogging(service), "reporting"
            );

            using var container = b.Build();
            var r = container.Resolve<IReportingService>();
            r.Report();
        }
    }
}
