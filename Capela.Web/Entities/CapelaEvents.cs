using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Capela.Web.Entities
{
        public class CapelaEvents
        {
                [Key, Required()]
                public Guid Id { get; set; }

                [AllowHtml]
                [Required(ErrorMessage = "Conteúdo Obrigatório")]
                [StringLength(int.MaxValue)]
                [Display(Name = "Conteúdo")]
                public string Text { get; set; }

                [Display(Name = "Foto")]
                public byte[] Photo { get; set; }
        }
}