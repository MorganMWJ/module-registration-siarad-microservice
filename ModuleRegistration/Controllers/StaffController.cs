using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuleRegistration.Data;
using ModuleRegistration.Models;

namespace ModuleRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IDataRepository _repo;

        public StaffController(IDataRepository repo)
        {
            _repo = repo;
        }

        /**
         * Returns all Staff entities.
         */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAllStaffMembers()
        {
            return Ok(await _repo.StaffListAsync());
        }

        /**
         *  Return a list of all staff uids.
         */
        [HttpGet("uids")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllStaffUids()
        {
            List<String> uids = new List<String>();
            var staff = await _repo.StaffListAsync();
            foreach (Staff s in staff)
            {
                uids.Add(s.Uid);
            }
            return Ok(uids);
        }


        //api/staff/{uid}
        [HttpDelete("{uid}")]
        public async Task<ActionResult> DeleteStaff(string uid)
        {
            if (!_repo.StaffExists(uid))
            {
                return NotFound();
            }
            await _repo.DeleteStaffAsync(uid);
            return Ok();
        }
        //api/staff
        [HttpPost]
        public async Task<ActionResult> PostStaff([FromBody]Staff staff)
        {
            if (_repo.StaffExists(staff.Uid))
            {
                return BadRequest();
            }
            await _repo.AddStaffAsync(staff);
            return Ok();
        }
        //api/staff
        [HttpDelete]
        public async Task<ActionResult> DeleteStaff()
        {
            await _repo.DeleteAllStaffAsync();
            return Ok();
        }

        //api/staff/{uid}/{mid}
        [HttpGet("{uid}/{mid}")]
        public async Task<ActionResult> GetStaffModuleLink(string uid, int mid)
        {
            await _repo.GetModuleStudentAsync(mid, uid);
            return Ok();
        }

        //api/staff/{uid}/{mid]
        [HttpPost("{uid}/{mid}")]
        public async Task<ActionResult> AddStaffModuleLink(string uid, int mid)
        {
            var staff = await _repo.GetStaffAsync(uid);
            var module = await _repo.GetModuleAsync(mid);
            if (staff == null || module == null)
            {
                return NotFound();
            }
            var moduleStudent = new ModuleStaff
            {
                Module = module,
                Staff = staff
            };
            await _repo.AddModuleStaffAsync(moduleStudent);
            return Ok();
        }

        //api/staff/{uid}/{mid}
        [HttpDelete("{uid}/{mid}")]
        public async Task<ActionResult> DeleteStaffModuleLink(string uid, int mid)
        {
            var staffModuleLink = await _repo.GetModuleStaffAsync(mid, uid);
            if (staffModuleLink == null)
            {
                return NotFound();
            }
            await _repo.DeleteModuleStaffAsync(staffModuleLink.Id);
            return Ok();
        }
    }
}