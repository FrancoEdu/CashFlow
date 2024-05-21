using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.Report.PDF.Fonts;
public class ExpenseReportFontResolver : IFontResolver
{
    private const string PATH_TO_FONTS = "CashFlow.Application.UseCases.Report.PDF.Fonts";

    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);
        
        stream ??= ReadFontFile(FontHelp.DEFAULT_FONT);

        var length = stream!.Length;
        var data = new byte[length];
        stream.Read(buffer: data, offset: 0, count: (int)length);
        
        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return assembly.GetManifestResourceStream($"{PATH_TO_FONTS}.{faceName}.ttf"); // lê os arquivos de fonte do projeto application
    }
}
