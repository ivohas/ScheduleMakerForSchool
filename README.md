Adding Subjects:(Removed for now) The first page is straightforward where subjects are added. This likely involves a simple form submission that interacts with your backend to store subject data in the database.

**Adding Teachers(Need to add delete row for teacher's subject) : On the second page, where you input teacher information, you seem to be facing an issue with the dropdown for assigning subjects to teachers. Here are a few steps you might take to troubleshoot this part:

Ensure that the dropdown is correctly populated with the subjects from the database.
Check that the form correctly binds the selected subject to the teacher when submitting.
Validate that the backend correctly associates the teacher with their subject(s) and persists this information in the database.
For the dropdown issue, here is a simple approach to troubleshoot:

Verify that the select element in your HTML is correctly named and that it's populated with option elements representing subjects.
In your ASP.NET backend, ensure that you are fetching the list of subjects from the database and passing them to the view correctly.
Use model binding to ensure that when the form is submitted, the selected subject is correctly associated with the teacher.
**Adding Classes: For the classes, you'll need a similar setup:

A form to input class names, associated subjects, and the frequency of each subject per week.
Ensure that the backend can handle this complex data, which might involve multiple entities and relationships.
Consider creating a view model that represents the form data as it will be submitted.
2.1- 3.9. Adding/Deleting Rows for New Data: This functionality typically involves JavaScript to dynamically add or remove elements from the DOM.

For adding a new row, you would clone an existing row or create a new one from scratch and append it to the form.
For deleting, you would remove the row from the DOM and, if it already exists in the database, ensure that an action is triggered to remove it from there as well.
Automaticly give a teacher class (Not implemented - In progress)
An option to change it on hand (Not implemented)
After a manual accepteance to create the schedule PART-2: My Subjects in a schedule! PART-3: Consultation part
Give all info for consultation(will be made a with roles and autotication)
Put them in a schedule
Avelable for all studdents and teacher, with option to join it PART-4: My Consulatations
