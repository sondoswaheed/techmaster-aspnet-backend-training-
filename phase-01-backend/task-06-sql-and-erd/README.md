# Phase 01 - Task Pack: Library Management System

## Selected Scenario
Library Management & Book Borrowing System

## Main Entities
- **Authors**: Stores information about book authors.
- **Categories**: Classifies books into genres or subjects.
- **Books**: Contains catalog details and inventory counts.
- **Members**: Stores library cardholders' information.
- **BorrowRecords**: Tracks book loans, due dates, and return statuses.

## Relationships
- **Author has many Books**: One author can write multiple books ($1:N$).
- **Category has many Books**: One category encompasses multiple books ($1:N$).
- **Book has many BorrowRecords**: A single book title can be borrowed multiple times ($1:N$).
- **Member has many BorrowRecords**: A library member can borrow multiple books over time ($1:N$).

## Why I Designed It This Way
This database design is normalized to 3NF to prevent redundancy and maintain data integrity. The system separates static catalog entities (**Authors**, **Categories**, **Books**) from transactional data (**BorrowRecords**), allowing simple stock tracking via `AvailableCopies`. A junction approach in **BorrowRecords** handles the many-to-many relationship between **Members** and **Books**, tracking specific loan windows (`BorrowDate`, `DueDate`, `ReturnDate`) and current borrowing status. Primary keys ensure entity uniqueness, while foreign keys strictly enforce referential integrity across all relationships.

## SQL Queries

```sql
-- 1. Select all books
SELECT * FROM Books;

-- 2. Select all active members
SELECT * FROM Members 
WHERE IsActive = 'True';

-- 3. Select books by category
SELECT * FROM Books 
WHERE CategoryId = 1;

-- 4. Count books per category
SELECT c.Name, COUNT(b.BookId) AS TotalBooks 
FROM Categories c
LEFT JOIN Books b ON c.CategoryId = b.CategoryId
GROUP BY c.Name;

-- 5. Join query: Select borrow record details with Member and Book names
SELECT br.BorrowRecordId, m.FullName AS MemberName, b.Title AS BookTitle
FROM BorrowRecords br
JOIN Members m ON br.MemberId = m.MemberId
JOIN Books b ON br.BookId = b.BookId;

-- 6. Select overdue books
SELECT br.BorrowRecordId, b.Title, br.DueDate
FROM BorrowRecords br
JOIN Books b ON br.BookId = b.BookId
WHERE br.ReturnDate IS NULL AND br.DueDate < CURRENT_DATE();

-- 7. Select a member's borrowing history
SELECT * FROM BorrowRecords 
WHERE MemberId = 1;

-- 8. Select available books in stock
SELECT * FROM Books 
WHERE AvailableCopies > 0;

-- 9. Count books per author
SELECT a.FullName, COUNT(b.BookId) AS BookCount
FROM Authors a
LEFT JOIN Books b ON a.AuthorId = b.AuthorId
GROUP BY a.FullName;

-- 10. Top 5 most borrowed books
SELECT b.Title, COUNT(br.BorrowRecordId) AS BorrowCount
FROM Books b
JOIN BorrowRecords br ON b.BookId = br.BookId
GROUP BY b.Title
ORDER BY BorrowCount DESC 
LIMIT 5;
