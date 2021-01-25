using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTable.Core.Models;
using TimeTableServer.Context;
using TimeTable.App.Views;
using System.Web.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeTableServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableController : ControllerBase
    {
        public TimeTableController()
        {

        }

    // GET: api/<TimeTableController>
        [HttpGet]
        public IEnumerable<TimeSlotDTO> Get()
        {
            using (var context=new DataContext())
            {
                var timeslots = context.TimeSlots
                    .Include(slot => slot.Course)
                    .ThenInclude(course=>course.Teacher)
                    .Include(slot => slot.Course)
                    .ThenInclude(course => course.Students)

                    .Include(slot => slot.Place).ToList();
                return timeslots.Select(slot => new TimeSlotDTO()
                {
                    Course = slot.Course.Title,
                    Place = slot.Place.Name,
                    Start = slot.Start.TotalMilliseconds,
                    End = slot.End.TotalMilliseconds,
                    Students=slot.Course.Students.Count,
                    Day = slot.Day
                }).ToList();

            }
        }

        
    }
}
