namespace EmployeePortal.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public required string Name { get; set; } //обязательно к заполнению
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }

    }
}
