﻿using System;
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
        private readonly ModuleRegistrationContext _context;

        public StaffController(ModuleRegistrationContext context)
        {
            _context = context;
        }

        /**
         * Returns all Staff entities.
         */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAllStaffMembers()
        {
            return Ok(await _context.Staff.ToListAsync());
        }

        /**
         *  Return a list of all staff uids.
         */
        [HttpGet("uids")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllStaffUids()
        {
            List<String> uids = new List<String>();
            var staff = await _context.Staff.ToListAsync();
            foreach (Staff s in staff)
            {
                uids.Add(s.Uid);
            }
            return Ok(uids);
        }
    }
}