using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TODOList_DapperCRUD_.Helpers;
using TODOList_DapperCRUD_.Models;

namespace TODOList_DapperCRUD_.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            return View("Index", viewModel);
        }

        public IActionResult Edit(int id)
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            viewModel.EditableItem = viewModel.TodoItems.FirstOrDefault(obj => obj.Id == id);
            return View("Index", viewModel);
        }

        public IActionResult Delete(int id)
        {
            using(var db = DBHelper.GetConnection())
            {
                TodoListItem item = db.Get<TodoListItem>(id); //Dapper method for finding the requested item in the database
                if (item != null)
                    db.Delete(item); //Dapper method for deleting the item from the db
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateUpdate(TodoListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = DBHelper.GetConnection())
                {
                    if (viewModel.EditableItem.Id <= 0)
                    {
                        viewModel.EditableItem.AddDate = DateTime.Now;
                        db.Insert<TodoListItem>(viewModel.EditableItem); //Dapper method for inserting an item to the db
                    }
                    else
                    {
                        TodoListItem dbItem = db.Get<TodoListItem>(viewModel.EditableItem.Id);
                        var result = TryUpdateModelAsync(dbItem, "EditableItem");
                        db.Update(dbItem); //Dapper method for updating the db
                    }
                    return RedirectToAction("Index");
                }
            }
            else
                return View("Index", new TodoListViewModel());
            
        }

        public IActionResult ToggleIsDone(int id)
        {
            using(var db = DBHelper.GetConnection())
            {
                TodoListItem item = db.Get<TodoListItem>(id);
                if(item != null)
                {
                    item.isDone = !item.isDone;
                    db.Update<TodoListItem>(item);
                }
                return RedirectToAction("Index");
            }
        }
    }
}
