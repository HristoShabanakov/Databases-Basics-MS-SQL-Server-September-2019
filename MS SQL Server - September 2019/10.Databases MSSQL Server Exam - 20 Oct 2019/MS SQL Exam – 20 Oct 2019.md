
## <p align="center"> **Database Basics MS SQL Exam – 20 Oct 2019** </p>
Problems description for Database Basics MSSQL Server” course at <a href="https://platform.softuni.bg/">Software University</a><br>
Students have 4 hours to create **database** called **Service**.<br>Implement database design, perform various queries and create one user defined function and one stored procedure.

![Service Database](https://user-images.githubusercontent.com/32416999/71515020-8423b600-28a1-11ea-991d-c0c6b8dfe3b3.png)
<br>
|You need to create 6 tables 

**Users** - contains information about the people who submit reports 

**Reports** - contains information about the problems 

**Employees** - contains information about the employees 

**Departments** - contains information about the departments  

**Categories** - contains information about categories in reports. 

**Status** - contains information about the possible  

## Problem 01 - <a href ="https://github.com/HristoShabanakov/Databases-Basics-MS-SQL-Server-September-2019/blob/master/MS%20SQL%20Server%20-%20September%202019/10.Databases%20MSSQL%20Server%20Exam%20-%2020%20Oct%202019/Databases%20MSSQL%20Server%20Exam%20-%2020%20Oct%202019/01.DDL.sql"> Table design </a>

#### Users

|**Column Name**|**Data Type**|**Constraints**|
|---|---|---|
|Id	| Integer from **0 to 2,147,483,647** |**Unique** table identificator, **Identity.** |
|Username	|String up to **30 symbols**|**Unique** for each user, **NULL** is **NOT** permitted. |
|Password	|String up to **50 symbols** |**NULL** is **NOT** permitted. |
|Name	|String up to **50 symbols**|**NULL** is permitted.|
|Birthdate	|Date **with time** |**NULL** is permitted.|
|Age	|Integer from **0 to 2,147,483,647**|In range between **14** and **110 (inclusive).**|
|Email	|String up to **50 symbols** |**NULL** is **NOT** permitted.|

#### Departments

|**Column Name**|**Data Type**|**Constraints**|
|---|---|---|
|Id	| Integer from **0 to 2,147,483,647** |**Unique** table identificator, **Identity.** |
|Name	|String up to **50 symbols**|**NULL** is **NOT** permitted. |


#### Employees

|**Column Name**|**Data Type**|**Constraints**|
|---|---|---|
|Id	| Integer from **0 to 2,147,483,647** |**Unique** table identificator, **Identity.**|
|FirstName| String up to **25 symbols**  |**NULL** is permitted.|
|LastName| String up to **25 symbols**  |**NULL** is permitted.|
|Birthdate| Date **with time** |**NULL** is permitted.|
|Age| Integer from **0 to 2,147,483,647** |In range between **18** and **110 (inclusive).**|
|DepartmentId| Integer from **0 to 2,147,483,647** |Relationship with table **departments.**|

#### Categories

|**Column Name**|**Data Type**|**Constraints**|
|---|---|---|
|Id	| Integer from **0 to 2,147,483,647** |**Unique** table identificator, **Identity.**|
|Name| String up to **50 symbols**  |**NULL** is **NOT** permitted.|
|DepartmentId| Integer from **0 to 2,147,483,647** |Relationship with table **departments.** **NULL** is **NOT** permitted .|

#### Status

|**Column Name**|**Data Type**|**Constraints**|
|---|---|---|
|Id	| Integer from **0 to 2,147,483,647** |**Unique** table identificator, **Identity.**|
|Label| String up to **30 symbols**  |**NULL** is **NOT** permitted.|

#### Reports

|**Column Name**|**Data Type**|**Constraints**|
|---|---|---|
|Id	| Integer from **0 to 2,147,483,647** |**Unique** table identificator, **Identity.**|
|CategoryId | Integer from **0 to 2,147,483,647**  |Relationship with table **categories. NULL is NOT** permitted.|
|StatusId | Integer from **0 to 2,147,483,647**  |Relationship with table **status.** **NULL** is **NOT** permitted. |
|OpenDate| Date **with time**  |**NULL** is **NOT** permitted.|
|CloseDate| Date **with time**  |**NULL** is permitted.|
|Description| String up to **200 symbols**   |**NULL** is **NOT** permitted.|
|UserId | Integer from **0 to 2,147,483,647**  |Relationship with table **users.** **NULL** is **NOT** permitted. |
|EmployeeId | Integer from **0 to 2,147,483,647**  |Relationship with table **employees.**|


## Problem 02 - Insert 
**Insert some sample data into the database.<br> Write a query to add the following records into the corresponding tables.<br> All Id's should be auto-generated.**

#### Employees

|**FirstName**|**LastName**|**Birthdate**|**DepartmentId**|
|---|---|---|---|
|Marlo| O'Malley |1958-9-21  | 1 |
|Niki | Stanaghan |1969-11-26| 4 |
|Ayrton | Senna |1960-03-21| 9 |
|Ronnie| Peterson |1944-02-14| 9 |
|Giovanna| Amati  |1959-07-20 | 5 |

#### Reports

|**CategoryId**|**StatusId**|**OpenDate**|**CloseDate**|**Description**|**UserId**|**EmployeeId**|
|---|---|---|---|---|---|---|
|1| 1 |2017-04-13 | | Stuck Road on Str.133 |6 | 2|
|6| 3 |2015-09-05  |2015-12-06  |Charity trail running  |3 | 5|
|14| 2 |2015-09-07 | | Falling bricks on Str.58  |5 | 2|
|4| 3 |2017-07-03 | 2017-07-06 | Cut off streetlight on Str.11 |1 | 1|

## Problem 03 - Update 
**Update the CloseDate with the current date of all reports, which don't have CloseDate.**

## Problem 04 - Delete 
**Delete all reports who have a Status 4.**

## Problem 05 - Unassigned Reports 
**Find all reports that don't have an assigned employee.<br> Order the results by OpenDate in ascending order, then by description ascending.<br> OpenDate must be in format - 'dd-MM-yyyy'.**

**Example:**

|**Description**|**OpenDate**|
|---|---|
|Art exhibition on July 24	| 17-12-2014 |
|Stuck Road on Str.133| 20-06-2015 |
|Burned facade on Str.560| 26-08-2015|

## Problem 06 - Reports & Categories
**Select all descriptions from reports, which have category.<br> Order them by description (ascending), then by category name (ascending).**

**Example:**

|**Description**|**CategoryName**|
|---|---|
|162 kg plastic for recycling.| Green Areas |
|246 kg plastic for recycling.| Snow Removal|
|366 kg plastic for recycling. | Recycling |

## Problem 07 - Most Reported Category
**Select the top 5 most reported categories and order them by the number of reports per category in descending order and then alphabetically by name.**

**Example:**

|**CategoryName**|**ReportsNumber**|
|---|---|
|Recycling |8 |
|Snow Removal| 5|
|Streetlight | 4 |

## Problem 08 - Birthday Report 
**Select the user's username and category name in all reports in which users have submitted a report on their birthday.<br> Order them by username (ascending) and then by category name (ascending).**

**Example:**

|**Username**|**CategoryName**|
|---|---|
|5omarkwelleyc | Snow Removal |
|dpennid | Dangerous Trees |
|llankham6  | Homeless Elders |


## Problem 09 - Users per Employee
**Select all employees and show how many unique users each of them has served to.<br>
Order by users count  (descending) and then by full name (ascending).**

**Example:**

|**FullName**|**UsersCount**|
|---|---|
|Bron Ledur  | 3 |
|Adelind Benns | 2 |
|Dick Wentworth | 2 |

## Problem 10 - Full Info
**Select all info for reports along with employee first name and last name (concataned with space), department name, category name, report description, open date, status label and name of the user.<br> Order them by first name (descending), last name (descending), department (ascending), category (ascending), description (ascending), open date (ascending), status (ascending) and user (ascending).<br>Date should be in format - dd.MM.yyyy<br>If there are empty records, replace them with 'None'.**


**Example:**

|**Employee**|**Department**|**Category**|**Description**|**OpenDate**|**Status**|**User**|
|---|---|---|---|---|---|---|
|Niki Stranaghan|Event Management|Sports Events|Sky Run competition on September 8|08.06.2015|Completed|Emlynn Alliberton|
|Marlo O'Malley|Infrastructure |Streetlight|Fallen streetlight columns on Str.14 |12.09.2017|Blocked|Erhart Alpine|
|Leonardo Shopcott|Animals Care|Animal in Danger|Parked car on green area on the sidewalk of Str.74|10.11.2016|In Progress|Jocko Gregor|

## Problem 11 - Hours to Complete 
**Create a user defined function with the name udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME) that receives a start date and end date and must returns the total hours which has been taken for this task. If start date is null or end is null return 0.**

**Example usage:**

|**Query**|**TotalHours**|
|---|---|
|SELECT dbo.udf_HoursToComplete(OpenDate, CloseDate) AS TotalHours FROM Reports| 120 |

## Problem 12 - Assign Employee
**Create a stored procedure with the name usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT) that receives an employee's Id and a report's Id and assigns the employee to the report only if the department of the employee and the department of the report's category are the same. Otherwise throw an exception with message: "Employee doesn't belong to the appropriate department!".**

**Example usage:**

|**Query**|**Response**|
|---|---|
|EXEC usp_AssignEmployeeToReport 30, 1 |Employee doesn't belong to the appropriate department!|
|EXEC usp_AssignEmployeeToReport 17, 2 |(1 row affected) | 

