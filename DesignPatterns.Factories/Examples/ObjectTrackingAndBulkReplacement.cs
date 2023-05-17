using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factories.Examples
{
    public interface ITheme
    {
        string TextColor { get; }
        string BackgroundColor { get; }
    }

    public class LightTheme : ITheme
    {
        public string TextColor => "Black";

        public string BackgroundColor => "White";
    }

    public class DarkTheme : ITheme
    {
        public string TextColor => "White";

        public string BackgroundColor => "Dark Grey";
    }

    public class TrackingThemeFactory
    {
        private readonly List<WeakReference<ITheme>> themes = new();

        public ITheme CreateTheme(bool dark)
        {
            ITheme theme = dark ? new DarkTheme() : new LightTheme();
            themes.Add(new WeakReference<ITheme>(theme));
            return theme;
        }

        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var reference in themes)
                {
                    if(reference.TryGetTarget(out var theme))
                    {
                        bool isDark = theme is DarkTheme;
                        sb.Append(isDark ? "Dark" : "Light")
                            .AppendLine(" theme");
                    }
                }
                return sb.ToString();
            }
        }
    }

    public class Ref<T> where T : class
    {
        public T Value;
        
        public Ref(T value)
        {
            Value = value;
        }
    }

    public class ReplecableThemeFactory
    {
        private readonly List<WeakReference<Ref<ITheme>>> themes = new();

        private ITheme createThemeImpl(bool dark)
        {
            return dark ? new DarkTheme() : new LightTheme();
        }

        public Ref<ITheme> CreateTheme(bool dark)
        {
            var r = new Ref<ITheme>(createThemeImpl(dark));
            themes.Add(new WeakReference<Ref<ITheme>>(r));
            return r;
        }

        public void ReplaceTheme(bool dark)
        {
            foreach(var weakReference in themes)
            {
                if(weakReference.TryGetTarget(out var reference))
                {
                    reference.Value = createThemeImpl(dark);
                }
            }
        }
    }

    public class ObjectTrackingAndBulkReplacement
    {
        public static void Start(string[] args)
        {
            var factory = new TrackingThemeFactory();
            var theme1 = factory.CreateTheme(true);
            var theme2 = factory.CreateTheme(false);
            Console.WriteLine(factory.Info);

            var repleacableFactory = new ReplecableThemeFactory();
            var themeR = repleacableFactory.CreateTheme(true);
            Console.WriteLine(themeR.Value.BackgroundColor);
            repleacableFactory.ReplaceTheme(false);
            Console.WriteLine(themeR.Value.BackgroundColor);
        }
    }
}
