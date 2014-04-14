using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace WeatherStation
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var kernel = new StandardKernel())
            {
                kernel.Bind<IDisplay>().To<ConsoleDisplay>();
                kernel.Bind<IWeatherStation>().To<WeatherStation>().WithConstructorArgument(

                        "weatherConditions",
                        new WeatherCondition[] { 
                            new WeatherCondition{ Name = "HOT", Color = "Yellow" }, 
	                        new WeatherCondition{ Name = "COLD", Color = "Blue" }, 
	                        new WeatherCondition{ Name = "STORM", Color = "DarkGray" }, 
	                        new WeatherCondition{ Name = "SNOW", Color = "White" }, 
	                        new WeatherCondition{ Name = "WINDY", Color = "Gray" }
                        }
                    );
                var weatherStation = kernel.Get<IWeatherStation>();
                weatherStation.DisplayReport();
                Console.ReadLine();
            }
        }
    }

    interface IWeatherStation
    {
        void DisplayReport();
    }

    interface IDisplay
    {
        void Write(string message);
        void SetColor(string color);
        void ResetColor();
    }

    class ConsoleDisplay : IDisplay
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void SetColor(string color)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
        }

        public void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    public class WeatherCondition
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }

    class WeatherStation : IWeatherStation
    {
        private readonly IDisplay _display;
        private readonly WeatherCondition[] _weatherConditions;
        private readonly Random _random;

        public WeatherStation(IDisplay display, WeatherCondition[] weatherConditions)
        {
            _display = display;
            _weatherConditions = weatherConditions;
            _random = new Random();
        }

        public void DisplayReport()
        {
            // Return a random weatherCondition within the _weatherConditions array
            var weatherCondition = _weatherConditions[_random.Next(0, _weatherConditions.Length)];
            _display.SetColor(weatherCondition.Color);
            _display.Write(String.Format("  {0} @ {1}", weatherCondition.Name, DateTime.Now.ToLongTimeString()));
            _display.ResetColor();
        }

    }
}
