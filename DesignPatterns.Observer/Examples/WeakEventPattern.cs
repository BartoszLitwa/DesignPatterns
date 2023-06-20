namespace DesignPatterns.Observer.Examples
{
    public class Button
    {
        public event EventHandler Clicked;

        public void Fire()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Window
    {
        public Window(Button button)
        {
            button.Clicked += ButtonOnClicked;
            // .NET Framework
            //WeakEventManager<Button, EventArgs>
            //    .AddHandler(button, "Clicked", ButtonOnClicked);
        }
        private void ButtonOnClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Button Clicked (window handler)");
        }

        ~Window()
        {
            Console.WriteLine("Window Destructor");
        }
    }

    public class WeakEventPattern
    {
        public static void Start(string[] args)
        {
            var btn = new Button();
            var window = new Window(btn);
            var windowRef = new WeakReference(window);
            btn.Fire();

            Console.WriteLine("Setting window to null");
            window = null;

            FireGC();
            Console.WriteLine($"Is window Alive: {windowRef.IsAlive}");
        }

        private static void FireGC()
        {
            Console.WriteLine("Starting GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Console.WriteLine("Ending GC");
        }
    }
}
