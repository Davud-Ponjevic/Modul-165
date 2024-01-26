namespace Praxisarbeit.Dto
{
    public class RegistrationDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PriorityId { get; set; }
        public string ServiceId { get; set; }

        public string? UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime PickupDate { get; set; }
    }
}
