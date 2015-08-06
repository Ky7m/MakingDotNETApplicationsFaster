using System;

namespace MakingDotNETApplicationsFaster
{
    struct StructWithRefTypeAndEquatableImplementation : IEquatable<StructWithRefTypeAndEquatableImplementation>
    {
        public int Age { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }

        public bool Equals(StructWithRefTypeAndEquatableImplementation other)
        {
            return Age == other.Age && Height == other.Height && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StructWithRefTypeAndOverridenEquals))
            {
                return false;
            }

            var other = (StructWithRefTypeAndOverridenEquals)obj;

            return Age == other.Age && Height == other.Height && Name == other.Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        //== != operators can be ommited for this example
    }
}