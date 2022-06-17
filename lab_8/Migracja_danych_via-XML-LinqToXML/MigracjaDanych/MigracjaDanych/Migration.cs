using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MigracjaDanych
{
    internal static class Migration
    {
        private static XNamespace ns = "http://pkp.sfu.ca";
        private static XNamespace xsiNs = "http://www.w3.org/2001/XMLSchema-instance";

        public static string MigrateToNewArticle(string filePath, string targetPath = null)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(filePath);
            if (string.IsNullOrEmpty(targetPath))
                targetPath = filePath.Substring(0, filePath.LastIndexOf("\\") + 1);

            try
            {
                var oldArtictle = XDocument.Load(filePath).Element("issues").Element("issue");

                var volume = oldArtictle.Element("volume")?.Value ?? "";
                var number = (oldArtictle.Element("number")?.Value ?? "").Substring(0, (oldArtictle.Element("number")?.Value ?? "").IndexOf('('));
                var year = oldArtictle.Element("year")?.Value ?? "";
                var datePublished = oldArtictle.Element("date_published")?.Value ?? "";
                var sectionRef = oldArtictle.Element("section").Elements("abbrev");

                foreach (var article in (IEnumerable<XElement>)oldArtictle.Element("section").Elements("article"))
                {
                    var remote = article.Element("galley").Element("file").Element("remote").Attribute("src")?.Value ?? "";
                    var fileName = remote.Substring(remote.Length - 9);
                    var locale = article.Attribute("locale")?.Value ?? "";
                    var newArticle = CreateArticleTemplate().Element(ns + "article");
                    newArticle.Attribute("locale").SetValue(locale);
                    newArticle.Attribute("date_published").SetValue(article.Element("date_published")?.Value ?? "");
                    newArticle.Element(ns + "id").SetValue(fileName.Replace("-", ""));
                    var sectionRefForArticle = sectionRef.FirstOrDefault(x => (x.Attribute("locale")?.Value ?? "") == locale)?.Value ?? "";
                    newArticle.Attribute("section_ref").SetValue(sectionRefForArticle);

                    newArticle.Element(ns + "title").Remove();
                    newArticle.Element(ns + "prefix").Remove();
                    newArticle.Element(ns + "abstract").Remove();
                    foreach (var title in (IEnumerable<XElement>)article.Elements(ns + "title"))
                    {
                        var element = newArticle.Element(ns + "title");
                        if (element == null)
                            element = newArticle.Element(ns + "id");

                        var value = title?.Value ?? "";
                        var prefix = string.Empty;

                        if (value.StartsWith("The") || value.StartsWith("A") || value.StartsWith("An"))
                        {
                            prefix = value.Substring(0, value.IndexOf(" "));
                            value = value.Substring(value.IndexOf(" "));
                        }
                        element.AddAfterSelf(new XElement(ns + "title", value,
                                                new XAttribute("locale", title.Attribute("locale")?.Value ?? "")));

                        element = newArticle.Element(ns + "title");
                        element.AddAfterSelf(new XElement(ns + "prefix", value.Substring(0, value.IndexOf(" ")),
                                                 new XAttribute(ns + "locale", title.Attribute("locale")?.Value ?? "")));
                    }
                    foreach (var abstractArticle in (IEnumerable<XElement>)article.Elements(ns + "abstract"))
                    {
                        var value = abstractArticle?.Value ?? "";
                        var elements = newArticle.Elements(ns + "abstract");
                        XElement element = null;
                        if (!elements.Any())
                            element = newArticle.Element(ns + "prefix");
                        else element = elements.Last();

                        element.AddAfterSelf(new XElement(ns + "abstract", value,
                                                new XAttribute("locale", abstractArticle.Attribute("locale")?.Value ?? "")));
                    }

                    newArticle.Element(ns + "licenseUrl").SetValue(article.Element("permissions").Element("license_url")?.Value ?? "");

                    newArticle.Element(ns + "keywords").Remove();
                    foreach (var subject in (IEnumerable<XElement>)article.Element("indexing").Elements("subject"))
                    {
                        var keywordsList = (subject?.Value ?? "").Split("; ");
                        var keywords = new XElement(ns + "keywords", null,
                                                new XAttribute("locale", subject.Attribute("locale")?.Value ?? ""));
                        foreach (var keyword in keywordsList)
                        {
                            keywords.Add(new XElement(ns + "keyword", keyword));
                        }

                        var elements = newArticle.Elements(ns + "keywords");
                        XElement element = null;
                        if (!elements.Any())
                            element = newArticle.Element(ns + "licenseUrl");
                        else element = elements.Last();
                        element.AddAfterSelf(keywords);
                    }

                    foreach (var author in (IEnumerable<XElement>)article.Elements("author"))
                    {
                        newArticle.Element(ns + "authors").Add(ChangeAuthorStructure(author));
                    }

                    newArticle.Element(ns + "article_galley").Element(ns + "id").SetValue(fileName.Substring(fileName.LastIndexOf("-") + 1).TrimStart(new char[] { '0' }));
                    newArticle.Element(ns + "article_galley").Element(ns + "name").SetValue(article.Element("galley").Element("label")?.Value ?? "");
                    newArticle.Element(ns + "article_galley").Element(ns + "name").Attribute("locale").SetValue(article.Element("galley").Attribute("locale")?.Value ?? "");
                    newArticle.Element(ns + "article_galley").Element(ns + "remote").Attribute("src").SetValue(remote);

                    newArticle.Element(ns + "issue_identification").Element(ns + "volume").SetValue(volume);
                    newArticle.Element(ns + "issue_identification").Element(ns + "number").SetValue(number);
                    newArticle.Element(ns + "issue_identification").Element(ns + "year").SetValue(year);
                    newArticle.Element(ns + "pages").SetValue(article.Element(ns + "pages")?.Value ?? "");

                    newArticle.Save(targetPath + fileName + ".xml");
                }
            }
            catch (Exception e)
            {
                // Logi
                return "Migracja nie powiodła się";
            }

            return "Migracja powiodła się";
        }

        private static XDocument CreateArticleTemplate()
        => new XDocument(
                new XDeclaration("1.0", "", null),
                new XElement(ns + "article",
                        new XAttribute("xmlns", "http://pkp.sfu.ca"),
                        new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                        new XAttribute("locale", ""),
                        new XAttribute("date_submitted", DateTime.Today.ToString("yyyy-MM-dd")),
                        new XAttribute("stage", "production"),
                        new XAttribute("date_published", ""),
                        new XAttribute("section_ref", ""),
                        new XAttribute("seq", "1"),
                        new XAttribute("access_status", "0"),
                        new XAttribute(xsiNs + "schemaLocation", "http://pkp.sfu.ca native.xsd"),
                    new XElement(ns + "id",
                        new XAttribute("type", "internal"),
                        new XAttribute("advice", "ignore")),
                    new XElement(ns + "title",
                        new XAttribute("locale", "")),
                    new XElement(ns + "prefix",
                        new XAttribute("locale", "")),
                    new XElement(ns + "abstract",
                        new XAttribute("locale", "")),
                    new XElement(ns + "licenseUrl"),
                    new XElement(ns + "keywords",
                        new XAttribute("locale", "")),
                    new XElement(ns + "authors",
                        new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                        new XAttribute(xsiNs + "schemaLocation", "http://pkp.sfu.ca native.xsd")),
                    new XElement(ns + "article_galley",
                            new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                            new XAttribute("approved", "false"),
                            new XAttribute(xsiNs + "schemaLocation", "http://pkp.sfu.ca native.xsd"),
                        new XElement(ns + "id",
                            new XAttribute("type", "internal"),
                            new XAttribute("advice", "ignore")),
                        new XElement(ns + "name",
                            new XAttribute("locale", "")),
                        new XElement(ns + "seq", "0"),
                        new XElement(ns + "remote",
                            new XAttribute("src", ""))),
                    new XElement(ns + "issue_identification",
                        new XElement(ns + "volume"),
                        new XElement(ns + "number"),
                        new XElement(ns + "year")),
                    new XElement(ns + "pages")
                ));

        private static XElement ChangeAuthorStructure(XElement oldAuthorElement)
        {
            var locale = oldAuthorElement.Element("affiliation").Attribute("locale")?.Value ?? "";

            return new XElement(ns + "author",
                        new XAttribute("include_in_browse", oldAuthorElement.Attribute("primary_contact")?.Value ?? "false"),
                        new XAttribute("user_group_ref", "Author"),
                    new XElement(ns + "givenname", oldAuthorElement.Element("firstname")?.Value ?? "",
                        new XAttribute("locale", locale)),
                    new XElement(ns + "familyname", oldAuthorElement.Element("lastname")?.Value ?? "",
                        new XAttribute("locale", locale)),
                    new XElement(ns + "affiliation", oldAuthorElement.Element("affiliation")?.Value ?? "",
                        new XAttribute("locale", locale)),
                    new XElement(ns + "country", oldAuthorElement.Element("country")?.Value ?? ""),
                    new XElement(ns + "email", oldAuthorElement.Element("email")?.Value ?? "")
                );
        }
            
    }
}