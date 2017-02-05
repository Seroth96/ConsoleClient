using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class Note
    {
        public string title { get; set; }
        public string body { get; set; }
        public string tag_id { get; set; }
        public int id { get; set; }
        public string category_id { get; set; }

        public Note() { }

        public Note(string title, string body, string tag,  int id, string category_id)
        {
            this.title = title;
            this.body = body;
            this.tag_id = tag;
            this.id = id;
            this.category_id = category_id;

        }

    }

    public class Notes
    {
        public List<Note> notes { get; set; }
    }
}
