using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend_petshop.DTOs;
using backend_petshop.Interfaces;

namespace backend_petshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TutoresController : ControllerBase
    {
        private readonly ITutorService _tutorService;

        public TutoresController(ITutorService tutorService)
        {
            _tutorService = tutorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TutorDto>>> GetAll()
        {
            var tutores = await _tutorService.GetAll();
            return Ok(tutores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TutorDto>> GetById(int id)
        {
            var tutor = await _tutorService.GetById(id);
            if (tutor == null)
                return NotFound(new ErrorResponse { Mensagem = "Tutor não encontrado." });

            return Ok(tutor);
        }

        [HttpPost]
        public async Task<ActionResult<TutorDto>> Create([FromBody] CreateTutorDto dto)
        {
            var tutor = await _tutorService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = tutor.Id }, tutor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TutorDto>> Update(int id, [FromBody] UpdateTutorDto dto)
        {
            var tutor = await _tutorService.Update(id, dto);
            return Ok(tutor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _tutorService.Delete(id);
            return NoContent();
        }
    }
}
