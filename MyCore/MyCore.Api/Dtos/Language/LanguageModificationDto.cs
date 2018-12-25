using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCore.Api.Dtos.Language
{
    public class LanguageModificationDto
    {
        [Required(ErrorMessage ="value不得为空")]
        public string Value { get; set; }
    }
}
