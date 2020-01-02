using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModuleRegistration.Data;

namespace ModuleRegistration.Controllers
{
    [Route("")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly ModuleRegistrationContext _context;

        public StaffController(ModuleRegistrationContext context)
        {
            _context = context;
        }

        [HttpGet("staff")]
        public ActionResult<string> GetAllStaffMembers()
        {
            //var staffId = (from a in _context.Staff select a.Uid.ToUpperInvariant()).Concat(from b in _context.modules select b.coordinator_id.ToUpperInvariant()).Distinct();
            //return Ok(staffId);

            return Ok("Unimplemented endpoint");
        }
    }
}