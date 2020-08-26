using System;
namespace Koai.WebApi.Models
{
    public class Book
    {
        public Book()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Publisher { get; set; }
    }
}
