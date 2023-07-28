using Casgem_DapperProject.DAL.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Data.SqlClient;

namespace Casgem_DapperProject.Controllers
{
    public class HeadingController : Controller
    {
        private readonly string _connectionString = "Server =DESKTOP-D19MH5E\\SQLEXPRESS; initial catalog = CasgemDbDapper; integrated security = true";

        public async Task<IActionResult> Index()
        {
            await using var connection = new SqlConnection(_connectionString);
            var values = (await connection.QueryAsync<Headings>("Select * from TblHeading")).AsList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddHeading()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddHeading(Headings headings)
        {
            await using var connection = new SqlConnection(_connectionString);
            var query = $"Insert Into TblHeading (HeadingName, HeadingStatus) Values ('{headings.HeadingName}', 'True')";
            await connection.QueryAsync(query);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteHeading(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync($"Delete From TblHeading Where HeadingID = '{id}'");
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> UpdateHeading(int id)
        {
            await using var connection = new SqlConnection(_connectionString);
            var values = await connection.QueryFirstAsync<Headings>($"Select * From TblHeading Where HeadingID = '{id}'");
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHeading(Headings headings)
        {
            await using var connection = new SqlConnection(_connectionString);
            var query = $"Update TblHeading set HeadingName = '{headings.HeadingName}', HeadingStatus ='{headings.HeadingStatus}'  Where HeadingID = '{headings.HeadingID}'";
            await connection.QueryAsync(query);
            return RedirectToAction("Index");
        }
    }


}

