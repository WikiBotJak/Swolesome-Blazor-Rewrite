Greetings Blogson,

You can find all of the code in the folder **“Swolesome vip”**. The main parent folder just holds our solution file and allows for additional projects we may create in this solution.

Inside the folder you’ll find all the individual pages and components for the project. I have added comments throughout the code to help you understand what each page is doing.

## Why Blazor?

As you know, Blazor is purely component-driven. That means:

- Everything you see on the screen is a component (a .razor file) combining HTML markup + C# logic. 

- Components can be nested and reused, which helps maintain a clean and modular architecture. 

- The UI is tied to state (variables) and events (methods) rather than only static HTML — you can respond to user interaction, change variables, and the UI updates.

## Project Structure

Here’s a quick overview of how the project is organized:

- Pages/ (or similar): Contains page components (routable via @page directive).

- Components/: Smaller UI parts (tables, search panels, result rows) that are used by pages.

- Layouts/: Layouts, wrappers and shared UI across multiple pages.

- Services/: Business logic or API clients (e.g., ApiService).

- Models/: Data structures (e.g., SearchResult) representing your data.
