﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Celulares.Models;
using Telefonos.Models;

namespace Celulares.Controllers
{
    [Route("api/1.0/[controller]")]
    [ApiController]
    public class OperariosController : ControllerBase
    {
        private readonly CelularesContext _context;

        public OperariosController(CelularesContext context)
        {
            _context = context;
        }

        // GET: api/Operarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operario>>> GetOperario()
        {
            return await _context.Operario.ToListAsync();
        }

        // GET: api/Operarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Operario>> GetOperario(int id)
        {
            var operario = await _context.Operario.FindAsync(id);

            if (operario == null)
            {
                return NotFound();
            }

            return operario;
        }

        // PUT: api/Operarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperario(int id, Operario operario)
        {
            if (id != operario.OperarioId)
            {
                return BadRequest();
            }

            _context.Entry(operario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Operarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Operario>> PostOperario(Operario operario)
        {
            _context.Operario.Add(operario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperario", new { id = operario.OperarioId }, operario);
        }

        // DELETE: api/Operarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperario(int id)
        {
            var operario = await _context.Operario.FindAsync(id);
            if (operario == null)
            {
                return NotFound();
            }

            _context.Operario.Remove(operario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperarioExists(int id)
        {
            return _context.Operario.Any(e => e.OperarioId == id);
        }
    }
}
