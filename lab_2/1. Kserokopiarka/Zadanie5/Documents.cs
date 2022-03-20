namespace ver5
{
    /// <summary>
    /// Interfejs dokumentu
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Obsługiwane typy dokumentów
        /// </summary>
        enum FormatType { TXT, PDF, JPG }

        /// <summary>
        /// Zwraca typ formatu dokumentu
        /// </summary>
        FormatType GetFormatType();

        /// <summary>
        /// Zwraca nazwę pliku dokumentu - nie może być `null` ani pusty `string`
        /// </summary>
        string GetFileName();
    }

    /// <summary>
    /// Abstrakcyjna klasa dokumentu
    /// </summary>
    public abstract class AbstractDocument : IDocument
    {
        /// <summary>
        /// Nazwa pliku
        /// </summary>
        private string fileName;

        /// <summary>
        /// Domyślny konstruktor
        /// </summary>
        /// <param name="fileName">Nazwa pliku</param>
        public AbstractDocument(string fileName) => this.fileName = fileName;

        /// <summary>
        /// Zwraca nazwe pliku
        /// </summary>
        /// <returns>Nazwa pliku</returns>
        public string GetFileName() => fileName;

        /// <summary>
        /// Zmienia nazwe pliku
        /// </summary>
        /// <param name="newFileName">Nowa nazwa pliku</param>
        public void ChangeFileName(string newFileName) => fileName = newFileName;

        /// <summary>
        /// Zwraca typ dokumentu
        /// </summary>
        /// <returns>Typ dokumentu</returns>
        public abstract IDocument.FormatType GetFormatType();
    }

    /// <summary>
    /// Dokument PDF
    /// </summary>
    public class PDFDocument : AbstractDocument
    {
        /// <summary>
        /// Domyślny konstruktor
        /// </summary>
        /// <param name="filename">Nazwa pliku</param>
        public PDFDocument(string filename) : base(filename) { }

        /// <summary>
        /// Zwraca typ dokumentu
        /// </summary>
        /// <returns>Typ dokumentu</returns>
        public override IDocument.FormatType GetFormatType() => IDocument.FormatType.PDF;
    }

    /// <summary>
    /// Obraz
    /// </summary>
    public class ImageDocument : AbstractDocument
    {
        /// <summary>
        /// Domyślny konstruktor
        /// </summary>
        /// <param name="filename">Nazwa pliku</param>
        public ImageDocument(string filename) : base(filename) { }

        /// <summary>
        /// Zwraca typ dokumentu
        /// </summary>
        /// <returns>Typ dokumentu</returns>
        public override IDocument.FormatType GetFormatType() => IDocument.FormatType.JPG;
    }

    /// <summary>
    /// Dokument tekstowy
    /// </summary>
    public class TextDocument : AbstractDocument
    {
        /// <summary>
        /// Domyślny konstruktor
        /// </summary>
        /// <param name="filename">Nazwa pliku</param>
        public TextDocument(string filename) : base(filename) { }
        /// <summary>
        /// 
        /// Zwraca typ dokumentu
        /// </summary>
        /// <returns>Typ dokumentu</returns>
        public override IDocument.FormatType GetFormatType() => IDocument.FormatType.TXT;
    }
}