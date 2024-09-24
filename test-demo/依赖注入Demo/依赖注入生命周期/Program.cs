using Microsoft.Extensions.DependencyInjection;

namespace 依赖注入生命周期
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestAddTransient();
            TestAddSingleton();
        }

        private static void TestAddSingleton()
        {
            Console.WriteLine("测试 AddSingleton 所创建的对象状态");

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<TestServerOne>();

            // AddTransient
            using var sp = services.BuildServiceProvider();
            var t1 = sp.GetService<TestServerOne>();
            var t2 = sp.GetService<TestServerOne>();

            var referenceEqual = object.ReferenceEquals(t1, t2);
            Console.WriteLine($"referenceEqual: {referenceEqual}");
        }

        static void TestAddTransient()
        {
            Console.WriteLine("测试 AddTransient 所创建的对象状态");

            IServiceCollection services = new ServiceCollection();
            services.AddTransient<TestServerOne>();

            // AddTransient
            using var sp = services.BuildServiceProvider();
            var t1 = sp.GetService<TestServerOne>();
            var t2 = sp.GetService<TestServerOne>();

            var referenceEqual = object.ReferenceEquals(t1, t2);
            Console.WriteLine($"referenceEqual: {referenceEqual}");
        }
    }

    interface ITestServer
    {
        void Start();
    }

    class TestServerOne : ITestServer
    {
        public void Start() { Console.WriteLine($"{nameof(TestServerOne)},starting"); }
    }

    class TestServerTwo : ITestServer
    {
        public void Start() { Console.WriteLine($"{nameof(TestServerTwo)},starting"); }
    }

}
