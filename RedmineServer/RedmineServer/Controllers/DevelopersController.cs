﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedmineServer.Models;

[ApiController]
[Route("api/[controller]")]
public class DevelopersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DevelopersController(ApplicationDbContext context)
    {
        _context = context;
    }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeveloperDTO>>> GetDevelopers()
    {
        try
        {
            var developers = await _context.Developers
                .Select(m => new DeveloperDTO { Email = m.Email, Name = m.Name})
                .ToListAsync();

            return Ok(developers);
        }
        catch (Exception)
        {
            // Return an internal server error if database operation fails.
            return StatusCode(500, "Error accessing database");
        }
    }
}