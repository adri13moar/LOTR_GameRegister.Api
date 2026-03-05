using Dapper;
using LOTR_GameRegister.Api.Models;
using LOTR_GameRegister.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LOTR_GameRegister.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DifficultiesController(DifficultyRepository difficultyRepository) : ControllerBase
    {
        private readonly DifficultyRepository _difficultyRepository = difficultyRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var difficulties = await _difficultyRepository.GetAllAsync();
                return Ok(difficulties);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var quest = await _difficultyRepository.GetById(id);

                if (quest == null)
                {
                    return NotFound($"Quest with ID {id} not found.");
                }

                return Ok(quest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
