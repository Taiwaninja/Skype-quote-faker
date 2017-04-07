using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuoteFaker
{
    /// <summary>
    /// A class used to fake a skype quote.
    /// </summary>
    public static class QuoteFaker
    {
        /// <summary>
        /// For some reason skype works with linux time... Epic xD
        /// </summary>
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Genereates the data object to be inserted into the clipboard
        /// </summary>
        /// <param name="user">The user to be quoted</param>
        /// <param name="quote">The text to be quoted</param>
        /// <param name="date">The date the quote has occured</param>
        /// <returns>The data object to be inserted to the clipboa</returns>
        public static DataObject GenerateQuote(string user, string quote, DateTime date)
        {
            var timeSpan = date.ToUniversalTime() - Epoch;

            var dataObject = new DataObject();

            var textFormatted = string.Format(
                "[{0:hh:mm:ss}] {1}: {2}",
                date,
                user,
                quote);
            var xmlFormatted = string.Format(
                "<quote author=\"{0}\" timestamp=\"{1}\">{2}</quote>",
                user,
                (int)timeSpan.TotalSeconds,
                quote);

            dataObject.SetData("System.String", textFormatted);
            dataObject.SetData("UnicodeText", textFormatted);
            dataObject.SetData("Text", textFormatted);
            dataObject.SetData("SkypeMessageFragment", new MemoryStream(Encoding.UTF8.GetBytes(xmlFormatted)));
            dataObject.SetData("Locale", new MemoryStream(BitConverter.GetBytes(CultureInfo.CurrentCulture.LCID)));
            dataObject.SetData("OEMText", textFormatted);

            return dataObject;
        }

    }
}
