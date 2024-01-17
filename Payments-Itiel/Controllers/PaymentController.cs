using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payments_Itiel.Entities;
using Payments_Itiel.Models;
using System.Security.Claims;

namespace Payments_Itiel.Controllers
{
    public class PaymentController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppDbContext _context;
        public PaymentController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                RedirectToAction("Login", "User");
            }
            return View();
        }
        public IActionResult Create()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                RedirectToAction("Login", "User");
            }
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {   
            var PaymentToUpdate = await _context.Payment.FindAsync(id);

            if (PaymentToUpdate == null)
            {
                return NotFound();
            }
            var PaymentToUpdateViewModel = new PaymentViewModel()
            {
                ID = id,
                Title = PaymentToUpdate.Title,
                Description = PaymentToUpdate.Description,
                Total = PaymentToUpdate.Total,
            };

            return View(PaymentToUpdateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentViewModel payment)
        {
            if (ModelState.IsValid)
            {
                
                string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if(userId == null)
                {
                    RedirectToAction("Login", "User");
                }

                _context.Payment.Add(new Payment
                {
                    Description = payment.Description,
                    Total = payment.Total,
                    Title = payment.Title,
                    UserId = userId,
                });

                await _context.SaveChangesAsync();

                return RedirectToAction("Get", "Payment");
            }
            return View(payment);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string? currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(currentUserId == null)
            {
                RedirectToAction("Register", "User");
            }
            List<Payment> payments = _context.Payment.Where(payment => payment.UserId == currentUserId).ToList();
            List<PaymentViewModel> paymentViewModels = payments
        .Select(payment => new PaymentViewModel
            {
                ID = payment.ID,
                Title= payment.Title,
                Description = payment.Description,
                Total = payment.Total,
                // Map other properties as needed
            })
            .ToList();
            return View(paymentViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var paymentToDelete = await _context.Payment.FindAsync(id);

            if (paymentToDelete == null)
            {
                return NotFound();
            }

            _context.Payment.Remove(paymentToDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction("Get","Payment");
        }

        [HttpPost]
        public async Task<IActionResult> Update(PaymentViewModel payment)
        {
            var paymentToDelete = await _context.Payment.FindAsync(payment.ID);

            if (paymentToDelete == null)
            {
                return NotFound();
            }
            paymentToDelete.Title = payment.Title;
            paymentToDelete.Description = payment.Description;
            paymentToDelete.Total = payment.Total;
            await _context.SaveChangesAsync();

            return RedirectToAction("Get", "Payment");
        }
    }
}
