namespace BasicsApi.Dto
{
    public class EmpRoleDto
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int RoleId { get; set; }
        public virtual RoleDto Role { get; set; }
    }
}
