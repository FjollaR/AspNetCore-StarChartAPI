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
            return Ok(celestialobject);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialobject = _context.CelestialObjects.Where(e => e.Name == name).ToList();
            if (!celestialobject.Any())
                return NotFound();
            foreach (var celestialObject in celestialobject)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celestialObject.Id).ToList();
            }
            
            return Ok(celestialobject);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects.ToList();
            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(e=> e.OrbitedObjectId == celestialObject.Id).ToList();
            }
            
            return Ok(celestialObjects);
        }
    }
    
}

