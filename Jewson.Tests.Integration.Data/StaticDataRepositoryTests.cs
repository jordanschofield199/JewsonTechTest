using Jewson.Data.Repositories;
using NUnit.Framework;

namespace Jewson.Tests.Integration.Data
{
    [TestFixture]
    public class StaticDataRepositoryTests
    {
        private StaticDataRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new StaticDataRepository();
        }

        [Test]
        public void LoadDataFromFile()
        {
            //  Arrange

            //  Act
            var result = _repository.LoadDataFromFile();

            //  Assert
            Assert.IsNotNull(result);
        }
    }
}
