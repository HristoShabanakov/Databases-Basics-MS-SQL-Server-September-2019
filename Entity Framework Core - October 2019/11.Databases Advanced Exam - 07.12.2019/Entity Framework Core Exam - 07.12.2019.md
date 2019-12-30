 Create a database application, using **Entity Framework Core**, using the **Code First** approach. 
 **Design** the domain **models** and **methods** for manipulating the **data**, as described below.<br>
 
 ![TeisterMask](https://user-images.githubusercontent.com/32416999/71554856-10aab180-2a25-11ea-992d-76f278f355d5.png)

## 01.<a href="https://github.com/HristoShabanakov/Databases-Basics-MS-SQL-Server-September-2019/tree/master/Entity%20Framework%20Core%20-%20October%202019/11.Databases%20Advanced%20Exam%20-%2007.12.2019/TeisterMask/Data/Models"> Model Definition </a><br>
The application needs to store the following data:<br> 
## Employee<br>

**Id** - integer, **Primary Key**. 

**Username** - text with length **[3, 40]**. Should contain only **lower** or **upper** case letters and/or **digits**. **(required)** 

**Email** – text **(required)**. Validate it! There is attribute for this job. 

**Phone** - text. **Consists only of three groups (separated by '-'), the first two consist of three digits and the last one - of 4 digits**. **(required)** 

**EmployeesTasks** - collection of type **EmployeeTask**.

## Project<br>

**Id** - integer, **Primary Key**. 

**Name** - text with length **[2, 40] (required)**. 

**OpenDate** - date and time **(required)**. 

**DueDate** - date and time **(can be null)**. 

**Tasks** - collection of type **Task**.

## Task<br>

**Id** - integer, **Primary Key**.

**Name** - text with length **[2, 40] (required)**.

**OpenDate** - date and time **(required)**. 

**DueDate** - date and time **(required)**. 

**ExecutionType** - enumeration of type **ExecutionType**, with possible values **(ProductBacklog, SprintBacklog, InProgress, Finished) (required)**. 

**LabelType** - enumeration of type **LabelType**, with possible values **(Priority, CSharpAdvanced, JavaAdvanced, EntityFramework, Hibernate) (required)**. 

**ProjectId** - integer, **foreign key (required)**. 

**Project** - **Project**. 

**EmployeesTasks** - collection of type **EmployeeTask**.

## EmployeeTask <br>

**EmployeeId** - integer, **Primary Key, foreign key (required)**. 

**Employee** - **Employee**. 

**TaskId** - integer, **Primary Key, foreign key (required)**. 

**Task** - **Task**. 

## 02. <a href="https://github.com/HristoShabanakov/Databases-Basics-MS-SQL-Server-September-2019/blob/master/Entity%20Framework%20Core%20-%20October%202019/11.Databases%20Advanced%20Exam%20-%2007.12.2019/TeisterMask/DataProcessor/Deserializer.cs"> Data Import </a><br>

Use the provided **JSON** and **XML** files to populate the **database** with **data**.

Import all the information from those files into the database. 

You are **not allowed** to modify the provided **JSON** and **XML** files. 

**If a record does not meet the requirements from the first section, print an error message:**

|**Error message**|
|---|
|Invalid Data!|

**XML Import** 

**Import Projects** 

Using the file **projects.xml**, import the **data** from the file into the **database**. Print information about each imported object in the format described below. 

**Constraints** 

If there are **any validation errors** for the **project entity** (such as invalid **name** or **open date**), **do not** import any part of the entity and **append an error message** to the **method output**. 

If there are **any validation errors** for the **task entity** (such as invalid **name**, **open** or **due date** are missing, **task open date** is before **project open date** or **task due date** is after **project due date**), **do not import it (only the task itself, not the whole project)** and **append an error message to the method output**. 

**NOTE**: Dates will be in format **dd/MM/yyyy**, do not forget to use **CultureInfo.InvariantCulture**.

|**Success message**|
|---|
|Successfully imported project - {projectName} with {tasksCount} tasks.|

**Example**

![importXML](https://user-images.githubusercontent.com/32416999/71560196-dd891200-2a66-11ea-8ade-9996446bb0aa.png)

![importXMLOutput](https://user-images.githubusercontent.com/32416999/71560216-1cb76300-2a67-11ea-99b0-7bd270eb62fd.png)

Upon **correct import logic**, you should have **imported 42 projects** and **62 tasks**. 

**JSON Import** 

**Import Employees** 

Using the file **employees.json**, import the **data** from that file into the **database**. Print information about each imported object in the format described below. 

**Constraints** 

If any validation errors occur (such as invalid **username**, **email** or **phone**), **do not import** any part of the entity and **append an error message** to the **method output**. 

Take only the **unique** tasks. 

If a **task** does **not exist** in the database, **append an error message** to the **method output** and **continue** with the next **task**. 

|**Success message**|
|---|
|Successfully imported employee - {employeeUsername} with {employeeTasksCount} tasks.|

**Example**

![importJSON](https://user-images.githubusercontent.com/32416999/71563248-8e55d800-2a8c-11ea-9463-ed3053326167.png)

![importJSONOutput](https://user-images.githubusercontent.com/32416999/71563282-fc9a9a80-2a8c-11ea-929e-9b26139e6758.png)

Upon **correct import logic**, you should have imported **30 employees** and **214 employee tasks**. 

## 03. <a href="https://github.com/HristoShabanakov/Databases-Basics-MS-SQL-Server-September-2019/blob/master/Entity%20Framework%20Core%20-%20October%202019/11.Databases%20Advanced%20Exam%20-%2007.12.2019/TeisterMask/DataProcessor/Serializer.cs"> Data Export </a><br>

**JSON Export**

**Export Most Busiest Employees** 

Select the **top 10 employees** who have **at least one task** that **its open date** is **after or equal** to the **given date** with 

their **tasks** that meet the same requirement (to have their open date after or equal to the giver date). 

For each **employee**, export their **username** and their **tasks**. 

For each **task**, export its **name** and **open date** (**must** be in format **"d"**), **due date** (**must** be in format **"d"**),

**label** and **execution**  type. Order the **tasks** by **due date (descending)**, then by **name (ascending)**. 

Order the **employees** by **all tasks count (descending)**, then by **username (ascending)**. 

**NOTE**: Do not forget to use **CultureInfo.InvariantCulture**.

**Example**

![json](https://user-images.githubusercontent.com/32416999/71542552-21492200-2968-11ea-9921-e0baa5e63f25.jpg)

**XML Export** 

**Export Projects with Their Tasks** 

Export all **projects** that have at least **one** task. 

For each **project**, export its **name**, **tasks count**, and if it **has end (due) date** which is represented like **"Yes"** and **"No"**. 

For each **task**, export its **name** and **label type**. Order the **tasks** by **name (ascending)**. 

Order the **projects** by **tasks count (descending)**, then by **name (ascending)**. 

**Example**

![xml](https://user-images.githubusercontent.com/32416999/71554857-13a5a200-2a25-11ea-938f-7a81202c3426.jpg)
