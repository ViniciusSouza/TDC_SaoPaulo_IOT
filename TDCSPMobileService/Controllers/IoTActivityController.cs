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
    public class IoTActivityController : TableController<IoTActivity>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            TDCSPMobileServiceContext context = new TDCSPMobileServiceContext();
            DomainManager = new EntityDomainManager<IoTActivity>(context, Request, Services);
        }

        // GET tables/IoTActivity
        public IQueryable<IoTActivity> GetAllIoTActivity()
        {
            return Query(); 
        }

        // GET tables/IoTActivity/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<IoTActivity> GetIoTActivity(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/IoTActivity/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<IoTActivity> PatchIoTActivity(string id, Delta<IoTActivity> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/IoTActivity/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostIoTActivity(IoTActivity item)
        {
            IoTActivity current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/IoTActivity/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteIoTActivity(string id)
        {
             return DeleteAsync(id);
        }

    }
}