#region  Dependency Injection(How to use it in .NET Core)
// Thư viện sử dụng DI : ServiceCollection
// Đăng kí dịch vụ vào ServiceCollection : Singleton , Transient , Scoped
// Singleton : Mỗi lần lấy ra sẽ trả về cùng 1 đối tượng, không tạo mới
// Transient : Mỗi lần lấy ra sẽ tạo mới 1 đối tượng
// Scoped : Mỗi request sẽ trả về cùng 1 đối tượng, không tạo mới
#endregion
#region DI interface
using Microsoft.Extensions.DependencyInjection;

public interface IHorn
{
    void Beep();
}
class Horn : IHorn
{
    public void Beep()
    {
        Console.WriteLine("Beep");
    }
}
class BMWHorn : IHorn
{
    public void Beep()
    {
        Console.WriteLine("Bop-bop");
    }
}
class MercHorn : IHorn
{
    string message; 
    public MercHorn( string msg ){
        this.message = msg;
    }
    public void Beep()
    {
        Console.WriteLine( message + " leng-keng");
    }
}
class Car
{
    IHorn horn;
    public Car(IHorn horn)
    {
        this.horn = horn;
    }
    public void Beep()
    {
        horn.Beep();
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Thư viện sử dụng DI : ServiceCollection
        var services = new ServiceCollection();

        // Đăng ký dịch vụ vào ServiceCollection : Singleton , Transient , Scoped
        services.AddScoped<Car,Car>();
        //Đăng kí dịch vụ không có tham số truyền vào
        services.AddScoped<IHorn, Horn>();
        //Đăng kí dịch vụ với tham số truyền vào
        services.AddScoped<IHorn, MercHorn>(
            (ServiceProvider) => new MercHorn("Merc")
        );
        //Tạo một ServiceProvider chứa các dịch vụ từ IServiceCollection được cung cấp.
        var ServiceProvider = services.BuildServiceProvider();

        //Lấy ra dịch vụ từ DI container
        Car car = ServiceProvider.GetService<Car>();
        car.Beep();
    }
}
#endregion