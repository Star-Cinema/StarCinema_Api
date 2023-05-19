using StarCinema_Api.DTOs;
using StarCinema_Api.Services.BookingService;
using WebBanHangOnline.Models.Payments;
using StarCinema_Api.Data.Entities;
using Org.BouncyCastle.Asn1.X9;

namespace StarCinema_Api.Services.VnPayService
{
    public class VnPayService : IVnPayService
    {
        private readonly IBookingService _bookingService;
        public VnPayService(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public async Task<ResponseDTO> CreateUrlPayment(int bookingID)
        {
            //var resBooking = await _bookingService.GetBookingById(bookingID);
            //if (resBooking.code != 200)
            //{
            //    return resBooking;
            //}
            //var bookingDto = resBooking.data as Bookings;

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = configBuilder.Build();

            var urlPayment = "";
            ////Get Config Info
            string vnp_Returnurl = configuration.GetSection("VnPay:vnp_ReturnUrl").Value; //URL nhan ket qua tra ve 
            string vnp_Url = configuration.GetSection("VnPay:vnp_Url").Value; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = configuration.GetSection("VnPay:vnp_TmnCode").Value; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = configuration.GetSection("VnPay:vnp_HashSecret").Value;//Secret Key

            ////Build URL for VNPAY

            //Get payment input
            Payment payment = new Payment();
            payment.Id = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
            payment.Amount = 100000; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
            payment.CreatedDate = DateTime.Now;

            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (payment.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            vnpay.AddRequestData("vnp_BankCode", "VNBANK");

            //if (bankcode_Vnpayqr.Checked == true)
            //{
            //    vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            //}
            //else if (bankcode_Vnbank.Checked == true)
            //{
            //}
            //else if (bankcode_Intcard.Checked == true)
            //{
            //    vnpay.AddRequestData("vnp_BankCode", "INTCARD");

            //}
            //vnpay.AddRequestData("vnp_BankCode", "VNBANK");


            vnpay.AddRequestData("vnp_CreateDate", payment.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");

            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + payment.Id);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", payment.Id.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return new ResponseDTO
            {
                code = 200,
                message = "Success",
                data = urlPayment
            };

        }

        public async Task<ResponseDTO> ReturnPayment(IQueryCollection vnpayData)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = configBuilder.Build();

            string vnp_HashSecret = configuration.GetSection("VnPay:vnp_HashSecret").Value;//Secret Key
            VnPayLibrary vnpay = new VnPayLibrary();

            foreach(var kvp in vnpayData)
            {
                //get all querystring data
                if (!string.IsNullOrEmpty(kvp.Key) && kvp.Key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(kvp.Key, kvp.Value);
                }
            }
            //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
            //vnp_TransactionNo: Ma GD tai he thong VNPAY
            //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
            //vnp_SecureHash: HmacSHA512 cua du lieu tra ve



            long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            String vnp_SecureHash = vnpayData.Where(kvp => kvp.Key == "vnp_SecureHash").FirstOrDefault().Value;
            String TerminalID = vnpayData.Where(kvp => kvp.Key == "vnp_TmnCode").FirstOrDefault().Value;
            long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
            String bankCode = vnpayData.Where(kvp => kvp.Key == "vnp_BankCode").FirstOrDefault().Value; 

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
            if (checkSignature)
            {
                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                {
                    //Thanh toan thanh cong
                    //displayMsg.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                    return new ResponseDTO
                    {
                        code = 200,
                        message = "Success",
                        data = new
                        {
                            VnpayTranId = vnpayTranId,
                            bankPayment = bankCode,
                            Amount = vnp_Amount
                        }
                    };
                }
                else
                {
                    return new ResponseDTO
                    {
                        code = 500,
                        message = "An error occurred during processing.Error code: " + vnp_ResponseCode,
                    };
                }
            }
            else
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = "Invalid signature",
                };
            }
        }

    }

}
