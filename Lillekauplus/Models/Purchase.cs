using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lillekauplus.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите имя")]
        public string Person { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите свою почту")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Введите настоящую почту")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите свой телефонный номер")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Пожалуйста, выбери вариант ответа")]
        public bool? Card { get; set; }
        public string Address { get; set; }
        public int BookId { get; set; }
        public DateTime Date { get; set; }
        public bool? Pay { get; set; }

    }
}