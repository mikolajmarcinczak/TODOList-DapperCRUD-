using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOList_DapperCRUD_.Helpers;
using Dapper;

namespace TODOList_DapperCRUD_.Models
{
    public class TodoListViewModel
    {
        public TodoListViewModel()
        {
            using (var db = DBHelper.GetConnection())
            {
                this.EditableItem = new TodoListItem();
                this.TodoItems = db.Query<TodoListItem>("SELECT * FROM TodoListItems ORDER BY AddDate DESC").ToList(); //Dapper query method
            }
        }

        public List<TodoListItem> TodoItems { get; set; }
        public TodoListItem EditableItem { get; set; }
    }
}
