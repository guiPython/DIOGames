using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DIOGames.Services;
using DIOGames.ViewModel;
using DIOGames.InputModel;
using DIOGames.Exceptions;

namespace DIOGames.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        
        [HttpGet]
        public async Task<ActionResult<List<ViewModelGame>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1,50)] int quantity = 5)
        {
            var result = await _gameService.Get(page, quantity);

            if (result.Count() == 0) return NoContent();

            return Ok(result);
        }

        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<ViewModelGame>> Get([FromRoute] Guid gameId)
        {
            var result = await _gameService.Get(id: gameId);

            if (result == null) return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ViewModelGame>> Post([FromBody] InputModelGame game)
        {
            try
            {
                var result = await _gameService.Insert(game);
                return Ok(result);
            }
            catch(GameAlreadyExistsException)
            {
                return UnprocessableEntity("This game already exists");
            }
            
        }

        [HttpPut("{gameId:guid}")]
        public async Task<ActionResult> Put([FromRoute] Guid gameId, [FromBody] InputModelGame game)
        {
            try
            {
                await _gameService.Update(gameId, game);
                return Ok();
            }
            catch (GameNotExistsException)
            {
                return NotFound("This game not exists");
            }
        }

        [HttpPatch("{gameId:guid}/value/{price:decimal}")]
        public async Task<ActionResult> Patch([FromRoute] Guid gameId,[FromRoute] decimal price)
        {
            try
            {
                await _gameService.Update(gameId, price);
                return Ok();
            }
            catch (GameNotExistsException)
            {
                return NotFound("This game not exists");
            }
        }

        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> Delete(Guid gameId)
        {
            try
            {
                await _gameService.Delete(gameId);
                return Ok();
            }
            catch (GameNotExistsException)
            {
                return NotFound("This game not exists");
            }
        }
    }
}
