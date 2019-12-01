using Castle.Windsor;

namespace CastleDynamicProxy.Infrastructure
{
    public class DependencyResolver
    {
        private static IWindsorContainer _container;

        public static void Initialize()
        {
            _container = new WindsorContainer();
            _container.Register(new ComponentRegistration());
        }

        public static T For<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
