using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.DataAccess;
using System.Text.Json;
using System.Xml.Serialization;

namespace Q2.Pages
{
    public class ListModel : PageModel
    {
        private readonly Prn221TrialContext context = new Prn221TrialContext();
        public List<Service> services = new List<Service>();
        public void OnGet(string room)
        {
            if(string.IsNullOrEmpty(room))
            {
                services = context.Services
                .Include(x => x.EmployeeNavigation)
                .Where(x => x.Month == 3)
                .Where(x => x.Year == 2022)
                .ToList();
            }
            else
            {
                services = context.Services
                    .Include(x => x.EmployeeNavigation)
                    .Where(x => x.RoomTitle.Contains(room))
                    .ToList();
            }
        }

        public void OnPost(IFormFile xmlFile)
        {
            //xml file
            //var xs = new XmlSerializer(typeof(Service[], new XmlRootAttribute("ArrayOfService")));
            //var file = System.IO.File.OpenRead(xmlFile.FileName);
            //var ses = (Service[]) xs.Deserialize(file);
            //json file
            var data = System.IO.File.ReadAllText(xmlFile.FileName);
            var ses = JsonSerializer.Deserialize<Service[]>(data);
            foreach(var se in ses)
            {
                se.Id = 0;
                context.Services.Add(se);
            }
            context.SaveChanges();
            services = context.Services
                .Include(x => x.EmployeeNavigation)
                .ToList();
        }
    }
}
