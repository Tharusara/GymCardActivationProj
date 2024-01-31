using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VirtuagymDevTask.Data;
using VirtuagymDevTask.DTOs;
using VirtuagymDevTask.Models;

namespace VirtuagymDevTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IGymRepository _repo;
        private readonly IMapper _mapper;

        public InvoiceController(IGymRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // api/Invoice   --Get All Invoices
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _repo.GetAllInvoices();
            return Ok(invoices);
        }

        // api/Invoice/5    -- get a single invoice details
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var invoice = await _repo.GetInvoice(id);
            if(invoice == null) return NotFound();
            return Ok(invoice);
        }

        // api/Invoice    -- create an Invoice
        [HttpPost]
        public async Task<IActionResult> Create(InvoiceToCreateDTO invoiceForCreation)
        {
            if (invoiceForCreation == null)
            {
                return BadRequest("Invoice data is Empty");
            }
            var invoice = _repo.MapInvoice(invoiceForCreation);
            _repo.Add(invoice);
            if (await _repo.SaveAll())
            {
                return Created("Invoice Successfully created", invoiceForCreation);
            }
            return BadRequest("Creating the Invoice Failed on save");
        }

        // api/Invoice  -- Update an Invoice
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, InvoiceToUpdateDTO invoiceForUpdate)
        {
            if (invoiceForUpdate == null || id == 0)
            {
                return BadRequest();
            }
            invoiceForUpdate.Id = id;
            var invoiceFromRepo = await _repo.GetInvoice(id);
            if (invoiceFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(invoiceForUpdate, invoiceFromRepo);
            if (await _repo.SaveAll())
                return Ok(new { message = "Invoice Update successfully." });

            return BadRequest($"Updating Invoice{id} failed on save");
        }

        // api/Invoice/3    -- Delete a single Invoice
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var invoiceToDelete = await _repo.GetInvoice(id);
            if (invoiceToDelete == null)
            {
                return NotFound();
            }
            _repo.Delete(invoiceToDelete);
            await _repo.SaveAll();
            return Ok(new { message = "Invoice deleted successfully." });
        }
    }
}
