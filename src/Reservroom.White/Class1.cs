using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using Xunit;

namespace Reservroom.White
{
    public class Tests
    {
        [Fact]
        public void GettingStarted()
        {
            Application application = Application.Launch(
                @"E:\TechSnovel\Learn\OOP\LearnMVVM\reservoom\src\Reservoom\bin\Debug\net5.0-windows\win-x64\Reservoom.exe"
                );
            Window main = application.GetWindow("");

        }
    }
}