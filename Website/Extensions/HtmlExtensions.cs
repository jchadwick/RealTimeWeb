using System.IO;
using System.Web;
using System.Web.Mvc;

public static class HtmlExtensions
{

    public static IHtmlString RenderSourceCode(this HtmlHelper helper, string relativePath)
    {
        var serverPath = helper.ViewContext.HttpContext.Server.MapPath(relativePath);

        if (!File.Exists(serverPath))
            return new HtmlString(string.Format("<!-- File not found: {0} -->", serverPath));

        return new HtmlString(string.Format("<label>{0}</label><pre>{1}</pre>", relativePath, File.ReadAllText(serverPath)));
    }

}