/*using Microsoft.EntityFrameworkCore;
using StudentHousingHub.Core.Entities;
using StudentHousingHub.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentHousingHub.Repository
{
    public class AppDbContextSeed
    {
        public async static Task SeedAsync(AppDbContext _context)
        {*//*
                // Admin
                if (_context.Admin.Count() == 0)
                {
                    var AdminData = File.ReadAllText(@"..\StudentHousingHub.Repository\Data\DataSeed\Admin.json");
                    var admins = JsonSerializer.Deserialize<List<Admin>>(AdminData);

                    if (admins?.Count > 0)
                    {
                        await _context.Admin.AddRangeAsync(admins);
                        await _context.SaveChangesAsync();
                    }
                }

                // Owners - يجب أن يتم إنشاؤهم أولاً
                if (_context.Ownerss.Count() == 0)
                {
                    var OwnerData = File.ReadAllText(@"..\StudentHousingHub.Repository\Data\DataSeed\Ownerss.json");
                    var owners = JsonSerializer.Deserialize<List<Ownerss>>(OwnerData);

                    if (owners?.Count > 0)
                    {
                        await _context.Ownerss.AddRangeAsync(owners);
                        await _context.SaveChangesAsync(); // حفظ المالكين أولاً
                    }
                }

                // Rooms - يجب التحقق من وجود المالكين أولاً
                if (_context.Roomss.Count() == 0)
                {
                    var RoomData = File.ReadAllText(@"..\StudentHousingHub.Repository\Data\DataSeed\Roomss.json");
                    var rooms = JsonSerializer.Deserialize<List<Roomss>>(RoomData);

                    if (rooms?.Count > 0)
                    {
                        // جلب جميع OwnerIDs الموجودة في قاعدة البيانات
                        var existingOwnerIds = await _context.Ownerss.Select(o => o.id).ToListAsync();

                        // تصفية الغرف لضمان أن جميع OwnerIDs موجودة
                        var validRooms = rooms.Where(r => r.OwnerID.HasValue &&
                                                        existingOwnerIds.Contains(r.OwnerID.Value))
                                            .ToList();

                        // تسجيل أي غرف تم تخطيها بسبب عدم وجود مالك
                        var invalidRooms = rooms.Except(validRooms).ToList();
                        if (invalidRooms.Any())
                        {
                            Console.WriteLine($"Warning: Skipped {invalidRooms.Count} rooms due to missing owners.");
                        }

                        await _context.Roomss.AddRangeAsync(validRooms);
                        await _context.SaveChangesAsync();
                    }
                }
            */
            ///*
            // Admin
            //C:\Users\20110\OneDrive\Important\GraduationProject\GraduationProject.Repository\Data\DataSeed\Admin.json
/*
            if (_context.Admin.Count() == 0)
            {
                // 1. Read data from json file
                var AdminData = File.ReadAllText(@"..\StudentHousingHub.Repository\Data\DataSeed\Admin.json");

                // 2. Convert json string to List<T>
                var admins = JsonSerializer.Deserialize<List<Admin>>(AdminData);

                // 3. Seed Data to DB
                if (admins is not null && admins.Count() > 0)
                {
                    await _context.Admin.AddRangeAsync(admins);
                }
            }
            
            // Owner
            if (_context.Owners.Count() == 0)
            {
                // 1. Read data from json file
                var OwnerData = File.ReadAllText(@"..\StudentHousingHub.Repository\Data\DataSeed\Owners.json");

                // 2. Convert json string to List<T>
                var owners = JsonSerializer.Deserialize<List<Owners>>(OwnerData);

                // 3. Seed Data to DB
                if (owners is not null && owners.Count() > 0)
                {
                    await _context.Owners.AddRangeAsync(owners);
                }
            }


            // Rooms
            if (_context.Rooms.Count() == 0)
            {
                // 1. Read data from json file
                var RoomData = File.ReadAllText(@"..\StudentHousingHub.Repository\Data\DataSeed\Rooms.json");

                // 2. Convert json string to List<T>
                var rooms = JsonSerializer.Deserialize<List<Rooms>>(RoomData);

                // 3. Seed Data to DB
                if (rooms is not null && rooms.Count() > 0)
                {
                    await _context.Rooms.AddRangeAsync(rooms);
                }
            }
            //*/

           /* // Student
            if (_context.Students.Count() == 0)
            {
                // 1. Read data from json file
                var StudentData = File.ReadAllText(@"..\StudentHousingHub.Repository\Data\DataSeed\Students.json");

                // 2. Convert json string to List<T>
                var students = JsonSerializer.Deserialize<List<Students>>(StudentData);

                // 3. Seed Data to DB
                if (students is not null && students.Count() > 0)
                {
                    await _context.Students.AddRangeAsync(students);
                }
            }


            // Report
            if (_context.Reports.Count() == 0)
            {
                // 1. Read data from json file
                var ReportData = File.ReadAllText(@"..\StudentHousingHub.Repository\Data\DataSeed\Reports.json");

                // 2. Convert json string to List<T>
                var reports = JsonSerializer.Deserialize<List<Reports>>(ReportData);

                // 3. Seed Data to DB
                if (reports is not null && reports.Count() > 0)
                {
                    await _context.Reports.AddRangeAsync(reports);
                }
            }

            /*
            // Contact_us
            if (_context.Contactss.Count() == 0)
            {
                // 1. Read data from json file
                var ContactData = File.ReadAllText(@"..\StudentHousingHub.Repository\Data\DataSeed\ContactUs.json");

                // 2. Convert json string to List<T>
                var Contacts = JsonSerializer.Deserialize<List<ContactUss>>(ContactData);

                // 3. Seed Data to DB
                if (Contacts is not null && Contacts.Count() > 0)
                {
                    await _context.Contactss.AddRangeAsync(Contacts);
            await _context.SaveChangesAsync();
                }
            }*/
           /*
            await _context.SaveChangesAsync();

        }
    }
}*/
