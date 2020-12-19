using System.Linq;
using NotesAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NotesAPI.Controllers
{
    [Route("api/NoteItems")]
    [ApiController]
    public class NoteItemsController : ControllerBase
    {
        private readonly NoteContext _context;

        public NoteItemsController(NoteContext context)
        {
            _context = context;
        }

        // GET: api/NoteItems1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteItem>>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        // GET: api/NoteItems1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteItem>> GetNoteItem(long id)
        {
            var noteItem = await _context.Notes.FindAsync(id);

            if (noteItem == null)
            {
                return NotFound();
            }

            return noteItem;
        }

        // PUT: api/NoteItems1/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoteItem(long id, NoteItem noteItem)
        {
            if (id != noteItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(noteItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteItemExists(id))
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

        // POST: api/NoteItems1
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NoteItem>> PostNoteItem(NoteItem noteItem)
        {
            _context.Notes.Add(noteItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoteItem), new { id = noteItem.Id }, noteItem);
        }

        // DELETE: api/NoteItems1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NoteItem>> DeleteNoteItem(long id)
        {
            var noteItem = await _context.Notes.FindAsync(id);
            if (noteItem == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(noteItem);
            await _context.SaveChangesAsync();

            return noteItem;
        }

        private bool NoteItemExists(long id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
