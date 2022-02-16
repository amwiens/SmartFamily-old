using SmarFamily.TestUtilities;

using SmartFamily.Core;
using SmartFamily.DomainServices.Services;

using System.Collections.Generic;

namespace SmartFamily.DomainServices.Tests
{
    public class NoteServiceTests : EntityServiceBaseTests<Note, NoteService>
    {
        protected override IEnumerable<Note> GetEntities(int count)
        {
            var notes = new List<Note>();

            for (int i = 0; i < count; i++)
            {
                notes.Add(new Note
                {
                    Id = i.ToString(),
                    Text = "Foo",
                    TreeId = TestConstants.TREE_Id
                });
            }

            return notes;
        }

        protected override Note NewEntity()
        {
            return new Note { Text = "Foo" };
        }

        protected override Note UpdateEntity()
        {
            return new Note { Id = TestConstants.ID_Exists, Text = "Foo" };
        }
    }
}