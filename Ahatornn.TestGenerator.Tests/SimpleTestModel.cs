namespace Ahatornn.TestGenerator.Tests
{
    internal class SimpleTestModel
    {
        public Guid Id { get; set; }
        public Guid DependencyEntityId { get; }
        public string Name { get; set; }
        public string Description { get; }
        public DateTime CreatedAt { get; set; }
        public DateTime CreatedAtDefault { get; set; }
        public DateTime LastUpdatedAt { get; }
        public DateTimeOffset ActualDate { get; set; }
        public DateTimeOffset DeletedAt { get; }
        public decimal Cost { get; set; }
        public decimal? Discount { get; set; }
    }
}
