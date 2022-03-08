using System;
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
    [Route("api/[controller]")]
    [ApiController]
    public class SensoresController : ControllerBase
    {
        private readonly CelularesContext _context;

        public SensoresController(CelularesContext context)
        {
            _context = context;
        }

        // GET: api/Sensores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensor()
        {
            return await _context.Sensor.ToListAsync();
        }

        // GET: api/Sensores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetSensor(int id)
        {
            var sensor = await _context.Sensor.FindAsync(id);

            if (sensor == null)
            {
                return NotFound();
            }

            return sensor;
        }

        // PUT: api/Sensores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensor(int id, Sensor sensor)
        {
            if (id != sensor.SensorId)
            {
                return BadRequest();
            }

            _context.Entry(sensor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorExists(id))
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

        // POST: api/Sensores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sensor>> PostSensor(Sensor sensor)
        {
            _context.Sensor.Add(sensor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSensor", new { id = sensor.SensorId }, sensor);
        }

        // DELETE: api/Sensores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var sensor = await _context.Sensor.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }

            _context.Sensor.Remove(sensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorExists(int id)
        {
            return _context.Sensor.Any(e => e.SensorId == id);
        }
    }
}
