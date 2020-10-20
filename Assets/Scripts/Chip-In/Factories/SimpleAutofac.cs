namespace Factories
{
    public static class SimpleAutofac
    {
        private static readonly FactoryContainer Container = new FactoryContainer();

        public static void AddObjectInstanceAs<T>(object objectInstance) where T : class
        {
            Container.AddObjectInstanceAs<T>(objectInstance);
        }

        public static void AddObjectInstanceAs<T,TInterface>() where T : TInterface, new()
        {
            Container.AddObjectInstanceAs<T,TInterface>();
        }

        public static T GetInstance<T>() where T : class
        {
            return Container.GetInstance<T>();
        }
    }
}