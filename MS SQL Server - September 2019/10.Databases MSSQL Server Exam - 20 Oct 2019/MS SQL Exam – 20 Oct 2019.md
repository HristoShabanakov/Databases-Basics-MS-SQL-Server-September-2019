
## Create database called Service
![Service Database](https://user-images.githubusercontent.com/32416999/71515020-8423b600-28a1-11ea-991d-c0c6b8dfe3b3.png)
<br>
|You need to create 6 tables 

**Users** - contains information about the people who submit reports 

**Reports** - contains information about the problems 

**Employees** - contains information about the employees 

**Departments** - contains information about the departments  

**Categories** - contains information about categories in reports. 

**Status** - contains information about the possible  

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
