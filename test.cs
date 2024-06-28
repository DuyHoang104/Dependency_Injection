using Microsoft.Extensions.DependencyInjection;
using System;
namespace DependencyInjection
{

    class Test
    {
        #region Không có DI 
        //public class ClassA
        //{
        //    private ClassB classB;

        //    public ClassA()
        //    {
        //        this.classB = new ClassB();
        //    }

        //    public void ActionA()
        //    {
        //        Console.WriteLine("ClassA");
        //        classB.ActionB();
        //    }
        //}

        //public class ClassB
        //{
        //     ClassC classC;

        //    public ClassB()
        //    {
        //        this.classC = new ClassC();
        //    }

        //    public void ActionB()
        //    {
        //        Console.WriteLine("ClassB");
        //        classC.ActionC();
        //    }
        //}

        //public class ClassC
        //{
        //    public void ActionC()
        //    {
        //        Console.WriteLine("ClassC");
        //    }
        //}

        //class Program
        //{
        //    static void Main(string[] args)
        //    {
        //        ClassA classA = new ClassA();
        //        classA.ActionA();
        //    }
        //}
        #endregion

        #region DI interface
        //public interface interfaceB
        //{
        //    void ActionB();
        //}
        //public interface interfaceC
        //{
        //    void ActionC();
        //}
        //public class classA
        //{
        //    interfaceB interfaceB;
        //    public classA(interfaceB interfaceB)
        //    {
        //        this.interfaceB = interfaceB;
        //    }
        //    public void ActionA()
        //    {
        //        Console.WriteLine("ActionA");
        //        interfaceB.ActionB();
        //    }
        //}
        //public class classB : interfaceB
        //{
        //    interfaceC interfaceC;
        //    public classB(interfaceC interfaceC)
        //    {
        //        this.interfaceC = interfaceC;
        //    }
        //    public void ActionB()
        //    {
        //        Console.WriteLine("ActionB");
        //        interfaceC.ActionC();
        //    }
        //}
        //public class classB1 : interfaceB
        //{
        //    interfaceC interfaceC;
        //    public classB1(interfaceC interfaceC)
        //    {
        //        this.interfaceC = interfaceC;
        //    }
        //    public void ActionB()
        //    {
        //        Console.WriteLine("ActionB");
        //        interfaceC.ActionC();
        //    }
        //}

        //public class classC : interfaceC
        //{
        //    public void ActionC()
        //    {
        //        Console.WriteLine("ActionC");
        //    }
        //}


        //class Program
        //{
        //    public static void Main(string[] args)
        //    {
        //        interfaceC interfaceC = new classC();
        //        interfaceB interfaceB = new classB1(interfaceC);
        //        classA classA = new classA(interfaceB);
        //        classA.ActionA();

        //    }

        //}
        #endregion

        #region Ví dụ
        public interface INotificationService
        {
            void SendNotification(string recipient, string message);
        }

        public class EmailNotificationService : INotificationService
        {
            public void SendNotification(string recipient, string message)
            {
                Console.WriteLine($"Sending email to {recipient}: {message}");
            }
        }

        public class SMSNotificationService : INotificationService
        {
            public void SendNotification(string recipient, string message)
            {
                Console.WriteLine($"Sending SMS to {recipient}: {message}");
            }
        }

        public class UserService
        {
            private readonly INotificationService _notificationService;

            public UserService(INotificationService notificationService)
            {
                _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            }

            public void NotifyUser(string username, string message)
            {
                _notificationService.SendNotification(username, message);
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                var services = new ServiceCollection();
                services.AddScoped<INotificationService, EmailNotificationService>();
                // Đăng ký UserService
                services.AddScoped<UserService>();

                var serviceProvider = services.BuildServiceProvider();

                // Lấy ra UserService từ DI container
                var userService = serviceProvider.GetService<UserService>();

                // Kiểm tra xem userService có null không
                if (userService != null)
                {
                    userService.NotifyUser("John Doe", "Welcome to our platform!");
                }
                else
                {
                    Console.WriteLine("UserService is not registered correctly.");
                }
            }
            #endregion
        }
    }

}
