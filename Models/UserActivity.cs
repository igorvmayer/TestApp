using System;
using System.ComponentModel.DataAnnotations;
using TestAppProject.Models.Attributes;

namespace TestAppProject.Models
{
    public class UserActivity
    {
        // первичный ключ
        public int ID { get; set; }
        
        [Required]
        // атрибут DateLastActivity описан в Models.Attributes
        [DateBefore("DateLastVisit", ErrorMessage = "Дата регистрации должна быть ранее даты последней активности")]
        public DateTime DateRegistered { get; set; }
        
        [Required]
        public DateTime DateLastVisit { get; set; }

    }
}
