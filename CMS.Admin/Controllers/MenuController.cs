using Application.MenuApp;
using Application.MenuApp.Dtos;
using CMS.Admin.Models;
using CMS.Admin.Models.Menu;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CMS.Admin.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuAppService _menuAppService;
        public MenuController(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetMenuTreeData()
        {
            var menus = _menuAppService.GetAllList();
            List<TreeModel> treeModels = new List<TreeModel>();
            foreach (var menu in menus)
            {
                treeModels.Add(new TreeModel()
                {
                    Id = menu.Id.ToString(),
                    Text = menu.Name,
                    Parent = menu.ParentId == Guid.Empty ? "#" : menu.ParentId.ToString()
                });
            }
            return Json(treeModels);
        }

        public IActionResult GetMenusByParent(Guid parentId, int startPage, int pageSize)
        {
            int rowCount = 0;
            var result = _menuAppService.GetMenusByParent(parentId, startPage, pageSize, out rowCount);
            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = result
            });
        }

        public IActionResult Edit(MenuDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultModel
                {
                    Result = "Faild",
                    Message = GetModelStateError()
                });
            }
            if (_menuAppService.InsertOrUpdate(dto))
            {
                return Json(new ResultModel { Result = "Success" });
            }

            return Json(new ResultModel() { Result = "Success" });
        }

        public IActionResult DeleteMuti(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                List<Guid> delIds = new List<Guid>();
                foreach (string id in idArray)
                {
                    delIds.Add(Guid.Parse(id));
                }

                _menuAppService.DeleteBatch(delIds);
                return Json(new ResultModel { Result = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new ResultModel { Result = "Faild", Message = ex.Message });
            }
        }

        public IActionResult Delete(Guid id)
        {
            try
            {
                _menuAppService.Delete(id);
                return Json(new ResultModel
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                return Json(new ResultModel
                {
                    Result = "Faild",
                    Message = ex.Message
                });
            }
        }
        public ActionResult Get(Guid id)
        {
            var dto = _menuAppService.Get(id);
            return Json(dto);
        }


    }
}