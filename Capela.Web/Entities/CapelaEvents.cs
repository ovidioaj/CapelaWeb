using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Capela.Web.Entities
{
        public class CapelaEvents
        {
                [Key, Required()]
                public Guid Id { get; set; }

                [StringLength(150), Required(ErrorMessage = "Título Obrigatório"), Display(Name = "Descrição")]
                public string Title { get; set; }

                [Display(Name = "Nome")]
                [StringLength(int.MaxValue), Required(ErrorMessage = "Nome Obrigatório")]
                public string Name { get; set; }

                [AllowHtml]
                [Required(ErrorMessage = "Conteúdo Obrigatório")]
                [StringLength(int.MaxValue)]
                [Display(Name = "Conteúdo")]
                public string Text { get; set; }

                [Display(Name = "Foto")]
                public byte[] Photo { get; set; }
        }
}