using ACC.Common.Repository;
using ACC.Services.Tracking.Domain;
using ACC.Services.Tracking.Repositories;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.UnitTests.Repositories
{
    public class TrackedVehicleRepositoryTests : TestBase
    {
        private IRepository<TrackedVehicle, string> _repository;
        private ITrackedVehicleRepository _trackedVehicleRepository;

        [OneTimeSetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository<TrackedVehicle, string>>();
            _trackedVehicleRepository = new TrackedVehicleRepository(_repository);
        }

        [Test]
        public async Task GetAsync_Returns_Item_With_Specified_Id()
        {
            _repository
                .GetAsync(Arg.Any<string>())
                .Returns(e => TestData.FirstOrDefault(x => x.Id == e[0].ToString()));

            var expected = TestData[3];

            var actual = await _trackedVehicleRepository
                         .GetAsync(expected.Id);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetAsync_Returns_Filtered_Items()
        {
            _repository
                .GetAsync(Arg.Any<Expression<Func<TrackedVehicle, bool>>>())
                .Returns(e => TestData
                           .Where(((Expression<Func<TrackedVehicle, bool>>)e[0])
                           .Compile()));

            var expected = TestData.Where(e => e.CustomerId == "KallesGrustransporter");

            var actual = await _trackedVehicleRepository
                         .GetAsync(e => e.CustomerId == "KallesGrustransporter");

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public async Task ExistsAsync_Checks_For_Item_Existence()
        {
            _repository
                .ExistsAsync(Arg.Any<Expression<Func<TrackedVehicle, bool>>>())
                .Returns(e => TestData
                              .FirstOrDefault(((Expression<Func<TrackedVehicle, bool>>)e[0])
                              .Compile()) != null);

            var item = TestData[3];

            var exists = await _trackedVehicleRepository
                         .ExistsAsync(item.Id);

            Assert.IsTrue(exists);

            exists = await _trackedVehicleRepository
                      .ExistsAsync(Guid.NewGuid().ToString());

            Assert.IsFalse(exists);
        }

        [Test]
        public async Task DeleteAsync_WhenCalled_Deletes_Specified_Item()
        {
            var item = TestData[3];

            await _trackedVehicleRepository
                          .DeleteAsync(item.Id);

            await _repository
                 .Received()
                 .DeleteAsync(Arg.Is<string>(id => id == item.Id));
        }

        [Test]
        public async Task AddAsync_WhenCalled_Adds_Specified_Item()
        {
            var item = TestData[3];

            await _trackedVehicleRepository
                          .AddAsync(item);

            await _repository
                 .Received()
                 .AddAsync(Arg.Is<TrackedVehicle>(e => e == item));
        }
    }
}