namespace Factories
{
    namespace ReferencesContainers
    {
        public static class MainObjectsReferencesContainer
        {
            private static readonly SimpleAutofacContainer Container = new SimpleAutofacContainer();

            public static void AddObjectInstance(object objectInstance)
            {
                Container.AddObjectInstance(objectInstance);
            }

            public static T GetObjectInstance<T>() where T : class
            {
                return Container.GetObjectInstance<T>();
            }
        }

        public static class DataRepositoriesReferencesContainer
        {
            private static readonly SimpleAutofacContainer Container = new SimpleAutofacContainer();

            public static void AddObjectInstance(object objectInstance)
            {
                Container.AddObjectInstance(objectInstance);
            }

            public static void CreateObjectInstance<T>() where T : class, new()
            {
                Container.AddObjectInstance(new T());
            }

            public static T GetObjectInstance<T>() where T : class
            {
                return Container.GetObjectInstance<T>();
            }
        }
    }
}