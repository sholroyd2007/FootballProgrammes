namespace FootballProgrammes.Models
{
    public class Address : Entity
    {
        public string Country { get; set; }

        /// <summary>
        /// First Name used to address
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name used to contact the person at the address
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// House/Street
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// District/Neighbourhood
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// State or Province of the Address
        /// </summary>
        public string StateProvince { get; set; }

        /// <summary>
        /// The City the Address is in
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The Postal Code of the Address
        /// </summary>
        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }

    }
}
