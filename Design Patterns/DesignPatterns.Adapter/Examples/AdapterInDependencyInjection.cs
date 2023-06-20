using Autofac;
using Autofac.Features.Metadata;

namespace DesignPatterns.Adapter.Examples.AdapterInDependencyInjection
{
    public interface ICommand
    {
        void Execute();
    }

    public class SaveCommand : ICommand
    {
        public void Execute() { Console.WriteLine("Saving a file"); }
    }

    public class OpenCommand : ICommand
    {
        public void Execute() { Console.WriteLine("Opening a file"); }
    }

    public class Button
    {
        private ICommand _command;
        private string _name;

        public Button(ICommand command, string name)
        {
            _command = command;
            _name = name;
        }

        public void Click() => _command.Execute();

        public void PrintMe() => Console.WriteLine($"Button: {_name}");
    }

    public class Editor
    {
        private readonly IEnumerable<Button> _buttons;

        public IEnumerable<Button> Buttons => _buttons;

        public Editor(IEnumerable<Button> buttons) { _buttons = buttons; }

        public void ClickAll()
        {
            foreach(var button in _buttons)
                button.Click();
        }
    }

    public class AdapterInDependencyInjection
    {
        public static void Start(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<SaveCommand>().As<ICommand>().WithMetadata("Name", "Save");
            b.RegisterType<OpenCommand>().As<ICommand>().WithMetadata("Name", "Open");
            //b.RegisterType<Button>();
            //b.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd));
            b.RegisterAdapter<Meta<ICommand>, Button>(cmd => new Button(cmd.Value, (string)cmd.Metadata["Name"]!));
            b.RegisterType<Editor>();

            using var c = b.Build();
            var editor = c.Resolve<Editor>();
            //editor.ClickAll();

            foreach (var btn in editor.Buttons)
                btn.PrintMe();

        }
    }
}
