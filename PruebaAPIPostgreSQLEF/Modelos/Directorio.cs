﻿using System.ComponentModel.DataAnnotations;

namespace PruebaAPIPostgreSQLEF.Modelos
{
    public class Directorio
       {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string TextComments { get; set; }
    }
}