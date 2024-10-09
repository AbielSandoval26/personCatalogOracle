using PersonCatalog.Model;
using System.Data;
using System.Net.Http.Json;

namespace PersonCatalog.Services
{
    public class PersonService
    {
        public readonly HttpClient _httpClient;

        public PersonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Person>> GetPersonsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Person>>("Person");
        }

        public async Task<Person> GetPerson(int id)
        {
            return await _httpClient.GetFromJsonAsync<Person>($"Person/{id}");
        }

        public async Task AddPerson(Person person)
        {
            await _httpClient.PostAsJsonAsync("Person", person);
        }

        public async Task UpdatePerson(Person person)
        {
            await _httpClient.PutAsJsonAsync($"Person/{person.Id}", person);
        }

        public async Task DeletePerson (int id)
        {
            await _httpClient.DeleteAsync($"Person/{id}");
        }
    }
}
