using SmartFamily.Core;
using SmartFamily.Core.Common;
using SmartFamily.Gedcom;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFamily.Data.Tests
{
    public partial class GEDCOMStoreTests
    {
        private readonly string _firstName = IndividualsResources.FirstName;
        private readonly Sex _individualsSex = (IndividualsResources.IndividualsSex == "Male") ? Sex.Male : Sex.Female;
        private readonly string _lastName = IndividualsResources.LastName;



        #region Other Helpers

        private Individual CreateTestIndividual()
        {
            return CreateTestIndividual(string.Empty);
        }

        private Individual CreateTestIndividual(string id)
        {
            // Create a test individual
            var newIndividual = new Individual
            {
                Id = id,
                FirstName = _firstName,
                LastName = _lastName,
                Sex = _individualsSex,
                TreeId = _treeId
            };

            // Return the individual
            return newIndividual;
        }

        private int GetIndividualCount(string file)
        {
            string fileName = Path.Combine(FilePath, file);
            Stream testStream = new FileStream(fileName, FileMode.Open);
            var doc = new GEDCOMDocument();
            doc.Load(testStream);

            return doc.IndividualRecords.Count;
        }

        #endregion
    }
}