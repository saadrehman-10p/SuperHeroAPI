using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Model;
namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        
        private  DataContext _context { get; }

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet(Name ="GetSuperHeroes"),Authorize(Roles ="Admin")]

        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet ("{id}"),Authorize(Roles = "Admin")]

        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero=await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not Found");
            }
            return Ok(hero);
        }
        [HttpPost,Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<SuperHero>>> PostHero(SuperHero hero)
        {
            
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPut ,Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<SuperHero>>> updateHero(SuperHero request)
        {
            var hero =await _context.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
            {
                return BadRequest("Hero not Found");
            }
            hero.Name=request.Name;
            hero.FirstName=request.FirstName;
            hero.LastName=request.LastName;
            hero.Place = request.Place;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}"),Authorize(Roles = "Admin")]

        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not Found");
            }
            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

    }
}
