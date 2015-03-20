using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Core
{
    public class Staff : Person
    {
        
        public int Id { get; set; }

        public string Username { get; set; }
                                
        public virtual ICollection<StaffOnProjects> StaffOnProjects { get; set; }
        
        public virtual ICollection<StaffOnTask> StaffOnTasks { get; set; }

        public virtual ICollection<Notes.Note> Notes { get; set; }
                
    }

    public class StaffService : IDisposable
    {

        private anthRContext _db;

        public StaffService()
        {
            _db = new anthRContext();
        }
        
        public async Task<Staff> GetStaffMemberAsync(string username)
        {            
            return await LoadStaffMemberAsync(username);
        }

        private Task<Staff> LoadStaffMemberAsync(string username)
        {
            var staffMember = _db.Staff.SingleOrDefault(s => s.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(staffMember);
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }
    }

}
