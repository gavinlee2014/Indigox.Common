using System;
using System.Collections.Generic;
using Indigox.Common.DomainModels.Interface.Specifications;
using Indigox.Common.DomainModels.Specifications;
using Indigox.Common.DomainModels.Test.Models;
using NUnit.Framework;

namespace Indigox.Common.DomainModels.Test.Specifications
{
    [TestFixture]
    public class SpecificationTest
    {
        [Test]
        public void TestHierarchy()
        {
            ISpecification spec = Specification.Equal( "HasFang", true );
            Husky husky = new Husky();
            husky.HasFang = true;
            husky.FurColor = "White";

            Assert.True( spec.IsStatisfiedBy( husky ) );
        }
    }
}
