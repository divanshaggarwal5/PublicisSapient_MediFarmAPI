using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediOps.DataModels;
using MediOps.DataTransferModels;
using Microsoft.AspNetCore.Cors;

namespace MediOps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly MedicineContext _context;

        public MedicinesController(MedicineContext context)
        {
            _context = context;
        }

        // GET: api/Medicines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedDbSet()
        {
            return await _context.MedDbSet.ToListAsync();
        }

        // GET: api/Medicines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetMedicine([FromQuery]int id)
        {
            var medicine = await _context.MedDbSet.FindAsync(id);

            if (medicine == null)
            {
                return NotFound();
            }

            return medicine;
        }

        // DTO is made to limit updates to specific fields
        // PUT: api/Medicines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine([FromQuery]MedicineDTO medicineDTO)
        {
            var medicine = await _context.MedDbSet.FindAsync(medicineDTO.Id);
            if (medicine==null)
            {
                return NoContent();
            }
            
            medicine.Notes = medicineDTO.Notes;
            _context.Entry(medicine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(medicineDTO.Id))
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

        // POST: api/Medicines
        [HttpPost]
        public async Task<ActionResult<Medicine>> PostMedicine([FromBody]Medicine medicine)
        {
            medicine.Id = _context.MedDbSet.Any() ? _context.MedDbSet.Max(med => med.Id) + 1 : 1;
            //todo : Here expiry date validation
            _context.MedDbSet.Add(medicine);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicine", new { id = medicine.Id }, medicine);
        }

        // DELETE: api/Medicines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Medicine>> DeleteMedicine(int id)
        {
            var medicine = await _context.MedDbSet.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }

            _context.MedDbSet.Remove(medicine);
            await _context.SaveChangesAsync();

            return medicine;
        }

        private bool MedicineExists(int id)
        {
            return _context.MedDbSet.Any(e => e.Id == id);
        }
    }
}
