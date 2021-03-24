using MonoRepo.Framework.Core.Domain;

namespace MonoRepo.Microservice.Application.Domain.Enums
{
    public class RelationType : Enumeration
    {
        public static readonly RelationType Father = new RelationType(1, "Father");
        public static readonly RelationType Mother = new RelationType(2, "Mother");
        public static readonly RelationType Guardian = new RelationType(3, "Guardian");

        public RelationType(int id, string name) : base(id, name)
        {
        }
    }
}
