using ApplicationCore.Interfaces;
using Infrastructure.Enums;
using Infrastructure.Utils;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IKoiOrderService koiOrderService;
        private readonly IConfiguration configuration;

        private readonly string url = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private readonly string tmnCode = "61U8WANU";
        private readonly string hashSecret = "CTAO26RGDX84IALDTG3KKWS5AVP4BRCV";

        public VNPayController(IKoiOrderService koiOrderService, IConfiguration configuration)
        {
            this.koiOrderService = koiOrderService;
            this.configuration = configuration;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }

        [HttpPost("Payment")]
        public IActionResult CreatePayment(int orderId)
        {
            var order = koiOrderService.GetOrderDetailsAsync(orderId).Result.Data;

            if (order == null)
            {
                return NotFound(new { Message = "Order not found." });
            }
            if (order.Status != OrderStatus.WaitingForPayment.ToString())
            {
                return BadRequest(new { Message = "This order cannot be processed for payment." });
            }

            string scheme = Request.Scheme;
            string host = Request.Host.Host;
            int? port = Request.Host.Port;
            string returnUrl = $"{scheme}://{host}:{port}/api/vnpay/PaymentConfirm";
            string txnRef = $"{Guid.NewGuid()}";

            string clientIPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            PayLib pay = new PayLib();
            pay.AddRequestData("vnp_Version", "2.1.0");
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", tmnCode);
            pay.AddRequestData("vnp_Amount", (Convert.ToInt32(order.Total) * 100).ToString());
            pay.AddRequestData("vnp_BankCode", "");
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", clientIPAddress);
            pay.AddRequestData("vnp_Locale", "vn");
            pay.AddRequestData("vnp_OrderInfo", orderId.ToString());
            pay.AddRequestData("vnp_OrderType", "other");
            pay.AddRequestData("vnp_ReturnUrl", returnUrl);
            pay.AddRequestData("vnp_TxnRef", txnRef);

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
            return Ok(new { PaymentUrl = paymentUrl });
            }

        [HttpGet("PaymentConfirm")]
        public IActionResult PaymentConfirm()
        {
            try
            {
                if (Request.QueryString.HasValue)
                {
                    var queryString = Request.QueryString.Value;
                    var json = HttpUtility.ParseQueryString(queryString);

                    string txnRef = json["vnp_TxnRef"];
                    string orderInfo = json["vnp_OrderInfo"];
                    long vnpayTranId = Convert.ToInt64(json["vnp_TransactionNo"]);
                    string vnp_ResponseCode = json["vnp_ResponseCode"];
                    string vnp_SecureHash = json["vnp_SecureHash"];
                    var pos = Request.QueryString.Value.IndexOf("&vnp_SecureHash");

                    var order = koiOrderService.GetOrderDetailsAsync(int.Parse(orderInfo)).Result.Data;

                    if (order == null)
                    {
                        return NotFound(new { Message = "Order not found." });
                    }

                    bool checkSignature = ValidateSignature(Request.QueryString.Value.Substring(1, pos - 1), vnp_SecureHash, hashSecret);
                    if (checkSignature && tmnCode == json["vnp_TmnCode"])
                    {
                        if (vnp_ResponseCode == "00")
                        {
                            var status = OrderStatus.Pending.ToString();
                            koiOrderService.UpdateOrderStatus(order.OrderId, status);
                            var returnUrl = configuration["PaymentSettings:ReturnURL"];
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return BadRequest(new { Message = "Payment Failed", ResponseCode = vnp_ResponseCode });
                        }
                    }
                    else
                    {
                        return BadRequest(new { Message = "Invalid Signature" });
                    }
                }
                return BadRequest(new { Message = "Invalid Request" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        private bool ValidateSignature(string rspraw, string inputHash, string secretKey)
        {
            string myChecksum = PayLib.HmacSHA512(secretKey, rspraw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
