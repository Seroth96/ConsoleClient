using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public static class Connection
    {
        private static string adress = "http://127.0.0.1:8080/"; //Adres API

        /// <summary>
        /// Pobiera wszystkie notatki tego użytkownika
        /// </summary>
        /// <returns></returns>
        public async static Task<List<Note>> GetNotes()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("JWT", Program.token.ToString());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, adress + "api/Notes");
            HttpResponseMessage response = await client.SendAsync(request);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            if (result != null)
            {
                Program.notes.notes = Parser.ParseNotes(result);
                return Program.notes.notes;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Pobiera notatkę o danym id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<Note> GetNoteById(string id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("JWT", Program.token.ToString());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, adress + "api/Notes/"+id);
            HttpResponseMessage response = await client.SendAsync(request);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            if (result != null)
            {
                Program.note = Parser.ParseNote(result);
                return Program.note;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Usuwa notatkę o danym id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<Message> DeleteNoteById(string id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("JWT", Program.token.ToString());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, adress + "api/Notes/" + id);
            HttpResponseMessage response = await client.SendAsync(request);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            if (result != null)
            {
                Program.message = Parser.ParseMessage(result);
                return Program.message;            
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Edytuje notatkę o danym id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public async static Task<Message> EditNoteById(string id, Note note)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("JWT", Program.token.ToString());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, adress + "api/Notes/" + id);

            request.Content = new StringContent(Parser.SerializeNote(note),
                                                Encoding.UTF8,
                                                "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            if (result != null)
            {
                Program.message = Parser.ParseMessage(result);
                return Program.message;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Dodaje nową notatkę
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public async static Task<Message> AddNote( Note note)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("JWT", Program.token.ToString());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, adress + "api/Notes");

            request.Content = new StringContent(Parser.SerializeNote(note),
                                                Encoding.UTF8,
                                                "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            if (result != null)
            {
                Program.message = Parser.ParseMessage(result);
                return Program.message;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Pobiera liste notatek po etykiecie
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async static Task<List<Note>> GetNotesByTag(string tag)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("JWT", Program.token.ToString());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, adress + "api/Tag/"+tag+"/Notes");
            HttpResponseMessage response = await client.SendAsync(request);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            if (result != null)
            {
                Program.notes.notes = Parser.ParseNotes(result);
                return Program.notes.notes;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Pobiera liste notatek po kategorii
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async static Task<List<Note>> GetNotesByCategory(string category)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("JWT", Program.token.ToString());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, adress + "api/Category/"+category+"/Notes");
            HttpResponseMessage response = await client.SendAsync(request);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            if (result != null)
            {
                Program.notes.notes = Parser.ParseNotes(result);
                return Program.notes.notes;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Rejestruje użytkownika
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async static Task register(string username, string password)
        {

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, adress + "api/register");

            request.Content = new StringContent("{\"username\": \"" + username + "\"," +
                                                "\"password\": \""+password+"\"}", Encoding.UTF8,
                                                "application/json");

            await client.SendAsync(request);
        }
        /// <summary>
        /// Loguje użytkownika
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async static Task login(string username, string password)
        {

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, adress + "api/login");

            request.Content = new StringContent("{\"username\": \"" + username + "\"," +
                                                "\"password\": \"" + password + "\"}", Encoding.UTF8,
                                                "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            HttpContent content = response.Content;
            string result = await content.ReadAsStringAsync();
            if (result != null)
            {
               Program.token = Parser.ParseToken(result);
            }
            else
            {
               Program.token = null;
            }



        }
    }
}
