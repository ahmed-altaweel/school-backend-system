namespace SchoolProject.Core.Features.Students.Queries.Results
{
    public class GetStudentPaginatedListResponse
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? DName { get; set; }


        public GetStudentPaginatedListResponse(int studentId, string? name, string? address, string? dName)
        {
            StudentId = studentId;
            Name = name;
            Address = address;
            DName = dName;
        }
    }
}
