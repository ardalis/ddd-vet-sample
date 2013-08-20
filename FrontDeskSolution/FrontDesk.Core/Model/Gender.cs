using System;
using System.Linq;

namespace FrontDesk.Core.Model
{
    public class Gender
    {
        public static readonly MaleGender Male = new MaleGender();
        public static readonly FemaleGender Female = new FemaleGender();
    }

    public class FemaleGender : Gender
    {
        public override string ToString()
        {
            return "Female";
        }
    }
    public class MaleGender : FemaleGender
    {
        public override string ToString()
        {
            return "Male";
        }
    }

}