using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Facade.Examples
{
    public interface IDoThings
    {
        void Open();
        void DoThing();
        void Close();
    }
    public interface ITextureMangar : IDoThings { }
    public interface IViewport : IDoThings { }

    public class ConsoleFacade
    {
        private ITextureMangar _textureMangar;
        private IViewport _viewport;

        public ConsoleFacade()
        {
            // assign sub-systems
        }

        public void Write()
        {
            if(_textureMangar is null || _viewport is null) {
                Console.WriteLine("Oops");
                return;
            }

            _textureMangar.Open();
            _textureMangar.DoThing();
            _viewport.Open();
            _viewport.DoThing();
            _viewport.Close();
            _textureMangar.Close();
            Console.WriteLine("Imitate work");
        }
    }

    public class Facade
    {
        public static void Start(string[] args)
        {
            var con = new ConsoleFacade();
            con.Write();
        }
    }
}
