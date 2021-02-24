using MonoRepo.Framework.Core.Domain;

namespace MonoRepo.Microservice.Application.Domain.Enums
{
    public class PhoneType : Enumeration
    {
        public static readonly PhoneType LandLine = new PhoneType(1, "Land Line");
        public static readonly PhoneType Mobile = new PhoneType(2, "Mobile");
        public PhoneType(int id, string name) : base(id, name) { }
    }
}
