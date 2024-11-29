using ASM2_PH52810.Entity;
using ASM2_PH52810.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASM2_PH52810.Controllers
{
    [Route("/")]
    [ApiController]
    public class BackEndController : ControllerBase
    {
        private readonly AsmbackendContext _context;

        public BackEndController(AsmbackendContext context)
        {
            _context = context;
        }
        // 1. Lấy thông tin tất cả các loại tài nguyên trong game
        [HttpGet("LayTaiNguyen")]
        public IActionResult LayTaiNguyen()
        {
            var resources = _context.Resources.ToList();
            return Ok(resources);
        }
        // 2. Lấy thông tin tất cả người chơi theo từng chế độ chơi
        [HttpGet("LayThongTinPlayerTheoCheDo")]
        public IActionResult GetPlayersByMode([FromQuery] string mode)
        {
            var players = _context.Players.Where(p => p.Mode == mode).ToList();
            return Ok(players);
        }

        // 3. Lấy tất cả các vũ khí có giá trị trên 100 điểm kinh nghiệm
        [HttpGet("LayVuKhi")]
        public IActionResult GetWeaponsAbove100()
        {
            var weapons = _context.Weapons.Where(w => w.ExperiencePoints > 100).ToList();
            return Ok(weapons);
        }

        // 4. Lấy thông tin các item mà người chơi có thể mua với số điểm kinh nghiệm tích lũy hiện tại
        [HttpGet("LayThongTinItem")]
        public IActionResult GetAvailableItems([FromQuery] int playerId)
        {
            var player = _context.Players.FirstOrDefault(p => p.PlayerId == playerId);
            if (player == null) return NotFound("Player not found");

            var items = _context.Items.Where(i => i.ExperienceCost <= player.TotalExperience).ToList();
            return Ok(items);
        }

        // 5. Lấy thông tin các item có tên chứa từ 'kim cương' và có giá trị dưới 500 điểm kinh nghiệm
        [HttpGet("LayKimCuong")]
        public IActionResult GetDiamondItems()
        {
            var items = _context.Items.Where(i => i.ItemName.Contains("kim cương") && i.ExperienceCost < 500).ToList();
            return Ok(items);
        }

        // 6. Lấy thông tin tất cả các giao dịch mua item và phương tiện của một người chơi
        [HttpGet("LayTTGiaoDich")]
        public IActionResult GetTransactions(int playerId)
        {
            var transactions = _context.Transactions.Where(t => t.PlayerId == playerId).OrderBy(t => t.TransactionDate).ToList();
            return Ok(transactions);
        }

        // 7. Thêm thông tin của một item mới
        [HttpPost("Additems")]
        public IActionResult AddItem([FromBody] Item newItem)
        {
            _context.Items.Add(newItem);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAvailableItems), new { playerId = newItem.ItemId }, newItem);
        }

        // 8. Cập nhật mật khẩu của người chơi
        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword(int playerId, [FromBody] string newPassword)
        {
            var player = _context.Players.FirstOrDefault(p => p.PlayerId == playerId);
            if (player == null) return NotFound("Player not found");

            player.Password = newPassword;
            _context.SaveChanges();
            return Ok(player);
        }

        // 9. Lấy danh sách các item được mua nhiều nhất
        [HttpGet("ItemMuaNhieuNhat")]
        public IActionResult GetMostPurchasedItems()
        {
            var mostPurchasedItems = _context.Purchases
                .GroupBy(p => p.ItemId)
                .OrderByDescending(g => g.Count())
                .Select(g => new { ItemId = g.Key, PurchaseCount = g.Count() })
                .Take(10)
                .ToList();

            return Ok(mostPurchasedItems);
        }

        // 10. Lấy danh sách tất cả người chơi và số lần họ đã mua hàng
        [HttpGet("LichSuMuaHang")]
        public IActionResult GetPlayerPurchaseCounts()
        {
            var playerPurchases = _context.Purchases
                .GroupBy(p => p.PlayerId)
                .Select(g => new { PlayerId = g.Key, PurchaseCount = g.Count() })
                .ToList();

            return Ok(playerPurchases);
        }
    }
}
