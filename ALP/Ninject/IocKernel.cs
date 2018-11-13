using Ninject;
using Ninject.Modules;

namespace ALP.Ninject
{
    /// <summary>
    /// Gives access to the kernel container
    /// Used to retrieve modules loaded into the kernel
    /// </summary>
    public static class IocKernel
    {
        /// <summary>
        /// The kernel of the applicaiton
        /// </summary>
        private static StandardKernel _kernel;

        /// <summary>
        /// Retrieves a module from the kernel container by its Type
        /// </summary>
        /// <typeparam name="T">The type of the required modules</typeparam>
        /// <returns>The required moduls</returns>
        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }

        /// <summary>
        /// Initializez a new Kernel container
        /// Loads any modules that it receives as a parameter into the kernel
        /// </summary>
        /// <param name="modules">The modules that are already loaded into the kernel</param>
        public static void Initialize(params INinjectModule[] modules)
        {
            if (_kernel == null)
            {
                _kernel = new StandardKernel(modules);
            }
        }
    }
}
