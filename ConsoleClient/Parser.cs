using System.Collections.Generic;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ConsoleClient
{
    class Parser
    {
        public static List<Note> ParseNotes(string content)
        {           
            Notes notes = new JavaScriptSerializer().Deserialize<Notes>(content);            
            return notes.notes;
        }

        public static Note ParseNote(string content)
        {
            Note note = new JavaScriptSerializer().Deserialize<Note>(content);
            return note;
        }

        public static Message ParseMessage(string content)
        {
            Message message = new JavaScriptSerializer().Deserialize<Message>(content);
            return message;
        }

        public static string SerializeNote(Note note)
        {
            string m = new JavaScriptSerializer().Serialize(note);
            return m;
        }

        public static Token ParseToken(string content)
        {
            Token tok = new JavaScriptSerializer().Deserialize<Token>(content);
            return tok;
        }

    }
}
