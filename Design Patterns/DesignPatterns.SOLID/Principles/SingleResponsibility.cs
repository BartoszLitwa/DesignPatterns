using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID.Principles
{
    public class Journal
    {
        public readonly List<string> entries = new();
        private static int count = 0;
        
        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count;
        }

        public int RemoveEntry(int index)
        {
            entries.RemoveAt(index);
            return --count;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    public class Persistence<T>
    {
        public void SaveToFiel(T obj, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, obj!.ToString());
        }
    }

    public class SingleResponsibility
    {
        public static void Start(string[] args)
        {
            var j = new Journal();
            j.AddEntry("First entry");
            j.AddEntry("Second entry");
            Console.WriteLine(j);

            var p = new Persistence<Journal>();
            var filename = @"C:\temp\journal.txt";
            p.SaveToFiel(j, filename, true);
            // Open the text file
            Process.Start("notepad.exe", filename);
        }
    }
}
