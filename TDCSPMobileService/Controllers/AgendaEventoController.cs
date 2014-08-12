using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using TDCSPMobileService.DataObjects;
using TDCSPMobileService.Models;

namespace TDCSPMobileService.Controllers
{
    public class AgendaEventoController : TableController<AgendaEvento>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            TDCSPMobileServiceContext context = new TDCSPMobileServiceContext();
            DomainManager = new EntityDomainManager<AgendaEvento>(context, Request, Services);
        }

        // GET tables/AgendaEvento
        public IQueryable<AgendaEvento> GetAllAgendaEvento()
        {
            return Query(); 
        }

        // GET tables/AgendaEvento/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<AgendaEvento> GetAgendaEvento(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/AgendaEvento/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<AgendaEvento> PatchAgendaEvento(string id, Delta<AgendaEvento> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/AgendaEvento/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostAgendaEvento(AgendaEvento item)
        {
            AgendaEvento current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/AgendaEvento/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAgendaEvento(string id)
        {
             return DeleteAsync(id);
        }

    }
}