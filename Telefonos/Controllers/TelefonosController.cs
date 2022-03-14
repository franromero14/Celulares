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
    [Route("api/1.0/[controller]")]
    [ApiController]
    public class TelefonosController : ControllerBase
    {
        private readonly CelularesContext _context;

        public TelefonosController(CelularesContext context)
        {
            _context = context;
        }

        // GET: api/Telefonos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefono()
        {
            return await _context.Telefono.ToListAsync();
        }

        // GET: api/Telefonos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Telefono>> GetTelefono(int id)
        {
            var telefono = await _context.Telefono.FindAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }

            return telefono;
        }

        [HttpGet("SensoresDeEquipo")]
        public dynamic SensoresEquipo(int id)
        {
            var test = _context.Telefono
                .Where(e => e.TelefonoId == id)
                .Select(item => new
                {
                    marca = item.Marca,
                    modelo = item.Modelo,
                    sensores = item.Sensores
                               .Select(e => e.Nombre).ToList()
                });
            return test;
        }

        [HttpGet("ListarSensores")]
        public dynamic Listar()
        {
            var test = _context.Telefono
                .Select(item => new 
                {
                    marca = item.Marca,
                    modelo = item.Modelo,
                    sensores = item.Sensores
                               .Select(e => e.Nombre).ToList()
                });
            return test;
        }

        [HttpGet("InstalacionesEquipo")]
        public dynamic Test3(int Id)
        {
            return _context.Instalacion
                .Where(item => item.TelefonoId == Id)
                .Select(e => new
                {
                    Aplicacion = e.App.Nombre,
                    NombreOperario = e.Operario.Nombre,
                    ApellidoOperario = e.Operario.Apellido,
                    FueExitosa = e.Exitosa
                }).ToList();
        }

        [HttpGet("InstalacionesExitosas")]
        public dynamic Test4(bool flag)
        {
            return _context.Instalacion
                .Where(item => item.Exitosa == flag)
                .Select(e => new
                {
                    IdEquipo = e.TelefonoId,
                    Marca = e.Telefono.Marca,
                    Modelo = e.Telefono.Modelo,
                    Aplicacion = e.App.Nombre,
                    NombreOperario = e.Operario.Nombre,
                    ApellidoOperario = e.Operario.Apellido,
                    Fecha = e.Fecha
                }).ToList();
        }

        [HttpGet("TelefonosConSensor")]
        public dynamic Test5(string Sensor)
        {
            return _context.Telefono
                .Where(item => item.Sensores.Any(e => e.Nombre == Sensor))
                .Select(e => new
                {
                    IdEquipo = e.TelefonoId,
                    Marca = e.Marca,
                    Modelo = e.Modelo
                }).ToList();
        }

        [HttpGet("TelefonosPorApp")]
        public dynamic Test6(int AppId)
        {
            return _context.Instalacion
                .Where(item => (item.AppId == AppId))
                .Select(e => new
                {
                    IdEquipo = e.TelefonoId,
                    Marca = e.Telefono.Marca,
                    Modelo = e.Telefono.Modelo
                }).ToList();
        }

        [HttpGet("InstalacionesPorOperario")]
        public dynamic Test7()
        {
            return _context.Instalacion
                .Where(p => p.Exitosa == true)
                .GroupBy(q => new
                {
                    q.Fecha.Date,
                    q.OperarioId
                    
                })
                .Select(e => new
                {   
                    e.Key.OperarioId,
                    fecha = e.Key.Date,
                    cantidad = e.Count()
                    
                }).ToList();
        }

        

        // PUT: api/Telefonos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelefonos(int id, Telefono telefono)
        {

            if (id != telefono.TelefonoId)
            {
                return BadRequest();
            }

            // La variable telefono tendrá la información que recibimos por PUT
            // La variable tel tendrá la info original de la vivienda con el id recibido

            var tel = await _context.Telefono.FindAsync(id);

            // Borraremos los sensores de los telefonos para reemplazarlos con los recibidos

            if (tel.Sensores != null)
            {
                tel.Sensores.Clear();
            }

            await _context.SaveChangesAsync();

            // Esto es importante porque tenemos que avisarle a EF
            // que aquí termina una transacción y comienza otra
            _context.ChangeTracker.Clear();


            // Agregamos a la info de la vivienda los nuevos propietarios
            if (telefono.SensoresList != null)
            {
                foreach (var telId in telefono.SensoresList)
                {
                    var sensor = await _context.Sensor.FindAsync(telId);
                    if (sensor != null)
                    {
                        telefono.Sensores.Add(sensor);
                    }
                }
            }

            // Avisamos que hemos modificado la vivienda para que EF tome los cambios al guardar
            _context.Entry(telefono).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            // Si llegamos aquí es porque todo salió bien
            // devolvemos OK (http 200) y los datos de la vivienda
            return Ok(telefono);
        }


        // POST: api/Telefonos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Telefono>> PostTelefono(Telefono telefono)
        {
            // A cada uno de los sensores recibidos lo agregamos al telefono
            foreach (var item in telefono.SensoresList)
            {
                Sensor s = await _context.Sensor.FindAsync(item);
                telefono.Sensores.Add(s);
            }

            // Agregamos el telefono con toda su info a la base de datos
            _context.Telefono.Add(telefono);
            await _context.SaveChangesAsync();

            // Devolvemos CREATED con el telefono generado
            return CreatedAtAction("GetTelefono", new { id = telefono.TelefonoId }, telefono);
        }

        // DELETE: api/Telefonos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelefono(int id)
        {
            var telefono = await _context.Telefono.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            _context.Telefono.Remove(telefono);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TelefonoExists(int id)
        {
            return _context.Telefono.Any(e => e.TelefonoId == id);
        }
    }
}
