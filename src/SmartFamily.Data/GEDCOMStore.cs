using SmartFamily.Core;
using SmartFamily.Core.Common;
using SmartFamily.Core.Guards;

namespace SmartFamily.Data
{
    public class GEDCOMStore : IGEDCOMStore
    {
        private GEDCOMDocument _document;
        private readonly string _path;
        private readonly string DEFAULT_TREE_ID = "1";

        public GEDCOMStore(string path)
        {
            Guard.Argument(path, nameof(path))
                .NotNull()
                .NotEmpty();

            _path = path;

            Initialize();
        }

        public List<Family> Families { get; private set; }

        public List<Individual> Individuals { get; private set; }

        public List<Repository> Repositories { get; private set; }

        public List<Source> Sources { get; private set; }

        private void CreateNewFamily(Individual individual)
        {
            var newFamily = new Family();

            if (!string.IsNullOrEmpty(individual.FatherId))
            {
                // New father
                newFamily.HusbandId = individual.FatherId;
            }
            if (!string.IsNullOrEmpty(individual.MotherId))
            {
                // New mother
                newFamily.WifeId = individual.MotherId;
            }

            newFamily.Children.Add(individual);

            // Save Family
            AddFamily(newFamily);
        }

        private GEDCOMFamilyRecord GetFamilyRecord(Individual individual)
        {
            string fatherId = individual.FatherId;
            string motherId = individual.MotherId;

            var familyRecord = !string.IsNullOrEmpty(fatherId)
                ? (!string.IsNullOrEmpty(motherId)
                    ? _document.SelectFamilyRecord(GEDCOMUtil.CreateId("I", fatherId), GEDCOMUtil.CreateId("I", motherId))
                    : _document.SelectHusbandFamilyRecords(GEDCOMUtil.CreateId("I", fatherId)).FirstOrDefault())
                : _document.SelectWifesFamilyRecords(GEDCOMUtil.CreateId("I", motherId)).FirstOrDefault();
            return familyRecord;
        }

        private void Initialize()
        {
            LoadDocument();

            ProcessIndividuals();
            ProcessFamilies();

            ProcessRepositories();

            ProcessSources();
        }

        private void LoadDocument()
        {
            _document = new GEDCOMDocument();

            using (var stream = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                _document.Load(stream);
            }
        }

        private void ProcessCitations(Entity entity, List<GEDCOMSourceCitationStructure> citations)
        {
            foreach (var citationStructure in citations)
            {
                if (citationStructure == null) continue;

                var newCitation = new Citation()
                {
                    Date = citationStructure.Date,
                    Page = citationStructure.Page,
                    Text = citationStructure.Text,
                    SourceId = GEDCOMUtil.GetId(citationStructure.XRefId),
                    OwnerId = entity.Id,
                    OwnerType = (entity is Individual)
                        ? EntityType.Individual
                        : (entity is Fact)
                            ? EntityType.Fact
                            : EntityType.Family
                };

                var factEntity = entity as Fact;
                if (factEntity != null)
                {
                    factEntity.Citations.Add(newCitation);
                }
                else
                {
                    var ancestorEntity = entity as AncestorEntity;
                    if (ancestorEntity != null)
                    {
                        ancestorEntity.Citations.Add(newCitation);
                    }
                }

                ProcessMultimedia(entity, citationStructure.Multimedia);

                ProcessNotes(entity, citationStructure.Notes);
            }
        }

        private void ProcessFacts(AncestorEntity entity, List<GEDCOMEventStructure> events)
        {
            foreach (var eventStructure in events)
            {
                var newFact = new Fact()
                {
                    Date = eventStructure.Date,
                    Place = (eventStructure.Place != null) ? eventStructure.Place.Data : String.Empty,
                    OwnerId = entity.Id,
                    OwnerType = (entity is Individual) ? EntityType.Individual : EntityType.Family
                };

                switch (eventStructure.EventClass)
                {
                    case EventClass.Individual:
                        newFact.FactType = eventStructure.IndividualEventType;
                        break;

                    case EventClass.Family:
                        newFact.FactType = eventStructure.FamilyEventType;
                        break;

                    case EventClass.Attribute:
                        newFact.FactType = eventStructure.IndividualAttributeType;
                        break;

                    default:
                        newFact.FactType = FactType.Unknown;
                        break;
                }
                entity.Facts.Add(newFact);

                ProcessMultimedia(newFact, eventStructure.Multimedia);

                ProcessNotes(newFact, eventStructure.Notes);

                ProcessCitations(newFact, eventStructure.SourceCitations);
            }
        }

        private void ProcessFamilies()
        {
            Families = new List<Family>();

            foreach (var gedcomRecord in _document.FamilyRecords)
            {
                var familyRecord = (GEDCOMFamilyRecord)gedcomRecord;
                var family = new Family
                {
                    Id = familyRecord.GetId(),
                    HusbandId = GEDCOMutil.GetId(familyRecord.Husband),
                    WifeId = GEDCOMUtil.GetId(familyRecord.Wife),
                    TreeId = DEFAULT_TREE_ID
                };

                ProcessFacts(family, familyRecord.Events);

                ProcessMultimedia(family, familyRecord.Multimedia);

                ProcessNotes(family, familyRecord.Notes);

                ProcessCitations(family, familyRecord.SourceCitations);

                foreach (string child in familyRecord.Children)
                {
                    var childId = GEDCOMUtil.GetId(child);
                    if (!string.IsNullOrEmpty(childId))
                    {
                        var individual = Individuals.SingleOrDefault(ind => ind.Id == childId);
                        if (individual != null)
                        {
                            individual.MotherId = family.WifeId;
                            individual.FatherId = family.HusbandId;
                        }
                    }
                }
                Families.Add(family);
            }
        }

        private void ProcessIndividuals()
        {
            Individuals = new List<Individual>();

            foreach (var gedcomRecord in _document.IndividualRecords)
            {
                var individualRecord = (GEDCOMIndividualRecord)gedcomRecord;
                var individual = new Individual
                {
                    Id = individualRecord.GetId(),
                    FirstName = (individualRecord.Name != null) ? individualRecord.Name.GivenName : string.Empty,
                    LastName = (individualRecord.Name != null) ? individualRecord.name.LastName : string.Empty,
                    Sex = individualRecord.Sex,
                    TreeId = DEFAULT_TREE_ID
                };

                ProcessFacts(individual, individualRecord.Events);

                ProcessMultimedia(individual, individualRecord.Multimedia);

                ProcessNotes(individual, individualRecord.Notes);

                ProcessCitations(individual, individualRecord.SourceCitations);

                Individuals.Add(individual);
            }
        }

        private void ProcessMultimedia(Entity entity, List<GEDCOMMultimediaStructure> multimedia)
        {
            foreach (var multimediaStructure in multimedia)
            {
                var multimediaLink = new MultimediaLink
                {
                    File = multimediaStructure.FileReference,
                    Format = multimediaStructure.Format,
                    Title = multimediaStructure.Title,
                    OwnerId = entity.Id,
                    OwnerType = (entity is Individual)
                        ? EntityType.Individual
                        : (entity is Fact)
                            ? EntityType.Fact
                            : (entity is Family)
                                ? EntityType.Family
                                : EntityType.Citation
                };

                entity.Multimedia.Add(multimediaLink);
            }
        }

        private void ProcessNote(Entity entity, string noteText)
        {
            if (!string.IsNullOrEmpty(noteText))
            {
                var newNote = new Note
                {
                    Text = noteText,
                    OwnerId = entity.Id
                };
                if (entity is Individual)
                {
                    newNote.OwnerType = EntityType.Individual;
                }
                else if (entity is Family)
                {
                    newNote.OwnerType = EntityType.Family;
                }
                else if (entity is Fact)
                {
                    newNote.OwnerType = EntityType.Fact;
                }
                else if (entity is Source)
                {
                    newNote.OwnerType = EntityType.Source;
                }
                else if (entity is Repository)
                {
                    newNote.OwnerType = EntityType.Repository;
                }
                entity.Notes.Add(newNote);
            }
        }

        private void ProcessNotes(Entity entity, List<GEDCOMNoteStructure> notes)
        {
            foreach (var noteStructure in notes)
            {
                if (string.IsNullOrEmpty(noteStructure.XRefId))
                {
                    ProcessNote(entity, noteStructure.Text);
                }
                else
                {
                    var noteRecord = _document.NoteRecords[noteStructure.XRefId] as GEDCOMNoteRecord;
                    if (noteRecord != null && !string.IsNullOrEmpty(noteRecord.Data))
                    {
                        ProcessNote(entity, noteRecord.Data);
                    }
                }
            }
        }

        private void ProcessRepositories()
        {
            Repositories = new List<Repository>();

            foreach (var gedcomRecord in _document.RepositoryRecords)
            {
                var repositoryRecord = (GEDCOMRepositoryRecord)gedcomRecord;
                var repository = new Repository
                {
                    Id = repositoryRecord.GetId(),
                    Address = repositoryRecord.Address.Address,
                    Name = repositoryRecord.Name,
                    TreeId = DEFAULT_TREE_ID
                };

                ProcessNotes(repository, repositoryRecord.Notes);

                Repositories.Add(repository);
            }
        }

        private void ProcessSources()
        {
            Sources = new List<Source>();

            foreach (var gedcomRecord in _document.SourceRecords)
            {
                var sourceRecord = (GEDCOMSourceRecord)gedcomRecord;
                var source = new Source
                {
                    Id = sourceRecord.GetId(),
                    Author = sourceRecord.Author,
                    Title = sourceRecord.Title,
                    Publisher = sourceRecord.PublisherInfo,
                    TreeId = DEFAULT_TREE_ID
                };
                if (sourceRecord.SourceRepository != null)
                {
                    source.RepositoryId = GEDCOMUtil.GetId(sourceRecord.SourceRepository.XRefId);
                }

                ProcessNotes(source, sourceRecord.Notes);

                Sources.Add(source);
            }
        }

        private static void RemoveIndividualFromFamilyRecord(Individual child, GEDCOMRecord familyRecord, GEDCOMTag tag)
        {
            var childRecord = (from GEDCOMRecord record in familyRecord.ChildRecords.GetLinesByTag(tag)
                               where record.XRefId == GEDCOMUtil.CreateId("I", child.Id)
                               select record).SingleOrDefault();

            if (childRecord != null)
            {
                familyRecord.ChildRecords.Remove(childRecord);
            }
        }

        private void UpdateFamilyDetails(Individual individual)
        {
            var familyRecord = _document.SelectChildsFamilyRecord(GEDCOMUtil.CreateId("I", individual.Id));

            if (familyRecord != null)
            {
                if (individual.FatherId != GEDCOMUtil.GetId(familyRecord.Husband) || individual.MotherId != GEDCOMUtil.GetId(familyRecord.Wife))
                {
                    // remove child from current family
                    RemoveIndividualFromFamilyRecord(individual, familyRecord, GEDCOMTag.CHIL);

                    familyRecord = GetFamilyRecord(individual);

                    if (familyRecord != null)
                    {
                        // Add Individual as Child
                        familyRecord.AddChild(GEDCOMUtil.CreateId("I", individual.Id));
                    }
                    else
                    {
                        // new Family
                        CreateNewFamily(individual);
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(individual.FatherId) || !string.IsNullOrEmpty(individual.MotherId))
                {
                    familyRecord = GetFamilyRecord(individual);

                    if (familyRecord != null)
                    {
                        // Add Individual as Child
                        familyRecord.AddChild(GEDCOMUtil.CreateId("I", individual.Id));
                    }
                    else
                    {
                        // new Family
                        CreateNewFamily(individual);
                    }
                }
            }
        }

        public void AddFamily(Family family)
        {
            Guard.Argument(family, nameof(family)).NotNull();

            family.Id = _document.Records.GetNextId(GEDCOMTag.FAM).ToString();

            var record = new GEDCOMFamilyRecord(family.Id);
            if (!string.IsNullOrEmpty(family.HusbandId))
            {
                // Add HUSB
                record.AddHusband(GEDCOMUtil.CreateId("I", family.HusbandId));
            }

            if (!string.IsNullOrEmpty(family.WifeId))
            {
                // Add WIFE
                record.AddWife(GEDCOMUtil.CreateId("I", family.WifeId));
            }

            foreach (Individual child in family.Children)
            {
                // Add CHIL
                record.AddChild(GEDCOMUtil.CreateId("I", child.Id));
            }

            _document.AddRecord(record);
        }

        public void AddIndividual(Individual individual)
        {
            Guard.Argument(individual, nameof(individual)).NotNull();

            // Add to internal List
            Individuals.Add(individual);

            // Add underlying GEDCOM record
            individual.Id = _document.Records.GetNextId(GEDCOMTag.INDI).ToString();

            var record = new GEDCOMIndividualRecord(individual.Id);

            var name = new GEDCOMNameStructure($"{individual.FirstName} /{individual.LastName}/", record.Level + 1);

            record.Name = name;
            record.Sex = individual.Sex;
            _document.AddRecord(record);

            // Update Family Info
            UpdateFamilyDetails(individual);
        }

        public void DeleteFamily(Family family)
        {
            Guard.Argument(family, nameof(family)).NotNull();

            GEDCOMFamilyRecord record = _document.SelectFamilyRecord(GEDCOMUtil.CreateId("F", family.Id));

            if (record == null)
            {
                // record not in repository
                throw new ArgumentOutOfRangeException();
            }

            _document.RemoveRecord(record);
        }

        public void DeleteIndividual(Individual individual)
        {
            Guard.Argument(individual, nameof(individual)).NotNull();

            string individualId = GEDCOMUtil.CreateId("I", individual.Id);

            // Remove from internal List
            Individuals.Remove(individual);

            GEDCOMIndividualRecord record = _document.SelectIndividualRecord(individualId);

            if (record == null)
            {
                // record not in repository
                throw new ArgumentOutOfRangeException();
            }

            _document.RemoveRecord(record);

            // see if individual is a child in a family
            var familyRecord = _document.SelectChildsFamilyRecord(individualId);
            if (familyRecord != null)
            {
                // remove child from family
                RemoveIndividualFromFamilyRecord(individual, familyRecord, GEDCOMTag.CHIL);
            }

            // TODO: check against both for same sex families
            if (individual.Sex == Sex.Male)
            {
                // see if individual is a husband in a family
                foreach (GEDCOMFamilyRecord fRecord in _document.SelectHusbandsFamilyRecords(individualId))
                {
                    // remove husband from family
                    RemoveIndividualFromFamilyRecord(individual, fRecord, GEDCOMTag.HUSB);
                }
            }
            else
            {
                // see if individual is a wife in a family
                foreach (GEDCOMFamilyRecord fRecord in _document.SelectWifesFamilyRecords(individualId))
                {
                    // remove wife from family
                    RemoveIndividualFromFamilyRecord(individual, fRecord, GEDCOMTag.WIFE);
                }
            }
        }

        public void SaveChanges()
        {
            using (var stream = new FileStream(_path, FileMode.Create, FileAccess.Write))
            {
                _document.Save(stream);
            }
        }

        public void UpdateFamily(Family family)
        {
            Guard.Argument(family, nameof(family)).NotNull();

            GEDCOMFamilyRecord record = _document.SelectFamilyRecord(GEDCOMUtil.CreateId("F", family.Id));
            if (record == null)
            {
                // record not in repository
                throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateIndividual(Individual individual)
        {
            Guard.Argument(individual, nameof(individual)).NotNull();

            GEDCOMIndividualRepository record = _document.SelectIndividualRecord(GEDCOMUtil.CreateId("I", individual.Id));
            if (record == null)
            {
                // record not in repository
                throw new ArgumentOutOfRangeException();
            }

            record.Name = new GEDCOMNameStructure($"{individual.FirstName} /{individual.LastName}/", record.Level + 1);
            record.Sex = individual.Sex;

            // Update Family Info
            UpdateFamilyDetails(individual);
        }
    }
}