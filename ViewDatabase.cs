using System;
using System.IO;
using Microsoft.Data.Sqlite;

class ViewDatabase
{
    static void Main()
    {
        var folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "QuanLyPhongGym");
        var dbPath = Path.Combine(folder, "gym.db");

        if (!File.Exists(dbPath))
        {
            Console.WriteLine($"Database không tồn tại: {dbPath}");
            Console.WriteLine("Hãy chạy ứng dụng trước để tạo database!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Database: {dbPath}");
        Console.WriteLine(new string('=', 100));

        using var connection = new SqliteConnection($"Data Source={dbPath}");
        connection.Open();

        // Đếm số lượng
        var countCmd = connection.CreateCommand();
        countCmd.CommandText = "SELECT COUNT(*) FROM Members";
        var count = (long)countCmd.ExecuteScalar();
        Console.WriteLine($"\nTổng số hội viên: {count}\n");

        // Lấy danh sách
        var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT
                m.Id,
                m.FamilyName || ' ' || COALESCE(m.MiddleName || ' ', '') || m.GivenName AS FullName,
                m.Phone,
                p.Name AS PlanName,
                m.JoinDate,
                m.PtStatus,
                m.TotalTrainingDays
            FROM Members m
            LEFT JOIN MembershipPlans p ON m.MembershipPlanId = p.Id
            ORDER BY m.JoinDate DESC";

        using var reader = command.ExecuteReader();

        Console.WriteLine($"{"ID",-5} {"Họ và tên",-25} {"SĐT",-15} {"Gói tập",-30} {"Ngày tham gia",-15} {"PT",-15} {"Ngày tập",-10}");
        Console.WriteLine(new string('-', 120));

        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var fullName = reader.GetString(1);
            var phone = reader.IsDBNull(2) ? "" : reader.GetString(2);
            var planName = reader.IsDBNull(3) ? "" : reader.GetString(3);
            var joinDate = DateTime.Parse(reader.GetString(4)).ToString("dd/MM/yyyy");
            var ptStatus = reader.GetInt32(5);
            var ptStatusText = ptStatus switch
            {
                0 => "Không có PT",
                1 => "Có PT",
                2 => "PT ngoài",
                _ => ""
            };
            var trainingDays = reader.GetInt32(6);

            Console.WriteLine($"{id,-5} {fullName,-25} {phone,-15} {planName,-30} {joinDate,-15} {ptStatusText,-15} {trainingDays,-10}");
        }

        Console.WriteLine(new string('=', 100));
        Console.WriteLine("\nNhấn phím bất kỳ để thoát...");
        Console.ReadKey();
    }
}
