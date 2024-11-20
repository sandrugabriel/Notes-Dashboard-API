using Newtonsoft.Json;
using Notes_Dashboard_API.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tests.Infrastructure;
using Tests.Notes.Helper;
using Notes_Dashboard_API.Notes.Dto;
using Notes_Dashboard_API.Notes.Models;

namespace Tests.Notes.UnitTests
{
    public class TestNoteIntegration : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TestNoteIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllNotes_NotesFound_ReturnsOkStatusCode_ValidResponse()
        {

            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest { Username = "test", Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result1.Token);

            var createNoteRequest = TestNoteFactory.CreateNote(1);
            content = new StringContent(JsonConvert.SerializeObject(createNoteRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerNote/createNote", content);

            response = await _client.GetAsync("/api/v1/ControllerNote/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetNoteById_NoteFound_ReturnsOkStatusCode_ValidResponse()
        {

            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest { Username = "test", Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result1.Token);

            var createNote = new CreateNoteRequest { Title = "New Note 1", Description = "asdsdf", CreateDate = "20 aug 2024", Category = "sad"};
            content = new StringContent(JsonConvert.SerializeObject(createNote), Encoding.UTF8, "application/json");
            var res = await _client.PostAsync("/api/v1/ControllerNote/CreateNote", content);
            var resString = await res.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<NoteResponse>(resString);

            response = await _client.GetAsync($"/api/v1/ControllerNote/FindById?id={result.Id}");
            resString = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<NoteResponse>(resString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Title, createNote.Title);
        }

        [Fact]
        public async Task GetNoteById_NoteNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerNote/findById/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }

}
