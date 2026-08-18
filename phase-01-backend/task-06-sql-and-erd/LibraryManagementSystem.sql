create database LibrarySystem


Create Table Authors (
    AuthorId INT Identity(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    BirthDate DATE NULL,
    Country NVARCHAR(50) NULL
);

CREATE Table Categories (
    CategoryId INT Identity(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255) NULL
);

Create Table Books (
    BookId INT Identity(1,1) PRIMARY KEY,
    Title NVARCHAR(150) NOT NULL,
    ISBN VARCHAR(20) NOT NULL UNIQUE,
    PublishedYear INT NULL,
    AvailableCopies INT DEFAULT 0,
    AuthorId INT NOT NULL,
    CategoryId INT NOT NULL,
    Constraint FK_Books_Authors FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId),
    Constraint FK_Books_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

Create Table Members (
    MemberId INT Identity(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber VARCHAR(20) NULL,
    JoinDate DATE DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

Create Table BorrowRecords (
    BorrowRecordId INT Identity(1,1) PRIMARY KEY,
    BookId INT NOT NULL,
    MemberId INT NOT NULL,
    BorrowDate DATE NOT NULL,
    DueDate DATE NOT NULL,
    ReturnDate DATE NULL,
    Status NVARCHAR(20) NOT NULL, 
    Constraint FK_BorrowRecords_Books FOREIGN KEY (BookId) REFERENCES Books(BookId),
    Constraint FK_BorrowRecords_Members FOREIGN KEY (MemberId) REFERENCES Members(MemberId)
);
go

Insert Into Authors (FullName, BirthDate, Country) VALUES
(N'Naguib Mahfouz', '1911-12-11', 'Egypt'),
(N'George Orwell', '1903-06-25', 'United Kingdom'),
(N'Agatha Christie', '1890-09-15', 'United Kingdom'),
(N'Ahmed Khaled Towfik', '1962-06-10', 'Egypt');

Insert Into Categories (Name, Description) VALUES
(N'Fiction', N'Fictional literature and stories'),
(N'Science Fiction', N'Speculative fiction involving futuristic science and technology'),
(N'Mystery', N'Stories focused on solving a crime or puzzle'),
(N'History', N'Books about historical events and figures');

Insert Into Books (Title, ISBN, PublishedYear, AvailableCopies, AuthorId, CategoryId) VALUES
(N'Palace Walk', '9789774163531', 1956, 3, 1, 1),
(N'1984', '9780451524935', 1949, 0, 2, 2),
(N'Murder on the Orient Express', '9780062073495', 1934, 2, 3, 3),
(N'Utopia', '9789770923053', 2008, 5, 4, 2),
(N'Animal Farm', '9780451526342', 1945, 1, 2, 1);

Insert Into Members (FullName, Email, PhoneNumber, JoinDate, IsActive) VALUES
(N'Ahmed Ali', 'ahmed.ali@example.com', '01012345678', '2025-01-15', 1),
(N'Sara Mohamed', 'sara.m@example.com', '01123456789', '2025-02-01', 1),
(N'Omar Hassan', 'omar.h@example.com', '01234567890', '2025-03-10', 0),
(N'Mona Mahmoud', 'mona.m@example.com', '01545678901', '2025-04-05', 1);

Insert Into BorrowRecords (BookId, MemberId, BorrowDate, DueDate, ReturnDate, Status) VALUES
(1, 1, '2026-07-01', '2026-07-15', '2026-07-14', N'Returned'),
(2, 1, '2026-08-01', '2026-08-15', NULL, N'Overdue'),
(3, 2, '2026-08-10', '2026-08-24', NULL, N'Borrowed'),
(4, 4, '2026-08-05', '2026-08-19', '2026-08-12', N'Returned'),
(2, 2, '2026-06-01', '2026-06-15', '2026-06-10', N'Returned');
GO

select * from Books

select * from Members where IsActive=1;

select * from Books where CategoryId=1;

select c.Name as CategoryName, count(b.BookId) as TotalBooks
from Categories c
left join Books b on c.CategoryId = b.CategoryId
group by c.Name;


select br.BorrowRecordId, m.FullName as MemberName, b.Title as BookTitle, br.BorrowDate, br.DueDate, br.Status
from BorrowRecords br
join Members m on br.MemberId = m.MemberId
join Books b on br.BookId = b.BookId;


select br.BorrowRecordId, b.Title as BookTitle, m.FullName as MemberName, br.DueDate
from BorrowRecords br
join Books b on br.BookId = b.BookId
join Members m on br.MemberId = m.MemberId
where br.ReturnDate is null and br.DueDate < getdate();


select br.BorrowRecordId, b.Title as BookTitle, br.BorrowDate, br.DueDate, br.ReturnDate, br.Status
from BorrowRecords br
join Books b on br.BookId = b.BookId
where br.MemberId = 1;


select * from Books 
where AvailableCopies > 0;


select a.FullName as AuthorName, count(b.BookId) as TotalBooks
from Authors a
left join Books b on a.AuthorId = b.AuthorId
group by a.FullName;


select top 5 b.Title, count(br.BorrowRecordId) as TimesBorrowed
from Books b
join BorrowRecords br on b.BookId = br.BookId
group by b.Title
order by TimesBorrowed desc;