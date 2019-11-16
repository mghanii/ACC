using System.ComponentModel.DataAnnotations;

namespace ACC.Services.Customers.Dto
{
    public class AddCustomerDto
    {
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [Required]
        public string PostCode { get; set; }

        [Required]
        public string City { get; set; }

        public string State { get; set; }

        [Required]
        public string Country { get; set; }
    }
}