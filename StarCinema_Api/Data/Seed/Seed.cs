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

            //Rooms rooms = new Rooms();
            //rooms.Name = "R001";
            //await context.Rooms.AddAsync(rooms);

            //List<Seats> seats = new List<Seats>();
            //char[] arr = new char[] { 'A', 'B', 'E', 'F' };
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    for (int j = 1; j < 10; j++)
            //    {
            //        var seat = new Seats() { Name = arr[i] + "" + j, RoomId = 1 };
            //        seats.Add(seat);
            //    }
            //}
            //await context.Seats.AddRangeAsync(seats);
            
            //List<Categories> categories = new List<Categories>()
            //{
            //    new Categories() {Name = "3D"},
            //    new Categories() {Name = "Tình cảm"},
            //    new Categories() {Name = "Đời sống"},
            //    new Categories() {Name = "Hành động"}
            //};
            //await context.Categories.AddRangeAsync(categories) ;

            //List<Films> films = new List<Films>()
            //{
            //    new Films() {Name = "Avatar2", CategoryId = 1, Description = "Avatar2 dong chay cua nuoc", Country = "America", Director = "John", Duration = 3, Producer = "Jack", Release = new DateTime(2023,05,01,00,00,00), VideoLink = "https://youtu.be/Ru4Jbmh7bcQ" },
            //    new Films() {Name = "Nhà Bà Nữ", CategoryId = 3, Description = "Phim Nhà Bà Nữ, một bộ phim của Trần Thành", Country = "Việt Nam", Director = "Trấn Thành", Duration = 2, Producer = "Trấn Thành", Release = new DateTime(2023,05,05,00,00,00), VideoLink = "https://youtu.be/IkaP0KJWTsQ" },
            //};
            //await context.Films.AddRangeAsync(films);

            //List<Schedules> schedules = new List<Schedules>()
            //{
            //    new Schedules() {StartTime = new DateTime(2023,06,01,19,00,00), EndTime= new DateTime(2023,06,01,21,00,00), FilmId = 1, RoomId = 1 },
            //    new Schedules() {StartTime = new DateTime(2023,06,01,21,00,00), EndTime= new DateTime(2023,06,01,23,00,00), FilmId = 2, RoomId = 1 },
            //    new Schedules() {StartTime = new DateTime(2023,06,01,17,00,00), EndTime= new DateTime(2023,06,01,19,00,00), FilmId = 1, RoomId = 1 }
            //};
            //await context.Schedules.AddRangeAsync(schedules);

            //Tickets tickets = new Tickets()
            //{
            //    Price = 70000,
            //    ScheduleId = 1
            //};
            //await context.Tickets.AddRangeAsync(tickets);

            //List<Entities.Services> services = new List<Entities.Services>()
            //{
            //    new Entities.Services() { Name = "Bắp", Price = 35000},
            //    new Entities.Services() { Name = "Nước ngọt", Price = 35000},
            //    new Entities.Services() { Name = "Combo Bắp nước", Price = 35000}
            //};
            //await context.Services.AddRangeAsync(services);

            context.SaveChanges();
        }
    }
}