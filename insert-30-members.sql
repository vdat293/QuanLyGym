-- Script SQL để insert 30 hội viên vào database
-- Cách dùng:
-- 1. Mở DB Browser for SQLite
-- 2. Mở file gym.db (ở %LocalAppData%\QuanLyPhongGym\gym.db)
-- 3. Vào tab "Execute SQL"
-- 4. Copy toàn bộ script này và paste vào
-- 5. Click "Play" (▶) hoặc nhấn F5

-- Xóa dữ liệu cũ nếu có (TÙY CHỌN)
-- DELETE FROM Members;

-- Insert 30 hội viên với dữ liệu thực tế
INSERT INTO Members (FamilyName, MiddleName, GivenName, Phone, CCCD, BirthDate, MembershipPlanId, JoinDate, TotalTrainingDays, IsActive, PtStatus, CreatedAt)
VALUES
-- Hội viên tham gia 6 tháng trước
('Nguyễn', 'Văn', 'An', '0901234567', NULL, '1995-03-15', 6, date('now', '-6 months'), 72, 1, 1, datetime('now', '-6 months')),
('Trần', 'Thị', 'Bình', '0902345678', NULL, '1998-07-22', 7, date('now', '-6 months', '-5 days'), 68, 1, 0, datetime('now', '-6 months', '-5 days')),
('Lê', 'Hoàng', 'Cường', '0903456789', NULL, '1992-11-10', 10, date('now', '-6 months', '-10 days'), 70, 1, 1, datetime('now', '-6 months', '-10 days')),

-- Hội viên tham gia 5 tháng trước
('Phạm', 'Minh', 'Dũng', '0904567890', NULL, '1996-01-08', 8, date('now', '-5 months'), 60, 1, 2, datetime('now', '-5 months')),
('Hoàng', 'Thị', 'Hoa', '0905678901', NULL, '1999-05-19', 5, date('now', '-5 months', '-3 days'), 58, 1, 0, datetime('now', '-5 months', '-3 days')),
('Vũ', 'Đức', 'Hùng', '0906789012', NULL, '1994-09-25', 9, date('now', '-5 months', '-7 days'), 62, 1, 1, datetime('now', '-5 months', '-7 days')),

-- Hội viên tham gia 4 tháng trước
('Đỗ', 'Thị', 'Lan', '0907890123', NULL, '1997-02-14', 6, date('now', '-4 months'), 50, 1, 0, datetime('now', '-4 months')),
('Bùi', 'Văn', 'Long', '0908901234', NULL, '1993-06-30', 7, date('now', '-4 months', '-2 days'), 48, 1, 1, datetime('now', '-4 months', '-2 days')),
('Đinh', 'Quang', 'Minh', '0909012345', NULL, '1991-10-12', 11, date('now', '-4 months', '-8 days'), 52, 1, 1, datetime('now', '-4 months', '-8 days')),
('Ngô', 'Thị', 'Nga', '0910123456', NULL, '2000-04-05', 5, date('now', '-4 months', '-12 days'), 46, 1, 0, datetime('now', '-4 months', '-12 days')),

-- Hội viên tham gia 3 tháng trước
('Trương', 'Văn', 'Phúc', '0911234567', NULL, '1995-08-20', 3, date('now', '-3 months'), 36, 1, 2, datetime('now', '-3 months')),
('Lý', 'Thị', 'Quỳnh', '0912345678', NULL, '1998-12-18', 6, date('now', '-3 months', '-5 days'), 38, 1, 1, datetime('now', '-3 months', '-5 days')),
('Mai', 'Xuân', 'Sơn', '0913456789', NULL, '1996-03-07', 9, date('now', '-3 months', '-10 days'), 35, 1, 0, datetime('now', '-3 months', '-10 days')),
('Võ', 'Thị', 'Thảo', '0914567890', NULL, '1999-07-11', 2, date('now', '-3 months', '-15 days'), 32, 1, 0, datetime('now', '-3 months', '-15 days')),

-- Hội viên tham gia 2 tháng trước
('Đặng', 'Minh', 'Tuấn', '0915678901', NULL, '1994-11-23', 2, date('now', '-2 months'), 24, 1, 1, datetime('now', '-2 months')),
('Dương', 'Thị', 'Uyên', '0916789012', NULL, '1997-04-16', 5, date('now', '-2 months', '-3 days'), 22, 1, 0, datetime('now', '-2 months', '-3 days')),
('Tô', 'Văn', 'Việt', '0917890123', NULL, '1992-08-09', 3, date('now', '-2 months', '-7 days'), 26, 1, 2, datetime('now', '-2 months', '-7 days')),
('Phan', 'Thị', 'Xuân', '0918901234', NULL, '2001-01-28', 6, date('now', '-2 months', '-12 days'), 28, 1, 1, datetime('now', '-2 months', '-12 days')),

-- Hội viên tham gia 1 tháng trước
('Hồ', 'Quốc', 'Anh', '0919012345', NULL, '1996-05-14', 1, date('now', '-1 month'), 12, 1, 0, datetime('now', '-1 month')),
('Cao', 'Thị', 'Bảo', '0920123456', NULL, '1998-09-21', 2, date('now', '-1 month', '-2 days'), 14, 1, 0, datetime('now', '-1 month', '-2 days')),
('Lâm', 'Văn', 'Chính', '0921234567', NULL, '1993-02-03', 5, date('now', '-1 month', '-5 days'), 16, 1, 1, datetime('now', '-1 month', '-5 days')),
('Tạ', 'Thị', 'Diễm', '0922345678', NULL, '2000-06-17', 1, date('now', '-1 month', '-10 days'), 10, 1, 0, datetime('now', '-1 month', '-10 days')),

-- Hội viên tham gia 3 tuần trước
('Từ', 'Minh', 'Em', '0923456789', NULL, '1995-10-29', 1, date('now', '-21 days'), 9, 1, 2, datetime('now', '-21 days')),
('Chu', 'Thị', 'Giang', '0924567890', NULL, '1999-03-12', 2, date('now', '-20 days'), 8, 1, 0, datetime('now', '-20 days')),

-- Hội viên tham gia 2 tuần trước
('Kiều', 'Văn', 'Hải', '0925678901', NULL, '1994-07-24', 1, date('now', '-14 days'), 6, 1, 1, datetime('now', '-14 days')),
('La', 'Thị', 'Khánh', '0926789012', NULL, '1997-11-06', 5, date('now', '-12 days'), 5, 1, 0, datetime('now', '-12 days')),

-- Hội viên tham gia 1 tuần trước
('Tăng', 'Minh', 'Linh', '0927890123', NULL, '1996-04-18', 1, date('now', '-7 days'), 3, 1, 0, datetime('now', '-7 days')),
('Ông', 'Thị', 'My', '0928901234', NULL, '1998-08-30', 2, date('now', '-6 days'), 2, 1, 1, datetime('now', '-6 days')),

-- Hội viên mới tham gia
('Lương', 'Văn', 'Nam', '0929012345', NULL, '1993-01-15', 1, date('now', '-3 days'), 1, 1, 0, datetime('now', '-3 days')),
('Hà', 'Thị', 'Oanh', '0930123456', NULL, '2001-05-22', 5, date('now', '-1 day'), 1, 1, 2, datetime('now', '-1 day'));

-- Kiểm tra kết quả
SELECT COUNT(*) AS 'Tổng số hội viên' FROM Members;

SELECT
    Id,
    FamilyName || ' ' || COALESCE(MiddleName || ' ', '') || GivenName AS FullName,
    Phone,
    JoinDate,
    CASE PtStatus
        WHEN 0 THEN 'Không có PT'
        WHEN 1 THEN 'Có PT'
        WHEN 2 THEN 'PT ngoài'
    END AS PtStatus,
    TotalTrainingDays
FROM Members
ORDER BY JoinDate DESC
LIMIT 10;
