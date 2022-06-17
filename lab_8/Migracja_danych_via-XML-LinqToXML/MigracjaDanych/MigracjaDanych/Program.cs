using System;

namespace MigracjaDanych
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MigracjaDanych.Migration.MigrateToNewArticle(@"E:\Projects\Programowanie_Obiektowe\lab_8\Migracja_danych_via-XML-LinqToXML\MigracjaDanych\MigracjaDanych\issues.xml", @"E:\Projects\Programowanie_Obiektowe\lab_8\Migracja_danych_via-XML-LinqToXML\MigracjaDanych\MigracjaDanych\NoweArtykuly\");
        }
    }
}
