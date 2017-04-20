using System;
using System.ComponentModel.DataAnnotations;

namespace Jewson.FrontEnd.Models.ViewModels
{
    public class HomeViewModel
    {
        [Required]
        public int Number { get; set; }
    }
}