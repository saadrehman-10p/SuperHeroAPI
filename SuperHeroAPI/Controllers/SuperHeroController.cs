using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Model;
using SuperHeroAPI.Services;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private readonly ISuperHeroService _superHeroService;

        public SuperHeroController(ISuperHeroService superHeroService)
        {
            _superHeroService = superHeroService;
        }

        [HttpGet(Name = "GetSuperHeroes")]

        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(_superHeroService.GetSuperHeroes().ToList());
        }

        [HttpGet("{id}"),]

        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = _superHeroService.GetSuperHeroById(id);
            if (hero == null)
            {
                return BadRequest("hero not found");
            }
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> PostHero(SuperHeroDTO hero)
        {

           
             _superHeroService.PostSuperHero(hero);

            return Ok(_superHeroService.GetSuperHeroes().ToList());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> updateHero(SuperHero Hero)
        {
            _superHeroService.PutSuperHero(Hero);
            

            return Ok(_superHeroService.GetSuperHeroes().ToList());
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            _superHeroService.DeleteSuperHero(id);
            return Ok(_superHeroService.GetSuperHeroes().ToList());
        }

    }
}
