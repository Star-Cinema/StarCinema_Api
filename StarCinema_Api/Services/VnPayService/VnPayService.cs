﻿using StarCinema_Api.DTOs;
using WebBanHangOnline.Models.Payments;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.Repositories.PaymentRepository;
using StarCinema_Api.Repositories.BookingRepository;

namespace StarCinema_Api.Services.VnPayService
{
    /// <summary>
    /// Account : AnhNT282
    /// Description : Class service using vnpay for function payment
    /// Date created : 2023/05/19
    /// </summary>
    public class VnPayService : IVnPayService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBookingRepository _bookingRepository;

        /// <summary>
        /// Constructor AnhNT282
        /// </summary>
        /// <param name="paymentRepository"></param>
        /// <param name="bookingRepository"></param>
        public VnPayService(IPaymentRepository paymentRepository, IBookingRepository bookingRepository)
        {
            _paymentRepository= paymentRepository;
            _bookingRepository= bookingRepository;
        }
        /// <summary>
        /// Create url payment AnhNT282
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="priceTicket"></param>
        /// <param name="priceService"></param>
        public async Task<ResponseDTO> CreateUrlPayment(int bookingID, double priceTicket, double priceService)
        {

            var booking = await _bookingRepository.GetByIdAsync(bookingID);
            if (booking == null) return new ResponseDTO
            {
                code = 404,
                message = $"Does not exist booking with id {bookingID}"
            };
            if (booking.Status != "Pending") return new ResponseDTO
            {
                code = 400,
                message = "Booking has been paid or overdue"
            };

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = configBuilder.Build();

            var urlPayment = "";
            // Get Config Info
            string vnp_Returnurl = configuration.GetSection("VnPay:vnp_ReturnUrl").Value; //URL nhan ket qua tra ve 
            string vnp_Url = configuration.GetSection("VnPay:vnp_Url").Value; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = configuration.GetSection("VnPay:vnp_TmnCode").Value; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = configuration.GetSection("VnPay:vnp_HashSecret").Value;//Secret Key

            // Build URL for VNPAY

            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", ((priceTicket + priceService) * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            vnpay.AddRequestData("vnp_BankCode", "VNBANK");


            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");

            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "BookingId=" + bookingID + " PriceTicket=" + priceTicket + " PriceService=" + priceService);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(5).ToString("yyyyMMddHHmmss"));




            // Add Params of 2.1.0 Version
            // Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return new ResponseDTO
            {
                code = 200,
                message = "Success",
                data = urlPayment
            };

        }

        // Return info payment AnhNT282
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


            // Get data from payment AnhNT282

            long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            String vnp_SecureHash = vnpayData.Where(kvp => kvp.Key == "vnp_SecureHash").FirstOrDefault().Value;
            String TerminalID = vnpayData.Where(kvp => kvp.Key == "vnp_TmnCode").FirstOrDefault().Value;
            string paymentInfo = vnpayData.Where(kvp => kvp.Key == "vnp_OrderInfo").FirstOrDefault().Value;

            int bookingId;
            double priceTicket;
            double priceService;

            ParseBookingInfo(paymentInfo, out bookingId, out priceTicket, out priceService);

            long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
            String bankCode = vnpayData.Where(kvp => kvp.Key == "vnp_BankCode").FirstOrDefault().Value; 

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
            if (checkSignature)
            {
                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                {
                    // Payment success
                    // Create payment and update status of booking

                    var payment = new Payment();
                    payment.bookingId = bookingId;
                    payment.PriceService = priceService;
                    payment.PriceTicket = priceTicket;
                    payment.CreatedDate = DateTime.Now;
                    payment.ModeOfPayment = bankCode;

                    // If payment does not exist, create a new payment
                    if (!(await _paymentRepository.IsPaymentOfBookingAlreadyExists(bookingId)))
                    {
                        await _paymentRepository.CreatePaymentAsync(payment);
                        await _paymentRepository.IsSaveChange();
                        _bookingRepository.UpdateBookingToSuccess(bookingId);
                    }

                    return new ResponseDTO
                    {
                        code = 200,
                        message = "Success",
                        data = new
                        {
                            VnpayTranId = vnpayTranId,
                            bankPayment = bankCode,
                            Amount = vnp_Amount,
                            bookingId = bookingId,
                            priceTicket = priceTicket,
                            priceService = priceService,
                            Total = priceTicket + priceService
                        }
                    };
                }
                else
                {
                    _bookingRepository.UpdateBookingToCancel(bookingId);
                    _bookingRepository.Save();
                    return new ResponseDTO
                    {
                        code = 500,
                        message = "An error occurred during processing.Error code: " + vnp_ResponseCode,
                    };
                }
            }
            else
            {
                _bookingRepository.UpdateBookingToCancel(bookingId);
                _bookingRepository.Save();
                return new ResponseDTO
                {
                    code = 500,
                    message = "Invalid signature",
                };
            }
        }

        /// <summary>
        /// Get booking info in vnp_OrderInfo AnhNT282
        /// </summary>
        /// <param name="input"></param>
        /// <param name="bookingId"></param>
        /// <param name="priceTicket"></param>
        /// <param name="priceService"></param>
        public void ParseBookingInfo(string input, out int bookingId, out double priceTicket, out double priceService)
        {
            string[] pairs = input.Split(' ');

            bookingId = 0;
            priceTicket = 0;
            priceService = 0;

            foreach (string pair in pairs)
            {
                string[] parts = pair.Split('=');
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    if (key.Equals("BookingId"))
                    {
                        int.TryParse(value, out bookingId);
                    }
                    else if (key.Equals("PriceTicket"))
                    {
                        double.TryParse(value, out priceTicket);
                    }
                    else if (key.Equals("PriceService"))
                    {
                        double.TryParse(value, out priceService);
                    }
                }
            }
        }

        public async Task<ResponseDTO> RePayment(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if(booking == null || booking.IsDelete == true)
            {
                return new ResponseDTO
                {
                    code = 404,
                    message = $"Does not exist booking with id {bookingId}"
                };
            }
            if (booking.Status != "Pending") return new ResponseDTO
            {
                code = 404,
                message = "Booking has been overdue"
            };
            return new ResponseDTO
            {
                code = 200,
                message = "Success",
                data = booking.UrlPayment
            };
        }
    }

}
