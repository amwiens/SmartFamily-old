using SmartFamily.Gedcom.Tests.Common;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartFamily.Gedcom.Tests
{
    public class GEDCOMReaderTests : GEDCOMTestBase
    {
        #region Protected Properties

        protected override string EmbeddedFilePath => "SmartFamily.Gedcom.Tests.TestFiles.GEDCOMReaderTests";

        #endregion Protected Properties

        protected override Stream GetEmbeddedFileStream(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(GetEmbeddedFileName(fileName));
        }
    }
}