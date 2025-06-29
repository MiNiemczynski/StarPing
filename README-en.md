<h2>Description</h2>
<p>
  StarPing is a web application for an internet service provider, featuring a store with subscription offers and networking devices. It is built using the <strong>MVC</strong> (Model-View-Controller) design pattern. The application allows users to browse the offer, place orders, and manage their shopping cart.
</p>

<p>
  The project consists of three application layers:
</p>
<ul>
  <li><strong>Website</strong> – This is the web application itself; it sends requests to the API, processes the received data, and renders views for the user.</li>
  <li><strong>Data Model</strong> – Contains a set of data models and the database context.</li>
  <li><strong>API</strong> – Provides access to functions and data; allows downloading and modifying data from the database.</li>
</ul>

<h2>Technologies</h2>
<ul>
  <li>ASP.NET Core MVC</li>
  <li>Entity Framework Core</li>
  <li>Razor</li>
  <li>REST API (JSON)</li>
</ul>

<h2>Website</h2>
<p>
  <strong>Razor</strong> is used for rendering views – a convenient combination of HTML and C# that allows easy interaction with controller functions or redirection to others. This is done using ASP attributes or <code>Url.Action()</code>, depending on the need.
  Website controllers retrieve data from services and call appropriate views for presentation.
  Services are responsible for sending requests to the API, receiving the data, and deserializing it.
</p>

<h2>Data Model Layer</h2>
<p>
  This is a class library – it contains the database context and entities that define its structure and relations. The application was built using the <strong>Code First</strong> approach, allowing the database to be recreated.
</p>

<h2>API</h2>
<p>
  The API acts as the data source and query handler for the website. It receives requests through URL addresses with optional parameters or serialized objects. Then, using services, it fetches or modifies data and returns it to the client with an appropriate response code.
  <strong>Entity Framework Core</strong> is used to communicate with the database.
</p>

<h2>License</h2>
<p>
  This project is released under the <strong>MIT</strong> license.
</p>
