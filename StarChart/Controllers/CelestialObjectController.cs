using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;


namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}", Name ="GetById")]
        public IActionResult GetById(int id)
        {
            var celestialobject = _context.CelestialObjects.Find(id);
            if (celestialobject == null)
                return NotFound();
            celestialobject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == id).ToList();
            return Ok(celestialobject.Id == id);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialobject = _context.CelestialObjects.Find(name);
            if (celestialobject == null)
                return NotFound();
            celestialobject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celestialobject.Id).ToList();
            return Ok(celestialobject.Name == name);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialobject = _context.CelestialObjects.Find();
            celestialobject.Satellites = _context.CelestialObjects.ToList();
            return Ok();
        }
    }
    
}

