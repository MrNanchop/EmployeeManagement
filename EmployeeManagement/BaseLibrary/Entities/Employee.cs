namespace BaseLibrary.Entities
{
    public class Employee:BaseEntity
    {
        public string? CivilId { get; set; }
        public string? FileNumber { get; set; }
        public string? Fullname { get; set; }
        public string? JobName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; } 
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Email { get; set; } = null;
        public string? Gender { get; set;}
        public string? MobilePhone { get; set;}
        public string? TelephonePhoneNumber { get;set;} = null;
     

        //Relationship
        public GeneralDepartment? GeneralDepartment { get; set;}
        public Guid GeneralDepartmentId { get; set; }    
        public Department? Department {  get; set; }
        public Guid EmployeeDepartmentId { get; set;}

        public Branch? Branch { get; set; }
        public Guid BranchId { get; set; }
        public Town? Town { get; set; } 
        public Guid TownId { get; set; }

    }
}
