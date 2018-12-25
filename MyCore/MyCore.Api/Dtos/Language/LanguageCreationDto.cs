using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCore.Api.Dtos.Language
{
    public class LanguageCreationDto
    {
        [Required(ErrorMessage ="key不得为空")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Value不得为空")]
        public string Value { get; set; }
    }
}
