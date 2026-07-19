using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend_petshop.DTOs;
using backend_petshop.Interfaces;

namespace backend_petshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AnimaisController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimaisController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AnimalDto>>> GetAll()
        {
            var animais = await _animalService.GetAll();
            return Ok(animais);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalDto>> GetById(int id)
        {
            var animal = await _animalService.GetById(id);
            if (animal == null)
                return NotFound(new ErrorResponse { Mensagem = "Animal não encontrado." });

            return Ok(animal);
        }

        [HttpPost]
        public async Task<ActionResult<AnimalDto>> Create([FromBody] CreateAnimalDto dto)
        {
            var animal = await _animalService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AnimalDto>> Update(int id, [FromBody] UpdateAnimalDto dto)
        {
            var animal = await _animalService.Update(id, dto);
            return Ok(animal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _animalService.Delete(id);
            return NoContent();
        }
    }
}
