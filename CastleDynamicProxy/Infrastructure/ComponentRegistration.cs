using Castle.Core;
using Castle.DynamicProxy;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using CastleDynamicProxy.Interceptors;
using CastleDynamicProxy.Services;

namespace CastleDynamicProxy.Infrastructure
{
    public class ComponentRegistration : IRegistration
    {
        public void Register(IKernelInternal kernel)
        {
            kernel.Register(
                Component.For<IInterceptor>()
                .ImplementedBy<ServiceInterceptor>());

            kernel.Register(
                Component.For<IDemoService>()
                         .ImplementedBy<DemoService>()
                         .Interceptors(InterceptorReference.ForType<ServiceInterceptor>()).Anywhere);
        }
    }
}
