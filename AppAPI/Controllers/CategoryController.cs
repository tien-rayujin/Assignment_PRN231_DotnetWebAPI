﻿using BOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Services;

namespace AppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController: ControllerBase
{
    ICategoryService _context;

    public CategoryController(ICategoryService context)
    {
        _context = context;
    }

    [HttpGet]
    [EnableQuery]
    [Authorize(Roles = "Staff, Admin")]
    public ActionResult<List<Category>> GetAll()
    {
        return Ok(_context.GetAll());   
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Staff, Admin")]
    public ActionResult<Category> Get([FromRoute] string id)
    {
        return Ok(_context.Get(id));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create([FromBody] Category Category)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            _context.Create(Category);
        }
        catch (Exception)
        {
            throw;
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Update([FromRoute]string id, [FromBody] Category Category)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var entity = _context.Get(id);
            if (entity == null) return NotFound();

            _context.Update(Category);
        }
        catch (Exception)
        {
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete([FromRoute] string id)
    {
        try
        {
            var entity = _context.Get(id);
            if(entity == null) return NotFound();

            _context.Delete(entity);
        }
        catch (Exception)
        {
            throw;
        }

        return NoContent();
    }
}
