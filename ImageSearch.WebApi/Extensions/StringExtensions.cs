using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ImageSearch.WebApi.Extensions
{
    public static class StringExtensions
    {
        public static string ConvertUrlToImageName(this string url)
        {
            var imageExtensions = new[] { "gif", "jpeg", "png", "jpg" };
            var sb = new StringBuilder();

            var imageExtension = imageExtensions.FirstOrDefault(url.Contains);
            if (imageExtension != null)
            {
                var indexEndName = url.LastIndexOf(imageExtension, StringComparison.InvariantCultureIgnoreCase) + imageExtension.Length - 1;
                for (var i = indexEndName; i > 0; i--)
                {
                    var currentChar = url[i];
                    var availableCharList = new List<Char>() { '-', '_', '.' };

                    if (Char.IsLetterOrDigit(currentChar) || availableCharList.Contains(currentChar))
                    {
                        sb.Insert(0, currentChar);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return "unnamed.jpg";
        }
    }
}