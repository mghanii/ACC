using ACC.Services.Tracking.Domain;

namespace ACC.Services.Tracking.UnitTests
{
    public class TestBase
    {
        private TrackedVehicle[] _data = new[] {
                     new TrackedVehicle ("YS2R4X20005399401","94.224.250.144", "ABC123","kallesgrustransporter","Kalles Grustransporter AB","Cementvägen 8, 111 11 Södertälje"),
                     new TrackedVehicle ("VLUR4X20009093588","11.106.191.224", "DEF456","kallesgrustransporter","Kalles Grustransporter AB","Cementvägen 8, 111 11 Södertälje"),
                     new TrackedVehicle ("VLUR4X20009048066","190.166.194.12", "GHI789","kallesgrustransporter","Kalles Grustransporter AB","Cementvägen 8, 111 11 Södertälje"),
                     new TrackedVehicle ("YS2R4X20005388011","48.226.144.103", "JKL012","johansbulk","Johans Bulk AB","Balkvägen 12, 222 22 Stockholm"),
                     new TrackedVehicle ("YS2R4X20005387949","145.1.95.177", "MNO345","johansbulk","Johans Bulk AB","Balkvägen 12, 222 22 Stockholm"),
                     new TrackedVehicle ("VLUR4X20009048066","12.25.82.23", "PQR678","haraldsvardetransporter","Haralds Värdetransporter AB","Budgetvägen 1, 333 33 Uppsala"),
                     new TrackedVehicle ("YS2R4X20005387055","208.92.39.218", "STU901","haraldsvardetransporter","Haralds Värdetransporter AB","Budgetvägen 1, 333 33 Uppsala"),
                   };

        protected TrackedVehicle[] TestData => _data;
    }
}