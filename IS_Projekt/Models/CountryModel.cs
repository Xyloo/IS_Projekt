﻿using System.ComponentModel.DataAnnotations;

namespace IS_Projekt.Models
{
    public class CountryModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string CountryCode { get; set; }
    }
}