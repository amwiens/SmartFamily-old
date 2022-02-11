using SmartFamily.Core;

namespace SmartFamily.Data
{
    public interface IGEDCOMStore
    {
        List<Family> Families { get; }

        List<Individual> Individuals { get; }

        void AddFamily(Family family);

        void AddIndividual(Individual individual);

        void DeleteFamily(Family family);

        void DeleteIndividual(Individual individual);

        void SaveChanges();

        void UpdateFamily(Family family);

        void UpdateIndividual(Individual individual);
    }
}