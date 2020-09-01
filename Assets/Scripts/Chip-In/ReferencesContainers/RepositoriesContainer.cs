using Autofac;
using Repositories.Remote;
using Repositories.Remote.Paginated;

namespace ReferencesContainers
{
    public static class RepositoriesContainer
    {
        static RepositoriesContainer()
        {
            Initialize();
        }
        
        private static IContainer _container;
        private static void Initialize()
        {
            _container = Configure();
        }
        
        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserAuthorisationDataRepository>().As<IUserAuthorisationDataRepository>().SingleInstance();
            builder.RegisterType<AdvertsPaginatedListRepository>().As<>().SingleInstance();
            return builder.Build();
        }

        public static T GetInstance<T>()
        {
            return _container.Resolve<T>();
        }
    }
}