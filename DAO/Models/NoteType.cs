using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAO.Models
{
    public class NoteType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Note> Notes { get; set; }

    }
}
