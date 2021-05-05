using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using diogames.Exceptions;
using diogames.InputModel;
using diogames.Services;
using diogames.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace diogames.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var result = await _gameService.Get(page, quantity);
            if (result.Count() == 0)
                return NoContent();
            return Ok(result);
        }

        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<GameViewModel>> Get([FromRoute] Guid gameId)
        {
            var result = await _gameService.Get(gameId);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> Post([FromBody] GameInputModel game)
        {
            try
            {
                var result = await _gameService.Post(game);
                return Ok(result);
            }
            catch (GameAlreadyExistsException ex)
            {
                return UnprocessableEntity(ex);
            }
        }

        [HttpPut("{gameId:guid}")]
        public async Task<ActionResult> Put([FromRoute] Guid gameId, [FromBody] GameInputModel game)
        {
            try
            {
                await _gameService.Put(gameId, game);
                return Ok();
            }
            catch (GameDoesNotExistsException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPatch("{gameId:guid}/price/{price:double}")]
        public async Task<ActionResult> Patch([FromRoute] Guid gameId, [FromRoute] double price)
        {
            try
            {
                await _gameService.Patch(gameId, price);
                return Ok();
            }
            catch (GameDoesNotExistsException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid gameId)
        {
            try
            {
                await _gameService.Delete(gameId);
                return Ok();
            }
            catch (GameDoesNotExistsException ex)
            {
                return NotFound(ex);
            }
        }
    }
}