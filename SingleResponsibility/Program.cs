﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace SingleResponsibility
{

    //just stores a couple of journal entries and ways of
    //working with them
    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; //memento pattern!
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }


        //breaks single responsibility principle
        public void save(string filename, bool overwrite = false)
        {
            File.WriteAllText(filename,ToString());
        }

        public void Load(string filename)
        {

        }

        public void Load(Uri uri)
        {

        }
    }

    //handles the responsibility of persisting objects
    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false)
        {
            if(overwrite || !File.Exists(filename))
                File.WriteAllText(filename,journal.ToString());
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I Love Android,Angular & ASP.NET");
            j.AddEntry("I Love building products");
            Console.WriteLine(j);

            var p = new Persistence();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(j,filename);
            Process.Start(filename);
        }
    }
}
