using MonoRepo.Framework.Core.Domain;

namespace MonoRepo.Microservice.Application.Domain.Enums
{
    public class DegreeType : Enumeration
    {
        public static readonly DegreeType HighSchoolDiploma = new DegreeType(1, "High School Diploma");
        public static readonly DegreeType Associates = new DegreeType(2, "Associates");
        public static readonly DegreeType Bachelors = new DegreeType(3, "Bachelors");
        public static readonly DegreeType Masters = new DegreeType(4, "Masters");
        public static readonly DegreeType Phd = new DegreeType(5, "PhD");

        public DegreeType(int id, string name) : base(id, name)
        {
        }
    }
}
