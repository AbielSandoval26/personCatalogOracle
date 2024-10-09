using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using personCatalogAPI.DTO;
using personCatalogAPI.Model;
using personCatalogAPI.Service;

namespace personCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personService.GetAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonByID(int id)
        {
            var person = await _personService.GetPersonByID(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson(PersonCreateDTO person)
        {
            await _personService.AddPerson(person);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(Person person)
        {
            await _personService.UpdatePerson(person);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _personService.GetPersonByID(id);

            if (person == null)
                return NotFound();

            await _personService.DeletePerson(id);
            return NoContent();
        }
       


    }
}
