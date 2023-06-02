// See https://aka.ms/new-console-template for more information

using DesignPatterns.Proxy.Examples;
using DesignPatterns.Proxy.Examples.DynamicProxyForLogging;
using DesignPatterns.Proxy.Examples.PropertyProxy;
using DesignPatterns.Proxy.Examples.ProtectionProxy;
using DesignPatterns.Proxy.Examples.ValueProxy;
using DesignPatterns.Proxy.Examples.ViewModel;

ProtectionProxy.Start(args);

PropertyProxy.Start(args);

ValueProxy.Start(args);

CompositeProxy_SoA_Aos.Start(args);

CompositeProxyWithArray_BackedProperties.Start(args);

DynamicProxyForLogging.Start(args);

ViewModel.Start(args);

BitFragging.Start(args);