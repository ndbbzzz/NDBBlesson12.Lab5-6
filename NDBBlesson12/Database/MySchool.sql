-- ============================================================
-- CSDL MySchool cho project TdpLesson12 (ASP.NET Core MVC + EF Core + SQL Server)
-- Chạy trong SSMS (Ctrl+A rồi F5). Có thể chạy lại nhiều lần: script sẽ tạo lại từ đầu.
-- ============================================================
IF DB_ID('MySchool') IS NULL CREATE DATABASE MySchool;
GO
USE MySchool;
GO

-- Xóa bảng cũ (Learner trước vì có khóa ngoại tới Major)
IF OBJECT_ID('dbo.Learner', 'U') IS NOT NULL DROP TABLE dbo.Learner;
IF OBJECT_ID('dbo.Major',   'U') IS NOT NULL DROP TABLE dbo.Major;
GO

CREATE TABLE dbo.Major (
    MajorID   INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Major PRIMARY KEY,
    MajorName NVARCHAR(100)     NOT NULL
);

CREATE TABLE dbo.Learner (
    LearnerID      INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Learner PRIMARY KEY,
    LastName       NVARCHAR(50)      NOT NULL,
    FirstMidName   NVARCHAR(50)      NOT NULL,
    MajorID        INT               NOT NULL,
    EnrollmentDate DATETIME          NOT NULL CONSTRAINT DF_Learner_Enroll DEFAULT (GETDATE()),
    CONSTRAINT FK_Learner_Major FOREIGN KEY (MajorID) REFERENCES dbo.Major(MajorID)
);
CREATE INDEX IX_Learner_MajorID ON dbo.Learner(MajorID);
GO

-- Dữ liệu mẫu
INSERT INTO dbo.Major (MajorName) VALUES (N'IT'), (N'Economics'), (N'Mathematics');

INSERT INTO dbo.Learner (LastName, FirstMidName, MajorID, EnrollmentDate) VALUES
    (N'Alexander', N'Carson',   1, '2005-09-01'),
    (N'Trần',      N'Tuấn Nam', 1, '2023-10-05 14:46'),
    (N'Nguyễn',    N'Lan',      2, '2022-09-05'),
    (N'Lê',        N'Minh',     3, '2021-09-06'),
    (N'Phạm',      N'Hương',    2, '2024-09-02'),
    (N'Hoàng',     N'Bảo',      1, '2024-09-02');
GO

SELECT * FROM dbo.Major;
SELECT l.LearnerID, l.LastName, l.FirstMidName, m.MajorName, l.EnrollmentDate
FROM dbo.Learner l JOIN dbo.Major m ON m.MajorID = l.MajorID;
GO
