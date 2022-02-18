namespace SmartFamily.DomainServices.Contracts.Services
{
    public interface IFamilyTreeServiceFactory
    {
        ICitationService CreateCitationService();

        IFactService CreateFactService();

        IFamilyService CreateFamilyService();

        IIndividualService CreateIndividualService();

        IMultimediaLinkService CreateMultimediaService();

        INoteService CreateNoteService();

        IRepositoryService CreateRepositoryService();

        ISourceService CreateSourceService();

        ITreeService CreateTreeService();
    }
}