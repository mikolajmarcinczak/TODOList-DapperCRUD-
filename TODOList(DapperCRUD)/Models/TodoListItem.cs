using System;
using System.ComponentModel.DataAnnotations;

namespace TODOList_DapperCRUD_.Models
{
    public class TodoListItem
    {
        public int Id { get; set; }
        public DateTime AddDate { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string Title { get; set; }
        public bool isDone { get; set; }
    }
}
