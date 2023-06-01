using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace StarCinema_Api.Data.Seed
{
    public class Seed 
    {
        public static async void SeedUsers(MyDbContext context)
        {     
            if (context.Roles.Any()) return;
            List<Role> roles = new List<Role>()
            {
                new Role() {Name="admin"},
                new Role() {Name="user"}
            };
            await context.Roles.AddRangeAsync(roles);

            Rooms room = new Rooms();
            room.Name = "Room 1";
            await context.Rooms.AddAsync(room);

            List<Seats> seats = new List<Seats>();
            char[] arr = new char[] { 'A', 'B', 'E', 'F' };
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    var seat = new Seats() { Name = arr[i] + "" + j, RoomId = 1 };
                    seats.Add(seat);
                }
            }
            await context.Seats.AddRangeAsync(seats);

            List<Categories> categories = new List<Categories>()
            {
                new Categories() {Name = "Hoạt hình 3D"},
                new Categories() {Name = "Tình cảm"},
                new Categories() {Name = "Đời sống"},
                new Categories() {Name = "Hành động"},
                new Categories() {Name = "Kinh dị"}
            };
            await context.Categories.AddRangeAsync(categories);

            List<Films> films = new List<Films>()
            {
                new Films() {Name = "THE LITTLE MERMAID", CategoryId = 1, Description = "“Nàng Tiên Cá” là câu chuyện được yêu thích về Ariel - một nàng tiên cá trẻ xinh đẹp và mạnh mẽ với khát khao phiêu lưu. Ariel là con gái út của Vua Triton và cũng là người ngang ngạnh nhất, nàng khao khát khám phá về thế giới bên kia đại dương. Trong một lần ghé thăm đất liền, nàng đã phải lòng Hoàng tử Eric bảnh bao. Trong khi tiên cá bị cấm tiếp xúc với con người, Ariel đã làm theo trái tim mình. Nàng đã thỏa thuận với phù thủy biển Ursula hung ác để cơ hội sống cuộc sống trên đất liền. Nhưng cuối cùng việc này lại đe dọa tới mạng sống của Ariel và vương miện của cha nàng. Phim mới The Little Mermaid khởi chiếu 21.04.2023 tại rạp chiếu phim toàn quốc.",
                    Country = "Mỹ", Director = "Rob Marshall", Duration = 135, Producer = "Walt Disney Pictures", Release = new DateTime(2023,05,31,00,00,00), VideoLink = "https://youtu.be/RxXHUnAi45E" },
                new Films() {Name = "FAST & FURIOUS 10", CategoryId = 4, Description = "Trong Fast Five (2011), Dom và nhóm của anh đã tiêu diệt trùm ma túy người Brazil Hernan Reyes ở Rio De Janeiro. Điều họ không biết là con trai của Reyes, Dante đã chứng kiến tất cả và dành 12 năm qua để lên một kế hoạch “hoàn hảo” sẽ khiến gia đình Dom phải trả giá đắt. Trải qua nhiều nhiệm vụ khó khăn tưởng chừng như bất khả thi nhưng Dom Toretto và gia đình của anh ấy đều đã vượt qua. Họ đánh bại mọi kẻ thù trên hành trình hơn 20 năm qua. Nhưng giờ đây, Dante được đánh giá là kẻ nguy hiểm nhất mà họ sẽ đối mặt: một mối đe dọa đáng sợ xuất hiện từ bóng tối của quá khứ, một kẻ thù đẫm máu, với quyết tâm phá tan gia đình và phá hủy mọi thứ mà Dom yêu thương mãi mãi. Phim mới Fast & Furious 10 ra mắt tại các rạp chiếu phim từ 19.05.2023.",
                    Country = "Mỹ", Director = "Louis Leterrier", Duration = 141, Producer = "Universal Pictures", Release = new DateTime(2023,05,31,00,00,00), VideoLink = "https://youtu.be/jTHpOm6L2FQ" },
                new Films() {Name = "DORAEMON: NOBITA’S SKY UTOPIA 2023", CategoryId = 1, Description = "Doraemon: Nobita’s Sky Utopia 2023 kể về chuyến phiêu lưu của Doraemon, Nobita và những người bạn thân tới Paradapia - một hòn đảo hình trăng lưỡi liềm lơ lửng trên bầu trời. Ở nơi đó, tất cả đều hoàn hảo… đến mức cậu nhóc Nobita mê ngủ ngày cũng có thể trở thành một thần đồng toán học, một siêu sao thể thao. Cả hội Doraemon cùng sử dụng một món bảo bối độc đáo chưa từng xuất hiện trước đây để đến với vương quốc tuyệt vời này. Cùng với những người bạn ở đây, đặc biệt là chàng robot mèo Sonya, nhóm Doraemon đã có chuyến hành trình tới vương quốc trên mây tuyệt vời… cho đến khi những bí mật đằng sau vùng đất lý tưởng này được hé lộ.  Phim Điện Ảnh Doraemon: Nobita và Vùng Đất Lý Tưởng Trên Bầu Trời ra mắt tại các rạp chiếu phim từ 26.05.2023",
                    Country = "Nhật Bản", Director = "Douyama Takumi", Duration = 108, Producer = "Shin-Ei Animation", Release = new DateTime(2023,05,31,00,00,00), VideoLink = "https://youtu.be/bUTfUVLP_Zk" },
                new Films() {Name = "LẬT MẶT 6: TẤM VÉ ĐỊNH MỆNH", CategoryId = 3, Description = "Tấm vé có mệnh giá 10.000 đồng và sở hữu những con số \"định mệnh\": 10, 16, 18, 20, 27, 28 - ngày sinh của hội bạn thân sáu người do Trung Dũng, Quốc Cường, Thanh Thức, Huy Khánh, Hoàng Mèo, Trần Kim Hải đảm nhận. Tuy nhiên, nhân vật do Thanh Thức thủ vai, cũng là người giữ tấm vé trúng giải độc đắc lại không may bị tai nạn và qua đời, từ đây, những người còn lại phải dùng đủ mọi cách để tìm lại tấm vé “đổi đời”. Liệu nhóm bạn có thành công và giải mã được cái chết bị ẩn người người bạn thân? Cùng chờ đón đến 28.04 để biết được câu trả lời nha! Phim mới Lật Mặt 6: Tấm Vé Định Mệnh ra mắt tại các rạp chiếu phim từ 28.04.2023.",
                    Country = "Việt Nam", Director = "Lý Hải", Duration = 132, Producer = "Unknown", Release = new DateTime(2023,05,31,00,00,00), VideoLink = "https://youtu.be/2EnP2tVC00Q" },

                new Films() {Name = "SPIDER-MAN: ACROSS THE SPIDER-VERSE", CategoryId = 1, Description = "Miles Morales tái xuất trong phần tiếp theo của bom tấn hoạt hình từng đoạt giải Oscar - Spider-Man: Across the Spider-Verse. Sau khi gặp lại Gwen Stacy, chàng Spider-Man thân thiện đến từ Brooklyn phải du hành qua đa vũ trụ và gặp một nhóm Người Nhện chịu trách nhiệm bảo vệ các thế giới song song. Nhưng khi nhóm siêu anh hùng xung đột về cách xử lý một mối đe dọa mới, Miles buộc phải đọ sức với các Người Nhện khác và phải xác định lại ý nghĩa của việc trở thành một người hùng để có thể cứu những người cậu yêu thương nhất. Phim mới Người Nhện: Du Hành Vũ Trụ Nhện dự kiến khởi chiếu 01.06.2023 tại các rạp chiếu phim toàn quốc.",
                    Country = "Mỹ", Director = "Joaquim Dos Santos", Duration = 140, Producer = "Sony Pictures", Release = new DateTime(2023,06,05,00,00,00), VideoLink = "https://youtu.be/HVgwRbQfpCc" },
                new Films() {Name = "ROUND UP: NO WAY OUT", CategoryId = 4, Description = "Quái vật cơ bắp Seok-do (Ma Dong Seok) dẫn đầu đội hình sự truy lùng đường dây buôn chất cấm của thiếu gia Joo Seong Cheol. Cuộc truy đuổi càng thêm gay cấn khi cú đấm công lý \"chú Ma\" chạm trán thanh kiếm lừng lẫy chốn giang hồ Nhật Bản. Phim mới Vây Hãm: Ngoài Vòng Pháp Luật khởi chiếu 02.06.2023 tại rạp chiếu phim toàn quốc.",
                    Country = "Hàn Quốc", Director = "Lee Sang Young", Duration = 105, Producer = "BA Entertainment", Release = new DateTime(2023,06,05,00,00,00), VideoLink = "https://youtu.be/ze0YBIE0ZkA" },
                new Films() {Name = "KHANZAB : TIẾNG GỌI ÂM BINH", CategoryId = 5, Description = "Chuyện phim theo chân Rahayu - cô gái từng chứng kiến cha mình bị giết hại trong vụ thảm sát Banyuwangi năm 1998. Tại đây, những thầy cúng bị nghi ngờ thực hành ma thuật đen sẽ bị người dân giả dạng ninja để sát hại. Sau sự cố này, Rahayu cùng gia đình quyết định rời khỏi Banyuwangi để chuyển đến quê hương của họ ở Jetis, Yogyakarta. Phim mới Tiếng Gọi Âm Binh khởi chiếu 26.05.2023 tại rạp chiếu phim toàn quốc.",
                    Country = "Indonesia", Director = "Anggy Umbara", Duration = 88, Producer = "PT Umbara Brothers Film", Release = new DateTime(2023,06,05,00,00,00), VideoLink = "https://youtu.be/RSADESwWRyw" },
                new Films() {Name = "THE CREEPING : OÁN HỒN", CategoryId = 5, Description = "Trải nghiệm thời thơ ấu đau thương, Anna trở về căn nhà xưa để chăm sóc người bà ốm yếu. Từ đó, những điều kỳ lạ bắt đầu xảy ra và các sự kiện kỳ quái dần xuất hiện cho đến khi Anna phát hiện ra mọi việc có liên quan đến một quá khứ bi thảm đã ám lên các thành viên trong gia đình. Điều gì sẽ xảy ra với Anna khi mọi oán niệm được ẩn giấu phía sau ngôi nhà và người bà kỳ lạ? Phim mới Oán Hồn khởi chiếu 26.05.2023 tại các rạp chiếu phim toàn quốc.",
                    Country = "Mỹ", Director = "Jamie Hooper", Duration = 94, Producer = "Cryptoscope Films", Release = new DateTime(2023,06,05,00,00,00), VideoLink = "https://youtu.be/2EnP2tVC00Q" },
            };
            await context.Films.AddRangeAsync(films);

            List<Images> images = new List<Images>()
            {
                new Images() {FilmId = 1, Name = "1", Path = "1"},
                new Images() {FilmId = 2, Name = "2", Path = "2"},
                new Images() {FilmId = 3, Name = "3", Path = "3"},
                new Images() {FilmId = 4, Name = "4", Path = "4"},
                new Images() {FilmId = 5, Name = "5", Path = "5"},
                new Images() {FilmId = 6, Name = "6", Path = "6"},
                new Images() {FilmId = 7, Name = "7", Path = "7"},
                new Images() {FilmId = 8, Name = "8", Path = "8"},
            };
            await context.Images.AddRangeAsync(images);


            List<Schedules> schedules = new List<Schedules>()
            {
                new Schedules() {StartTime = new DateTime(2023,05,31,09,00,00), EndTime= new DateTime(2023,05,31,11,00,00), FilmId = 1, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,05,31,11,30,00), EndTime= new DateTime(2023,05,31,13,30,00), FilmId = 2, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,05,31,14,00,00), EndTime= new DateTime(2023,05,31,16,00,00), FilmId = 3, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,05,31,16,30,00), EndTime= new DateTime(2023,05,31,18,30,00), FilmId = 4, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,01,07,00,00), EndTime= new DateTime(2023,06,01,08,45,00), FilmId = 1, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,01,19,00,00), EndTime= new DateTime(2023,06,01,20,45,00), FilmId = 1, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,01,21,00,00), EndTime= new DateTime(2023,06,01,22,45,00), FilmId = 1, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,02,07,00,00), EndTime= new DateTime(2023,06,02,08,45,00), FilmId = 2, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,02,19,00,00), EndTime= new DateTime(2023,06,02,20,45,00), FilmId = 2, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,02,21,00,00), EndTime= new DateTime(2023,06,02,22,45,00), FilmId = 2, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,01,11,30,00), EndTime= new DateTime(2023,06,01,13,30,00), FilmId = 2, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,01,14,00,00), EndTime= new DateTime(2023,06,01,16,00,00), FilmId = 3, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,01,16,30,00), EndTime= new DateTime(2023,06,01,18,30,00), FilmId = 4, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,02,09,00,00), EndTime= new DateTime(2023,06,02,11,00,00), FilmId = 1, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,02,11,30,00), EndTime= new DateTime(2023,06,02,13,30,00), FilmId = 2, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,02,14,00,00), EndTime= new DateTime(2023,06,02,16,00,00), FilmId = 3, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,02,16,30,00), EndTime= new DateTime(2023,06,02,18,30,00), FilmId = 4, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,03,09,00,00), EndTime= new DateTime(2023,06,03,11,00,00), FilmId = 1, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,03,11,30,00), EndTime= new DateTime(2023,06,03,13,30,00), FilmId = 2, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,03,14,00,00), EndTime= new DateTime(2023,06,03,16,00,00), FilmId = 3, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,03,16,30,00), EndTime= new DateTime(2023,06,03,18,30,00), FilmId = 4, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,04,09,00,00), EndTime= new DateTime(2023,06,04,11,00,00), FilmId = 1, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,04,11,30,00), EndTime= new DateTime(2023,06,04,13,30,00), FilmId = 2, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,04,14,00,00), EndTime= new DateTime(2023,06,04,16,00,00), FilmId = 3, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,04,16,30,00), EndTime= new DateTime(2023,06,04,18,30,00), FilmId = 4, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,05,09,00,00), EndTime= new DateTime(2023,06,05,11,00,00), FilmId = 5, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,05,11,30,00), EndTime= new DateTime(2023,06,05,13,30,00), FilmId = 6, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,05,14,00,00), EndTime= new DateTime(2023,06,05,16,00,00), FilmId = 7, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,05,16,30,00), EndTime= new DateTime(2023,06,05,18,30,00), FilmId = 8, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,06,09,00,00), EndTime= new DateTime(2023,06,06,11,00,00), FilmId = 5, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,06,11,30,00), EndTime= new DateTime(2023,06,06,13,30,00), FilmId = 6, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,06,14,00,00), EndTime= new DateTime(2023,06,06,16,00,00), FilmId = 7, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,06,16,30,00), EndTime= new DateTime(2023,06,06,18,30,00), FilmId = 8, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,07,09,00,00), EndTime= new DateTime(2023,06,07,11,00,00), FilmId = 5, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,07,11,30,00), EndTime= new DateTime(2023,06,07,13,30,00), FilmId = 6, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,07,14,00,00), EndTime= new DateTime(2023,06,07,16,00,00), FilmId = 7, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,07,16,30,00), EndTime= new DateTime(2023,06,07,18,30,00), FilmId = 8, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,08,09,00,00), EndTime= new DateTime(2023,06,08,11,00,00), FilmId = 5, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,08,11,30,00), EndTime= new DateTime(2023,06,08,13,30,00), FilmId = 6, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,08,14,00,00), EndTime= new DateTime(2023,06,08,16,00,00), FilmId = 7, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,08,16,30,00), EndTime= new DateTime(2023,06,08,18,30,00), FilmId = 8, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,09,09,00,00), EndTime= new DateTime(2023,06,09,11,00,00), FilmId = 5, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,09,11,30,00), EndTime= new DateTime(2023,06,09,13,30,00), FilmId = 6, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,09,14,00,00), EndTime= new DateTime(2023,06,09,16,00,00), FilmId = 7, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,09,16,30,00), EndTime= new DateTime(2023,06,09,18,30,00), FilmId = 8, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,07,07,00,00), EndTime= new DateTime(2023,06,07,08,45,00), FilmId = 6, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,07,19,00,00), EndTime= new DateTime(2023,06,07,20,45,00), FilmId = 6, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,07,21,00,00), EndTime= new DateTime(2023,06,07,22,45,00), FilmId = 6, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,08,07,00,00), EndTime= new DateTime(2023,06,08,08,45,00), FilmId = 8, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,08,19,00,00), EndTime= new DateTime(2023,06,08,20,45,00), FilmId = 8, RoomId = 1 },
                new Schedules() {StartTime = new DateTime(2023,06,08,21,00,00), EndTime= new DateTime(2023,06,08,22,45,00), FilmId = 8, RoomId = 1 },

            };
            await context.Schedules.AddRangeAsync(schedules);

            List<Tickets> tickets = new List<Tickets>()
            {
                new Tickets() {Price = 50000, ScheduleId = 1},
                new Tickets() {Price = 50000, ScheduleId = 2},
                new Tickets() {Price = 50000, ScheduleId = 3},
                new Tickets() {Price = 50000, ScheduleId = 4},
                new Tickets() {Price = 50000, ScheduleId = 5},
                new Tickets() {Price = 50000, ScheduleId = 6},
                new Tickets() {Price = 50000, ScheduleId = 7},
                new Tickets() {Price = 50000, ScheduleId = 8},
                new Tickets() {Price = 50000, ScheduleId = 9},
                new Tickets() {Price = 50000, ScheduleId = 10},
                new Tickets() {Price = 50000, ScheduleId = 11},
                new Tickets() {Price = 50000, ScheduleId = 12},
                new Tickets() {Price = 50000, ScheduleId = 13},
                new Tickets() {Price = 50000, ScheduleId = 14},
                new Tickets() {Price = 50000, ScheduleId = 15},
                new Tickets() {Price = 50000, ScheduleId = 16},
                new Tickets() {Price = 50000, ScheduleId = 17},
                new Tickets() {Price = 50000, ScheduleId = 18},
                new Tickets() {Price = 50000, ScheduleId = 19},
                new Tickets() {Price = 50000, ScheduleId = 20},
                new Tickets() {Price = 50000, ScheduleId = 21},
                new Tickets() {Price = 50000, ScheduleId = 22},
                new Tickets() {Price = 50000, ScheduleId = 23},
                new Tickets() {Price = 50000, ScheduleId = 24},
                new Tickets() {Price = 50000, ScheduleId = 25},
                new Tickets() {Price = 50000, ScheduleId = 26},
                new Tickets() {Price = 50000, ScheduleId = 27},
                new Tickets() {Price = 50000, ScheduleId = 28},
                new Tickets() {Price = 50000, ScheduleId = 29},
                new Tickets() {Price = 50000, ScheduleId = 30},
                new Tickets() {Price = 50000, ScheduleId = 31},
                new Tickets() {Price = 50000, ScheduleId = 32},
                new Tickets() {Price = 50000, ScheduleId = 33},
                new Tickets() {Price = 50000, ScheduleId = 34},
                new Tickets() {Price = 50000, ScheduleId = 35},
                new Tickets() {Price = 50000, ScheduleId = 36},
                new Tickets() {Price = 50000, ScheduleId = 37},
                new Tickets() {Price = 50000, ScheduleId = 38},
                new Tickets() {Price = 50000, ScheduleId = 39},
                new Tickets() {Price = 50000, ScheduleId = 40},
                new Tickets() {Price = 50000, ScheduleId = 41},
                new Tickets() {Price = 50000, ScheduleId = 42},
                new Tickets() {Price = 50000, ScheduleId = 43},
                new Tickets() {Price = 50000, ScheduleId = 44},
                new Tickets() {Price = 50000, ScheduleId = 45},
                new Tickets() {Price = 50000, ScheduleId = 46},
                new Tickets() {Price = 50000, ScheduleId = 47},
                new Tickets() {Price = 50000, ScheduleId = 48},
                new Tickets() {Price = 50000, ScheduleId = 49},
                new Tickets() {Price = 50000, ScheduleId = 50},
                new Tickets() {Price = 50000, ScheduleId = 51},

            };
            await context.Tickets.AddRangeAsync(tickets);

            List<Entities.Services> services = new List<Entities.Services>()
            {
                new Entities.Services() { Name = "Bắp", Price = 35000},
                new Entities.Services() { Name = "Nước ngọt", Price = 35000},
                new Entities.Services() { Name = "Combo Bắp nước", Price = 35000}
            };
            await context.Services.AddRangeAsync(services);


            var pass = "123456";
            //Encrypt password 
            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(pass);

            //Create random token verify 
            Random rnd = new Random();
            string verifyCode = "";
            for (int i = 0; i < 6; i++)
            {
                verifyCode += rnd.Next(0, 10).ToString();
            }
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(passwordBytes);

            List<User> users = new List<User>()
            {
                new User() { Email="admin@gmail.com", PasswordSalt = passwordSalt, PasswordHash=passwordHash,
                    Name = "Admin", Phone="0935667334", Dob= new DateTime(1998,06,01,19,00,00),
                    IsDelete=false, RoleId=1, Gender=true, IsEmailVerified=true  },
                new User() { Email="user01@gmail.com", PasswordSalt = passwordSalt, PasswordHash=passwordHash,
                    Name = "Nguyen Van A01", Phone="0935699934", Dob= new DateTime(2001,06,01,19,00,00),
                    IsDelete=false, RoleId=2, Gender=true, IsEmailVerified=false  },
                new User() { Email="user02@gmail.com", PasswordSalt = passwordSalt, PasswordHash=passwordHash,
                    Name = "Tran Huu An02", Phone="0935699934", Dob= new DateTime(2001,06,01,19,00,00),
                    IsDelete=false, RoleId=2, Gender=true, IsEmailVerified=false  },
                new User() { Email="user03@gmail.com", PasswordSalt = passwordSalt, PasswordHash=passwordHash,
                    Name = "Nguyen Van B03", Phone="0935699934", Dob= new DateTime(2001,06,01,19,00,00),
                    IsDelete=false, RoleId=2, Gender=true, IsEmailVerified=false  },
                new User() { Email="user04@gmail.com", PasswordSalt = passwordSalt, PasswordHash=passwordHash,
                    Name = "Le Thi Hoa04", Phone="0935699934", Dob= new DateTime(2001,06,01,19,00,00),
                    IsDelete=false, RoleId=2, Gender=true, IsEmailVerified=false  },
                new User() { Email="user05@gmail.com", PasswordSalt = passwordSalt, PasswordHash=passwordHash,
                    Name = "Nguyen Van Tuan05", Phone="0935699934", Dob= new DateTime(2001,06,01,19,00,00),
                    IsDelete=false, RoleId=2, Gender=true, IsEmailVerified=false  },
                new User() { Email="hungklyhongkl@gmail.com", PasswordSalt = passwordSalt, PasswordHash=passwordHash,
                    Name = "Trần Đức Hùng", Phone="0326614248", Dob= new DateTime(2001,07,26,00,00,00),
                    IsDelete=false, RoleId=2, Gender=true, IsEmailVerified=false  },
            };
            await context.Users.AddRangeAsync(users);
            context.SaveChanges();
        }
    }
}